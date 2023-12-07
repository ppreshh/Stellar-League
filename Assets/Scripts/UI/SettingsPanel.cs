using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private List<Toggle> m_Tabs = null;
    [SerializeField] private InputAction m_TabRightAction = null;
    [SerializeField] private InputAction m_TabLeftAction = null;

    [SerializeField] private int m_CurrentTabIndex = 0;

    private void Awake()
    {
        m_TabRightAction.performed += OnTabRightActionPerformed;
        m_TabLeftAction.performed += OnTabLeftActionPerformed;
    }

    private void OnEnable()
    {
        m_TabRightAction.Enable();
        m_TabLeftAction.Enable();
    }

    private void OnDisable()
    {
        m_TabRightAction.Disable();
        m_TabLeftAction.Disable();
    }

    private void OnDestroy()
    {
        m_TabRightAction.performed -= OnTabRightActionPerformed;
        m_TabLeftAction.performed -= OnTabLeftActionPerformed;
    }

    private void OnTabRightActionPerformed(InputAction.CallbackContext obj)
    {
        UpdateCurrentTab(m_CurrentTabIndex + 1);
    }

    private void OnTabLeftActionPerformed(InputAction.CallbackContext obj)
    {
        UpdateCurrentTab(m_CurrentTabIndex - 1);
    }

    private void UpdateCurrentTab(int index)
    {
        if (index >= 0 && index < m_Tabs.Count)
        {
            m_CurrentTabIndex = index;
            m_Tabs[m_CurrentTabIndex].isOn = true;
        }
    }
}
