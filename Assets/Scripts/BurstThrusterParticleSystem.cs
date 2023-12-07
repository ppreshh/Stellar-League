using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstThrusterParticleSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FlightController m_FlightController = null;
    [SerializeField] private ParticleSystem m_PitchUpThrusterUpper = null;
    [SerializeField] private ParticleSystem m_PitchUpThrusterLower = null;
    [SerializeField] private ParticleSystem m_PitchDownThrusterUpper = null;
    [SerializeField] private ParticleSystem m_PitchDownThrusterLower = null;
    [SerializeField] private ParticleSystem m_RollRightThrusterUpper = null;
    [SerializeField] private ParticleSystem m_RollRightThrusterLower = null;
    [SerializeField] private ParticleSystem m_RollLeftThrusterUpper = null;
    [SerializeField] private ParticleSystem m_RollLeftThrusterLower = null;
    [SerializeField] private ParticleSystem m_JumpThrusterUpper = null;
    [SerializeField] private ParticleSystem m_JumpThrusterLower = null;

    [Header("Parameters")]
    [SerializeField] private float m_ThrusterVisualFactor = 0.5f;

    private void Update()
    {
        UpdateThrusterVisuals();
    }

    private void UpdateThrusterVisuals()
    {
        var pitchUpUpperMain = m_PitchUpThrusterUpper.main;
        pitchUpUpperMain.startLifetime = m_FlightController.IsPitchingUp && m_FlightController.IsBursting ? m_ThrusterVisualFactor : 0f;
        var pitchUpLowerMain = m_PitchUpThrusterLower.main;
        pitchUpLowerMain.startLifetime = m_FlightController.IsPitchingUp && m_FlightController.IsBursting ? m_ThrusterVisualFactor : 0f;

        var pitchDownUpperMain = m_PitchDownThrusterUpper.main;
        pitchDownUpperMain.startLifetime = m_FlightController.IsPitchingDown && m_FlightController.IsBursting ? m_ThrusterVisualFactor : 0f;
        var pitchDownLowerMain = m_PitchDownThrusterLower.main;
        pitchDownLowerMain.startLifetime = m_FlightController.IsPitchingDown && m_FlightController.IsBursting ? m_ThrusterVisualFactor : 0f;

        var rollRightUpperMain = m_RollRightThrusterUpper.main;
        rollRightUpperMain.startLifetime = m_FlightController.IsYawingRight && m_FlightController.IsBursting ? m_ThrusterVisualFactor : 0f;
        var rollRightLowerMain = m_RollRightThrusterLower.main;
        rollRightLowerMain.startLifetime = m_FlightController.IsYawingRight && m_FlightController.IsBursting ? m_ThrusterVisualFactor : 0f;

        var rollLeftUpperMain = m_RollLeftThrusterUpper.main;
        rollLeftUpperMain.startLifetime = m_FlightController.IsYawingLeft && m_FlightController.IsBursting ? m_ThrusterVisualFactor : 0f;
        var rollLeftLowerMain = m_RollLeftThrusterLower.main;
        rollLeftLowerMain.startLifetime = m_FlightController.IsYawingLeft && m_FlightController.IsBursting ? m_ThrusterVisualFactor : 0f;

        var jumpUpperMain = m_JumpThrusterUpper.main;
        jumpUpperMain.startLifetime = m_FlightController.IsJumping ? m_ThrusterVisualFactor : 0f;
        var jumpLowerMain = m_JumpThrusterLower.main;
        jumpLowerMain.startLifetime = m_FlightController.IsJumping ? m_ThrusterVisualFactor : 0f;
    }
}
