using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RaceLogger : MonoBehaviour
{
    [SerializeField] private Rigidbody m_Ship = null;

    public class RaceLogEntry
    {
        public Vector3 position;
        public Vector3 velocity;

        public RaceLogEntry(Vector3 position, Vector3 velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }
    }

    private List<RaceLogEntry> m_RaceLog = new List<RaceLogEntry>();

    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        StartCoroutine(Log(60));
    }

    private IEnumerator Log(int raceLength)
    {
        int count = raceLength;
        while (count > 0)
        {
            yield return new WaitForSeconds(1f);

            m_RaceLog.Add(new RaceLogEntry(m_Ship.gameObject.transform.position, m_Ship.velocity));

            count --;
        }

        StreamWriter writer = new StreamWriter(Application.persistentDataPath + "\\RaceLog.csv");

        foreach(RaceLogEntry entry in m_RaceLog)
        {
            writer.WriteLine(entry.position.ToString() + "," + entry.velocity.ToString());
        }

        writer.Flush();
        writer.Close();

        Debug.Log("DONE");
    }
}
