using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_ParticleSystem = null;
    [SerializeField] private FlightController m_FlightController = null;

    [Header("Extra")]
    [SerializeField] private List<ParticleSystem> m_HyperSpeedBoosters = null;

    private void Update()
    {
        var emission = m_ParticleSystem.emission;
        emission.enabled = (GameInput.Instance.BoosterValue > 0f || m_FlightController.IsHyperSpeedActivated) && !m_FlightController.IsHyperSpeedPreparing && !m_FlightController.IsStrafing;

        if (m_HyperSpeedBoosters != null)
        {
            foreach(ParticleSystem booster in m_HyperSpeedBoosters)
            {
                var boosterEmission = booster.emission;
                boosterEmission.enabled = m_FlightController.IsHyperSpeedActivated;
            }
        }
    }
}
