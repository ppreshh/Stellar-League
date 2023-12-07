using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class RaceTrack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraController m_CameraController = null;
    [SerializeField] private RaceRing m_RaceRingPrefabReference = null;

    [Header("Race Track Selection")]
    [SerializeField] private int m_RaceTrackNumber;

    private List<RaceRing> m_RaceRings;
    private double m_StartTime;

    public delegate void RaceFinished(string finishTime);
    public event RaceFinished OnRaceFinished;
    private void RaiseOnRaceFinished(string finishTime)
    {
        if (OnRaceFinished != null)
        {
            OnRaceFinished(finishTime);
        }
    }

    private void Awake()
    {
        InitRaceTrack();

        foreach(RaceRing raceRing in m_RaceRings)
        {
            raceRing.OnEntered += OnEntered;
        }
    }

    private void Start()
    {
        m_StartTime = Time.fixedTimeAsDouble;
    }

    private void OnDestroy()
    {
        foreach (RaceRing raceRing in m_RaceRings)
        {
            raceRing.OnEntered -= OnEntered;
        }
    }

    private void OnEntered(RaceRing.Type type, Transform nextRingTransform)
    {
        if (type == RaceRing.Type.FIRST)
        {
            m_StartTime = Time.fixedTimeAsDouble;
        }
        else if (type == RaceRing.Type.FINAL)
        {
            var completionTime = TimeSpan.FromSeconds(Time.fixedTimeAsDouble - m_StartTime);
            RaiseOnRaceFinished(string.Format("{0:00}:{1:00}", completionTime.Minutes, completionTime.Seconds));

            //m_RaceRings[0].IsCurrentRing = true;
        }

        m_CameraController.TargetToLockOn = nextRingTransform;
    }

    private void InitRaceTrack()
    {
        m_RaceRings = new List<RaceRing>();

        List<Vector3> positions = new List<Vector3>();
        List<Vector3> velocities = new List<Vector3>();

        TextAsset raceTrackCSV = Resources.Load<TextAsset>("RaceTracks/RaceTrack" + m_RaceTrackNumber);
        char[] splitChars = { ',', ' ' };
        var data = raceTrackCSV.text.Replace(" ", string.Empty).Replace("\r\n", " ").Split(splitChars);

        for (int i = 0; i < data.Length-1; i += 6)
        {
            Vector3 position = new Vector3(float.Parse(data[i].Trim('(')), float.Parse(data[i+1]), float.Parse(data[i+2].Trim(')')));
            Vector3 velocity = new Vector3(float.Parse(data[i+3].Trim('(')), float.Parse(data[i+4]), float.Parse(data[i+5].Trim(')')));

            positions.Add(position);
            velocities.Add(velocity);
        }

        int count = 0;
        foreach (Vector3 position in positions)
        {
            RaceRing raceRing = Instantiate(m_RaceRingPrefabReference, transform);
            raceRing.transform.position = position;
            raceRing.transform.rotation = Quaternion.LookRotation(velocities[count]);

            float scaleFactor = Mathf.Max(0.8f, velocities[count].magnitude / 30f);
            raceRing.transform.localScale = new Vector3(raceRing.transform.localScale.x * scaleFactor, raceRing.transform.localScale.y * scaleFactor, raceRing.transform.localScale.z * scaleFactor);

            if (count == 0)
            {
                raceRing.SetAsFirstRing();
                m_CameraController.TargetToLockOn = raceRing.LockOnPoint;
            }
            if (count == positions.Count - 1)
            {
                raceRing.SetAsFinalRing();
            }
            
            if (count != 0)
            {
                m_RaceRings[count - 1].NextRing = raceRing;
                raceRing.PreviousRing = m_RaceRings[count - 1];
            }

            m_RaceRings.Add(raceRing);

            count++;
        }
    }
}
