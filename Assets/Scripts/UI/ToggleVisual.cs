using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleVisual : MonoBehaviour
{
    [SerializeField] private Toggle m_Toggle = null;
    [SerializeField] private TextMeshProUGUI m_Text = null;

    private void Awake()
    {
        m_Toggle.onValueChanged.AddListener(UpdateVisual);

        UpdateVisual(m_Toggle.isOn);
    }

    private void OnDestroy()
    {
        m_Toggle.onValueChanged.RemoveAllListeners();
    }

    private void UpdateVisual(bool value)
    {
        m_Text.color = new Color(1f, 1f, 1f, value ? 1f : 0.25f);
    }
}
