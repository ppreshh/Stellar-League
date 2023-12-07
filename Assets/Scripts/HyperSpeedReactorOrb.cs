using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperSpeedReactorOrb : MonoBehaviour
{
    [SerializeField] private FlightController m_FlightController = null;
    [SerializeField] private GameObject m_Orb = null;
    [SerializeField] private GameObject m_Light = null;

    private void Awake()
    {
        m_FlightController.OnHyperSpeedStateChanged += OnHyperSpeedStateChanged;
    }

    private void OnDestroy()
    {
        m_FlightController.OnHyperSpeedStateChanged -= OnHyperSpeedStateChanged;
    }

    private void OnHyperSpeedStateChanged(FlightController.HyperSpeedTransition transition)
    {
        if (transition == FlightController.HyperSpeedTransition.DEFAULT_TO_PREPARING)
        {
            m_Orb.SetActive(true);
            m_Light.SetActive(true);
        }

        if (transition == FlightController.HyperSpeedTransition.PREPARING_TO_FAILING ||
            transition == FlightController.HyperSpeedTransition.PREPARING_TO_GOING)
        {
            m_Orb.SetActive(false);
        }

        if (transition == FlightController.HyperSpeedTransition.PREPARING_TO_FAILING || 
            transition == FlightController.HyperSpeedTransition.GOING_TO_STOPPING)
        {
            m_Light.SetActive(false);
        }
    }
}
