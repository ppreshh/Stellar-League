using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySelector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject m_DemoBody = null;
    [SerializeField] private GameObject m_RociBody = null;
    [SerializeField] private GameObject m_WingsBody = null;

    [Header("Parameters")]
    [SerializeField] private Body m_CurrentBody = Body.WINGS;
    public Body CurrentBody { 
        set
        {
            m_CurrentBody = value;
            UpdateBodyVisuals();
        }
    }

    public enum Body
    {
        DEMO, ROCI, WINGS
    }

    private void Awake()
    {
        CurrentBody = m_CurrentBody;
    }

    private void UpdateBodyVisuals()
    {
        m_DemoBody.SetActive(m_CurrentBody == Body.DEMO);
        m_RociBody.SetActive(m_CurrentBody == Body.ROCI);
        m_WingsBody.SetActive(m_CurrentBody == Body.WINGS);
    }
}
