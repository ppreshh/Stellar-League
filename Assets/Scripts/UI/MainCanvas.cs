using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCanvas : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject m_SettingsPanel = null;

    [Header("Controls")]
    [SerializeField] private InputAction m_ToggleSettingsMenuAction = null;

    private FlightController m_FlightController;

    private void Awake()
    {
        m_FlightController = FindObjectOfType<FlightController>();

        m_ToggleSettingsMenuAction.performed += OnToggleSettingsMenuActionPerformed;
    }

    private void OnEnable()
    {
        m_ToggleSettingsMenuAction.Enable();
    }

    private void OnDisable()
    {
        m_ToggleSettingsMenuAction.Disable();
    }

    private void OnToggleSettingsMenuActionPerformed(InputAction.CallbackContext obj)
    {
        m_SettingsPanel.SetActive(!m_SettingsPanel.activeInHierarchy);
        m_FlightController.enabled = !m_SettingsPanel.activeInHierarchy;
    }
}
