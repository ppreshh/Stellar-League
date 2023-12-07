using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraHUDPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image m_LockIconImage = null;
    [SerializeField] private Image m_UnlockIconImage = null;

    private CameraController m_CameraController = null;

    private void Awake()
    {
        m_CameraController = FindObjectOfType<CameraController>();

        m_CameraController.OnCameraModeChanged += OnCameraModeChanged;
    }

    private void OnDestroy()
    {
        m_CameraController.OnCameraModeChanged -= OnCameraModeChanged;
    }

    private void OnCameraModeChanged(CameraController.CameraMode cameraMode)
    {
        UpdateVisuals(cameraMode);
    }

    private void UpdateVisuals(CameraController.CameraMode cameraMode)
    {
        m_LockIconImage.gameObject.SetActive(cameraMode == CameraController.CameraMode.LOCK);
        m_UnlockIconImage.gameObject.SetActive(cameraMode == CameraController.CameraMode.FORWARD);
    }
}
