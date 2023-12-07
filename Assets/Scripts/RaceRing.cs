using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceRing : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<MeshRenderer> m_RingRails = null;
    [SerializeField] private Transform m_LockOnPoint = null;
    public Transform LockOnPoint { get { return m_LockOnPoint; } }

    [Header("Parameters")]
    [SerializeField] private Color m_DefaultColor;
    [SerializeField] private Color m_HighlightColor;
    [SerializeField] private Type m_Type = Type.DEFAULT;

    private RaceRing m_PreviousRing = null;
    public RaceRing PreviousRing { get { return m_PreviousRing; } set { m_PreviousRing = value; } }

    private RaceRing m_NextRing = null;
    public RaceRing NextRing { get { return m_NextRing; } set { m_NextRing = value; } }

    public enum Type
    {
        FIRST, DEFAULT, FINAL
    }

    private bool m_IsCurrentRing = false;
    public bool IsCurrentRing { get { return m_IsCurrentRing; } 
        set 
        { 
            m_IsCurrentRing = value;
            UpdateVisual();
        } 
    }

    public delegate void Entered(Type type, Transform nextRingTransform);
    public event Entered OnEntered;
    private void RaiseOnEntered()
    {
        if (OnEntered != null && m_IsCurrentRing)
        {
            OnEntered(m_Type, m_Type != Type.FINAL ? m_NextRing.LockOnPoint : null);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.CheckpointComplete);
            IsCurrentRing = false;
            if (m_NextRing != null) m_NextRing.IsCurrentRing = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_Type == Type.FIRST)
        {
            RaiseOnEntered();
        }
        else if (m_IsCurrentRing)
        {
            RaiseOnEntered();
        }
        
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        foreach (MeshRenderer rail in m_RingRails)
        {
            rail.material.color = m_IsCurrentRing ? m_HighlightColor : m_DefaultColor;
        }
    }

    public void SetAsFirstRing()
    {
        m_Type = Type.FIRST;
        m_IsCurrentRing = true;
        UpdateVisual();
    }

    public void SetAsFinalRing()
    {
        m_Type = Type.FINAL;
    }
}
