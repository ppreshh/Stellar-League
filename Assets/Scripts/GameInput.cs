using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerControls m_PlayerControls;

    public Vector2 PitchYawValue { get { return m_PlayerControls.ActionMap.LeftJoystick.ReadValue<Vector2>(); } }
    public Vector2 CameraMoveValue { get { return m_PlayerControls.ActionMap.RightJoystick.ReadValue<Vector2>(); } }
    public bool IsRolling { get { return m_PlayerControls.ActionMap.LeftShoulder.ReadValue<float>() == 1f; } }
    public float BoosterValue { get { return m_PlayerControls.ActionMap.RighTrigger.ReadValue<float>(); } }
    public float ReverseValue { get { return m_PlayerControls.ActionMap.LeftTrigger.ReadValue<float>(); } }

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
        m_PlayerControls.ActionMap.EastButton.performed += EastButton_performed;
        m_PlayerControls.ActionMap.SouthButton.performed += SouthButton_performed;
        m_PlayerControls.ActionMap.NorthButton.performed += NorthButton_performed;
        m_PlayerControls.ActionMap.RightStickPress.performed += RightStickPress_performed;
    }    

    private void OnDisable()
    {
        m_PlayerControls.Disable();
    }

    private void OnDestroy()
    {
        m_PlayerControls.ActionMap.EastButton.performed -= EastButton_performed;
        m_PlayerControls.ActionMap.SouthButton.performed -= SouthButton_performed;
        m_PlayerControls.ActionMap.NorthButton.performed -= NorthButton_performed;
        m_PlayerControls.ActionMap.RightStickPress.performed -= RightStickPress_performed;
    }

    private void EastButton_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnHyperSpeedPerformed?.Invoke();
    }

    private void SouthButton_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnBurstPerformed?.Invoke();
    }

    private void NorthButton_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCameraLockToggle?.Invoke();
    }

    private void RightStickPress_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCameraYOffsetToggle?.Invoke();
    }
}
