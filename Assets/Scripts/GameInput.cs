using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerControls m_PlayerControls;

    public Vector2 PitchYawValue { get { return m_PlayerControls.ActionMap.Movement.ReadValue<Vector2>(); } }
    public Vector2 CameraMoveValue { get { return m_PlayerControls.ActionMap.RightJoystick.ReadValue<Vector2>(); } }
    public bool IsRolling { get { return m_PlayerControls.ActionMap.AirRollToggle.ReadValue<float>() == 1f; } }
    public float BoosterValue { get { return m_PlayerControls.ActionMap.Booster.ReadValue<float>(); } }
    public float ReverseValue { get { return m_PlayerControls.ActionMap.Reverse.ReadValue<float>(); } }

    public event Action OnHyperSpeedPerformed;
    public event Action OnBurstPerformed;
    public event Action OnCameraLockToggle;
    public event Action OnCameraYOffsetToggle;

    public static GameInput Instance { private set; get; }

    private void Awake()
    {
        Instance = this;

        m_PlayerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        m_PlayerControls.Enable();
    }

    private void Start()
    {
        m_PlayerControls.ActionMap.Hyperspeed.performed += Hyperspeed_performed;
        m_PlayerControls.ActionMap.Burst.performed += Burst_performed;
        m_PlayerControls.ActionMap.CameraModeToggle.performed += CameraModeToggle_performed;
        m_PlayerControls.ActionMap.CameraYOffsetToggle.performed += CameraYOffsetToggle_performed;
    }    

    private void OnDisable()
    {
        m_PlayerControls.Disable();
    }

    private void OnDestroy()
    {
        m_PlayerControls.ActionMap.Hyperspeed.performed -= Hyperspeed_performed;
        m_PlayerControls.ActionMap.Burst.performed -= Burst_performed;
        m_PlayerControls.ActionMap.CameraModeToggle.performed -= CameraModeToggle_performed;
        m_PlayerControls.ActionMap.CameraYOffsetToggle.performed -= CameraYOffsetToggle_performed;
    }

    private void Hyperspeed_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnHyperSpeedPerformed?.Invoke();
    }

    private void Burst_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnBurstPerformed?.Invoke();
    }

    private void CameraModeToggle_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCameraLockToggle?.Invoke();
    }

    private void CameraYOffsetToggle_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCameraYOffsetToggle?.Invoke();
    }
}
