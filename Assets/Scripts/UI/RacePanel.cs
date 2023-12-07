using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RacePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text = null;

    private RaceTrack m_RaceTrack = null;

    private void Awake()
    {
        m_RaceTrack = FindObjectOfType<RaceTrack>();

        if (m_RaceTrack != null)
        {
            m_RaceTrack.OnRaceFinished += OnRaceFinished;
        }
    }

    private void OnDestroy()
    {
        if (m_RaceTrack != null)
        {
            m_RaceTrack.OnRaceFinished -= OnRaceFinished;
        }
    }

    private void OnRaceFinished(string finishTime)
    {
        m_Text.text = "Race Completed in " + finishTime;
    }
}
