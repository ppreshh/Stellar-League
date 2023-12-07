using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using System;

public class ThrusterParticleSystems : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FlightController m_FlightController = null;
    [SerializeField] private GameObject m_ReverseThruster1 = null;
    [SerializeField] private GameObject m_ReverseThruster2 = null;
    [SerializeField] private GameObject m_ReverseThruster3 = null;
    [SerializeField] private ParticleSystem m_PitchUpThrusterUpper = null;
    [SerializeField] private ParticleSystem m_PitchUpThrusterLower = null;
    [SerializeField] private ParticleSystem m_PitchDownThrusterUpper = null;
    [SerializeField] private ParticleSystem m_PitchDownThrusterLower = null;
    [SerializeField] private ParticleSystem m_YawRightThrusterUpper = null;
    [SerializeField] private ParticleSystem m_YawRightThrusterLower = null;
    [SerializeField] private ParticleSystem m_YawLeftThrusterUpper = null;
    [SerializeField] private ParticleSystem m_YawLeftThrusterLower = null;
    [SerializeField] private ParticleSystem m_RollRightThrusterUpper = null;
    [SerializeField] private ParticleSystem m_RollRightThrusterLower = null;
    [SerializeField] private ParticleSystem m_RollLeftThrusterUpper = null;
    [SerializeField] private ParticleSystem m_RollLeftThrusterLower = null;
    [SerializeField] private ParticleSystem m_StrafeUpThrusterUpper = null;
    [SerializeField] private ParticleSystem m_StrafeUpThrusterLower = null;
    [SerializeField] private ParticleSystem m_StrafeDownThrusterUpper = null;
    [SerializeField] private ParticleSystem m_StrafeDownThrusterLower = null;
    [SerializeField] private ParticleSystem m_StrafeRightThrusterUpper = null;
    [SerializeField] private ParticleSystem m_StrafeRightThrusterLower = null;
    [SerializeField] private ParticleSystem m_StrafeLeftThrusterUpper = null;
    [SerializeField] private ParticleSystem m_StrafeLeftThrusterLower = null;

    [Header("Parameters")]
    [SerializeField] private float m_ThrusterVisualFactor = 15f;

    private EventInstance m_ThrusterEventInstance1;
    private EventInstance m_ThrusterEventInstance2;
    private EventInstance m_ThrusterEventInstance3;
    private EventInstance m_ThrusterEventInstance4;
    private EventInstance m_ThrusterEventInstance5;
    private EventInstance m_ThrusterEventInstance6;

    private void Start()
    {
        m_ThrusterEventInstance1 = AudioManager.instance.CreateEventInstance(FMODEvents.instance.Thruster);
        m_ThrusterEventInstance2 = AudioManager.instance.CreateEventInstance(FMODEvents.instance.Thruster);
        m_ThrusterEventInstance3 = AudioManager.instance.CreateEventInstance(FMODEvents.instance.Thruster);
        m_ThrusterEventInstance4 = AudioManager.instance.CreateEventInstance(FMODEvents.instance.Thruster);
        m_ThrusterEventInstance5 = AudioManager.instance.CreateEventInstance(FMODEvents.instance.Thruster);
        m_ThrusterEventInstance6 = AudioManager.instance.CreateEventInstance(FMODEvents.instance.Thruster);
    }

    private void Update()
    {
        UpdateThrusterVisuals();
        UpdateSound();
    }

    private void UpdateSound()
    {
        AudioManager.instance.PlayLoop(m_FlightController.IsPitchingUp || m_FlightController.IsPitchingDown, m_ThrusterEventInstance1);
        m_ThrusterEventInstance1.setParameterByName("thruster_intensity", Mathf.Abs(GameInput.Instance.PitchYawValue.y));
        m_ThrusterEventInstance1.setParameterByName("thruster_loudness", m_FlightController.IsHyperSpeedActivated ? 1f : 0f);

        AudioManager.instance.PlayLoop(m_FlightController.IsYawingRight || m_FlightController.IsYawingLeft, m_ThrusterEventInstance2);
        m_ThrusterEventInstance2.setParameterByName("thruster_intensity", Mathf.Abs(GameInput.Instance.PitchYawValue.x));
        m_ThrusterEventInstance2.setParameterByName("thruster_loudness", m_FlightController.IsHyperSpeedActivated ? 1f : 0f);

        AudioManager.instance.PlayLoop(m_FlightController.IsRollingRight || m_FlightController.IsRollingLeft, m_ThrusterEventInstance3);
        m_ThrusterEventInstance3.setParameterByName("thruster_intensity", Mathf.Abs(GameInput.Instance.PitchYawValue.x));
        m_ThrusterEventInstance3.setParameterByName("thruster_loudness", m_FlightController.IsHyperSpeedActivated ? 1f : 0f);
        
        AudioManager.instance.PlayLoop(m_FlightController.IsStrafingLeft || m_FlightController.IsStrafingRight, m_ThrusterEventInstance4);
        m_ThrusterEventInstance4.setParameterByName("thruster_intensity", Mathf.Abs(GameInput.Instance.PitchYawValue.x));
        m_ThrusterEventInstance4.setParameterByName("thruster_loudness", m_FlightController.IsHyperSpeedActivated ? 1f : 0f);

        AudioManager.instance.PlayLoop(m_FlightController.IsStrafingUp || m_FlightController.IsStrafingDown, m_ThrusterEventInstance5);
        m_ThrusterEventInstance5.setParameterByName("thruster_intensity", Mathf.Abs(GameInput.Instance.PitchYawValue.y));
        m_ThrusterEventInstance5.setParameterByName("thruster_loudness", m_FlightController.IsHyperSpeedActivated ? 1f : 0f);

        AudioManager.instance.PlayLoop(m_FlightController.IsReversing, m_ThrusterEventInstance6);
        m_ThrusterEventInstance6.setParameterByName("thruster_intensity", GameInput.Instance.ReverseValue);
    }

    private void UpdateThrusterVisuals()
    {
        m_PitchUpThrusterUpper.gameObject.SetActive(m_FlightController.IsPitchingUp);
        m_PitchUpThrusterLower.gameObject.SetActive(m_FlightController.IsPitchingUp);

        m_PitchDownThrusterUpper.gameObject.SetActive(m_FlightController.IsPitchingDown);
        m_PitchDownThrusterLower.gameObject.SetActive(m_FlightController.IsPitchingDown);

        m_YawRightThrusterUpper.gameObject.SetActive(m_FlightController.IsYawingRight);
        m_YawRightThrusterLower.gameObject.SetActive(m_FlightController.IsYawingRight);

        m_YawLeftThrusterUpper.gameObject.SetActive(m_FlightController.IsYawingLeft);
        m_YawLeftThrusterLower.gameObject.SetActive(m_FlightController.IsYawingLeft);

        m_RollRightThrusterUpper.gameObject.SetActive(m_FlightController.IsRollingRight);
        m_RollRightThrusterLower.gameObject.SetActive(m_FlightController.IsRollingRight);

        m_RollLeftThrusterUpper.gameObject.SetActive(m_FlightController.IsRollingLeft);
        m_RollLeftThrusterLower.gameObject.SetActive(m_FlightController.IsRollingLeft);

        m_StrafeUpThrusterUpper.gameObject.SetActive(m_FlightController.IsStrafingUp);
        m_StrafeUpThrusterLower.gameObject.SetActive(m_FlightController.IsStrafingUp);

        m_StrafeDownThrusterUpper.gameObject.SetActive(m_FlightController.IsStrafingDown);
        m_StrafeDownThrusterLower.gameObject.SetActive(m_FlightController.IsStrafingDown);

        m_StrafeRightThrusterUpper.gameObject.SetActive(m_FlightController.IsStrafingRight);
        m_StrafeRightThrusterLower.gameObject.SetActive(m_FlightController.IsStrafingRight);

        m_StrafeLeftThrusterUpper.gameObject.SetActive(m_FlightController.IsStrafingLeft);
        m_StrafeLeftThrusterLower.gameObject.SetActive(m_FlightController.IsStrafingLeft);

        m_ReverseThruster1.SetActive(m_FlightController.IsReversing);
        m_ReverseThruster2.SetActive(m_FlightController.IsReversing);
        if (m_ReverseThruster3 != null)
        {
            m_ReverseThruster3.SetActive(m_FlightController.IsReversing);
        }

        var pitchUpUpperMain = m_PitchUpThrusterUpper.main;
        pitchUpUpperMain.startSpeed = Mathf.Abs(GameInput.Instance.PitchYawValue.y) * m_ThrusterVisualFactor;
        var pitchUpLowerMain = m_PitchUpThrusterLower.main;
        pitchUpLowerMain.startSpeed = Mathf.Abs(GameInput.Instance.PitchYawValue.y) * m_ThrusterVisualFactor;

        var pitchDownUpperMain = m_PitchDownThrusterUpper.main;
        pitchDownUpperMain.startSpeed = GameInput.Instance.PitchYawValue.y * m_ThrusterVisualFactor;
        var pitchDownLowerMain = m_PitchDownThrusterLower.main;
        pitchDownLowerMain.startSpeed = GameInput.Instance.PitchYawValue.y * m_ThrusterVisualFactor;

        var yawRightUpperMain = m_YawRightThrusterUpper.main;
        yawRightUpperMain.startSpeed = GameInput.Instance.PitchYawValue.x * m_ThrusterVisualFactor;
        var yawRightLowerMain = m_YawRightThrusterLower.main;
        yawRightLowerMain.startSpeed = GameInput.Instance.PitchYawValue.x * m_ThrusterVisualFactor;

        var yawLeftUpperMain = m_YawLeftThrusterUpper.main;
        yawLeftUpperMain.startSpeed = Mathf.Abs(GameInput.Instance.PitchYawValue.x) * m_ThrusterVisualFactor;
        var yawLeftLowerMain = m_YawLeftThrusterLower.main;
        yawLeftLowerMain.startSpeed = Mathf.Abs(GameInput.Instance.PitchYawValue.x) * m_ThrusterVisualFactor;

        var rollRightUpperMain = m_RollRightThrusterUpper.main;
        rollRightUpperMain.startSpeed = GameInput.Instance.PitchYawValue.x * m_ThrusterVisualFactor;
        var rollRightLowerMain = m_RollRightThrusterLower.main;
        rollRightLowerMain.startSpeed = GameInput.Instance.PitchYawValue.x * m_ThrusterVisualFactor;

        var rollLeftUpperMain = m_RollLeftThrusterUpper.main;
        rollLeftUpperMain.startSpeed = Mathf.Abs(GameInput.Instance.PitchYawValue.x) * m_ThrusterVisualFactor;
        var rollLeftLowerMain = m_RollLeftThrusterLower.main;
        rollLeftLowerMain.startSpeed = Mathf.Abs(GameInput.Instance.PitchYawValue.x) * m_ThrusterVisualFactor;

        var strafeUpUpperMain = m_StrafeUpThrusterUpper.main;
        strafeUpUpperMain.startSpeed = GameInput.Instance.PitchYawValue.y * m_ThrusterVisualFactor;
        var strafeUpLowerMain = m_StrafeUpThrusterLower.main;
        strafeUpLowerMain.startSpeed = GameInput.Instance.PitchYawValue.y * m_ThrusterVisualFactor;

        var strafeDownUpperMain = m_StrafeDownThrusterUpper.main;
        strafeDownUpperMain.startSpeed = Mathf.Abs(GameInput.Instance.PitchYawValue.y) * m_ThrusterVisualFactor;
        var strafeDownLowerMain = m_StrafeDownThrusterLower.main;
        strafeDownLowerMain.startSpeed = Mathf.Abs(GameInput.Instance.PitchYawValue.y) * m_ThrusterVisualFactor;

        var strafeRightUpperMain = m_StrafeRightThrusterUpper.main;
        strafeRightUpperMain.startSpeed = GameInput.Instance.PitchYawValue.x * m_ThrusterVisualFactor;
        var strafeRightLowerMain = m_StrafeRightThrusterLower.main;
        strafeRightLowerMain.startSpeed = GameInput.Instance.PitchYawValue.x * m_ThrusterVisualFactor;

        var strafeLeftUpperMain = m_StrafeLeftThrusterUpper.main;
        strafeLeftUpperMain.startSpeed = Mathf.Abs(GameInput.Instance.PitchYawValue.x) * m_ThrusterVisualFactor;
        var strafeLeftLowerMain = m_StrafeLeftThrusterLower.main;
        strafeLeftLowerMain.startSpeed = Mathf.Abs(GameInput.Instance.PitchYawValue.x) * m_ThrusterVisualFactor;
    }
}
