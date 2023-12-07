using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsHUDPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image m_BoosterFillImage = null;
    [SerializeField] private Image m_ChargeUpFillImage = null;
    [SerializeField] private Image m_BurstCooldownFillImage = null;
    [SerializeField] private Image m_HyperSpeedIndicatorImage = null;

    [Header("Parameters")]
    [SerializeField] private Color m_HyperSpeedAvailableColor;
    [SerializeField] private Color m_HyperSpeedActivatedColor;

    private FlightController m_FlightController = null;
    private float m_BurstCooldownFillAmount = 1f;
    private Coroutine m_RampUpBurstFillCoroutine = null;

    private void Awake()
    {
        m_FlightController = FindObjectOfType<FlightController>();

        m_FlightController.OnDirectionalBurst += OnDirectionalBurst;
    }

    private void Update()
    {
        m_BoosterFillImage.fillAmount = GameInput.Instance.BoosterValue;
        m_ChargeUpFillImage.fillAmount = m_FlightController.ChargeUpIntensity;
        m_BurstCooldownFillImage.fillAmount = m_BurstCooldownFillAmount;

        m_HyperSpeedIndicatorImage.gameObject.SetActive(m_FlightController.ChargeUpIntensity >= 1f || m_FlightController.IsHyperSpeedActivated);
        m_HyperSpeedIndicatorImage.color = m_FlightController.IsHyperSpeedActivated ? m_HyperSpeedActivatedColor : m_HyperSpeedAvailableColor;
    }

    private void OnDestroy()
    {
        m_FlightController.OnDirectionalBurst -= OnDirectionalBurst;
    }

    private void OnDirectionalBurst(int burstCooldown)
    {
        m_BurstCooldownFillAmount = 0f;

        if (m_RampUpBurstFillCoroutine != null)
        {
            StopCoroutine(m_RampUpBurstFillCoroutine);
        }

        m_RampUpBurstFillCoroutine = StartCoroutine(RampUpBurstFill(burstCooldown));
    }

    private IEnumerator RampUpBurstFill(int seconds)
    {
        float count = seconds;
        while (count > 0)
        {
            yield return new WaitForSeconds(0.01f);
            count -= 0.01f;
            m_BurstCooldownFillAmount += 0.01f;
        }

        m_BurstCooldownFillAmount = 1f;

        m_RampUpBurstFillCoroutine = null;
    }
}
