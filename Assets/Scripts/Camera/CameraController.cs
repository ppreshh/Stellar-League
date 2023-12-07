using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform m_TargetToFollow = null;
    [SerializeField] private FlightController m_FlightController = null;
    [SerializeField] private Transform m_TargetToLockOn = null;
    public Transform TargetToLockOn { set { m_TargetToLockOn = value; } }

    [Header("Parameters")]
    [SerializeField] private float m_SmoothSpeed = 5f;
    [SerializeField] private float m_RotationSpeed = 2.5f;
    [SerializeField] private float m_CameraMoveFactor = 90f;
    [SerializeField] private CameraMode m_CameraMode = CameraMode.FORWARD;

    [Header("Y Offset")]
    [SerializeField] private GameObject m_CameraYOffsetGameObject = null;
    [SerializeField] private Vector3 m_YOffsetDefaultPosition;
    [SerializeField] private Vector3 m_YOffsetDownwardOffsetPosition;
    [SerializeField] private AnimationCurve m_YOffsetCurve;
    [SerializeField] private float m_YOffsetSpeed = 2f;

    public enum CameraMode
    {
        FORWARD, LOCK
    }

    public delegate void CameraModeChanged(CameraMode cameraMode);
    public event CameraModeChanged OnCameraModeChanged;
    private void RaiseOnCameraModeChanged()
    {
        if (OnCameraModeChanged != null)
        {
            OnCameraModeChanged(m_CameraMode);
        }
    }
    
    private float m_SpeedFactor = 1f;
    private Coroutine m_RampSmoothingCoroutine = null;
    private bool m_CameraYOffsetToggle = false;
    private Coroutine m_CurveLerpCoroutine = null;

    private void Awake()
    {
        m_FlightController.OnHyperSpeedStateChanged += OnHyperSpeedStateChanged;
    }

    private void Start()
    {
        GameInput.Instance.OnCameraLockToggle += GameInput_OnCameraLockToggle;
        GameInput.Instance.OnCameraYOffsetToggle += GameInput_OnCameraYOffsetToggle;
    }

    private void FixedUpdate()
    {
        if (m_TargetToFollow != null)
        {
            // Calculate the desired position of the camera
            Vector3 desiredPosition = m_TargetToFollow.position;

            // Smoothly move the camera towards the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, m_SmoothSpeed * m_SpeedFactor * Time.deltaTime);

            // Calculate the direction of the target's forward momentum
            Vector3 targetForward = m_TargetToFollow.forward;
            targetForward.y = 0f;  // Ignore any vertical component

            switch (m_CameraMode)
            {
                case CameraMode.FORWARD:

                    if (targetForward != Vector3.zero)
                    {
                        // Calculate the rotation offset based on the input Vector2 values
                        Quaternion rotationOffset = Quaternion.Euler(GameInput.Instance.CameraMoveValue.y * m_CameraMoveFactor, GameInput.Instance.CameraMoveValue.x * m_CameraMoveFactor, 0f);

                        // Apply the rotation offset to the target's forward momentum
                        Quaternion targetRotation = Quaternion.LookRotation(targetForward) * rotationOffset;

                        // Rotate the camera to face the target's forward momentum with the rotation offset
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_RotationSpeed * m_SpeedFactor * Time.deltaTime);
                    }

                    break;

                case CameraMode.LOCK:

                    if (m_TargetToLockOn != null)
                    {
                        Quaternion rotationToFaceBall = Quaternion.LookRotation(m_TargetToLockOn.position - transform.position);
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToFaceBall, m_RotationSpeed * m_SpeedFactor * Time.deltaTime);
                    }
                    else
                    {
                        // freeze rotation lerping
                    }

                    break;

                default:

                    break;
            }
        }
    }

    private void OnDestroy()
    {
        m_FlightController.OnHyperSpeedStateChanged -= OnHyperSpeedStateChanged;

        GameInput.Instance.OnCameraLockToggle -= GameInput_OnCameraLockToggle;
        GameInput.Instance.OnCameraYOffsetToggle -= GameInput_OnCameraYOffsetToggle;

    }

    private void OnHyperSpeedStateChanged(FlightController.HyperSpeedTransition transition)
    {
        switch (transition)
        {
            case FlightController.HyperSpeedTransition.DEFAULT_TO_PREPARING:

                RampSmoothSpeedFactorTo(3f, 0.05f);
                break;

            case FlightController.HyperSpeedTransition.PREPARING_TO_GOING:

                break;

            case FlightController.HyperSpeedTransition.GOING_TO_STOPPING:

                RampSmoothSpeedFactorTo(1f, 0.01f);
                break;

            case FlightController.HyperSpeedTransition.PREPARING_TO_FAILING:

                RampSmoothSpeedFactorTo(1f, 0.01f);
                break;

            default:

                break;
        }
    }

    private void RampSmoothSpeedFactorTo (float endValue, float increment)
    {
        if (m_RampSmoothingCoroutine != null) StopCoroutine(m_RampSmoothingCoroutine);
        m_RampSmoothingCoroutine = StartCoroutine(RampSmoothSpeedFactorIEnumerator(endValue, increment));
    }

    private IEnumerator RampSmoothSpeedFactorIEnumerator (float endValue, float increment)
    {
        if (m_SpeedFactor > endValue)
        {
            while (m_SpeedFactor >= endValue)
            {
                yield return null;

                m_SpeedFactor -= increment;
            }
        }
        else if (m_SpeedFactor < endValue)
        {
            while (m_SpeedFactor <= endValue)
            {
                yield return null;

                m_SpeedFactor += increment;
            }
        }

        m_SpeedFactor = endValue;

        m_RampSmoothingCoroutine = null;
    }

    private void GameInput_OnCameraLockToggle()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.CameraModeToggle);

        if (m_CameraMode == CameraMode.LOCK)
        {
            m_CameraMode = CameraMode.FORWARD;
        }
        else
        {
            m_CameraMode = CameraMode.LOCK;
        }

        RaiseOnCameraModeChanged();
    }

    private void GameInput_OnCameraYOffsetToggle()
    {
        if (!m_CameraYOffsetToggle)
        {
            if (m_CurveLerpCoroutine != null)
            {
                StopCoroutine(m_CurveLerpCoroutine);
                m_CurveLerpCoroutine = null;
            }

            m_CurveLerpCoroutine = StartCoroutine(CurveLerp(m_YOffsetDownwardOffsetPosition));
            m_CameraYOffsetToggle = true;
        }
        else
        {
            if (m_CurveLerpCoroutine != null)
            {
                StopCoroutine(m_CurveLerpCoroutine);
                m_CurveLerpCoroutine = null;
            }

            m_CurveLerpCoroutine = StartCoroutine(CurveLerp(m_YOffsetDefaultPosition));
            m_CameraYOffsetToggle = false;
        }
    }

    private IEnumerator CurveLerp(Vector3 destination)
    {
        float timeElapsed = 0;
        while (timeElapsed < 1)
        {
            m_CameraYOffsetGameObject.transform.localPosition = Vector3.Lerp(m_CameraYOffsetGameObject.transform.localPosition, destination, m_YOffsetCurve.Evaluate(timeElapsed));
            timeElapsed += Time.deltaTime * m_YOffsetSpeed;

            yield return null;
        }

        m_CameraYOffsetGameObject.transform.localPosition = destination;
        m_CurveLerpCoroutine = null;
    }
}
