using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FlightController m_FlightController = null;

    [Header("Parameters")]
    [SerializeField] private float m_ShakeIntensity = 0f;
    [SerializeField] private float m_ShakeSpeed = 1f;

    private bool m_IsOn = false;
    public bool IsOn
    {
        set
        {
            if (!value) transform.localPosition = m_InitialPosition;
            m_IsOn = value;
        }
    }

    private Vector3 m_InitialPosition;

    private void Start()
    {
        m_InitialPosition = transform.localPosition;
    }

    private void Update()
    {
        if (m_IsOn)
        {
            // Generate random offsets using perlin noise
            float offsetX = Mathf.PerlinNoise(Time.time * m_ShakeSpeed, 0f) * 2f - 1f;
            float offsetY = Mathf.PerlinNoise(0f, Time.time * m_ShakeSpeed) * 2f - 1f;

            // Calculate the new position with added offsets
            Vector3 newPosition = m_InitialPosition + new Vector3(offsetX, offsetY, 0f) * m_ShakeIntensity;

            // Apply the new position to the camera
            transform.localPosition = newPosition;
        }

        IsOn = m_FlightController.IsHyperSpeedActivated;
    }
}
