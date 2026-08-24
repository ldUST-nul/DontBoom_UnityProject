using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class TitleLightFlash : MonoBehaviour
{
    [SerializeField] List<TitleLights> m_lights;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var light in m_lights)
        {
            light.Lighting();
        }
    }
}

[Serializable]
class TitleLights
{
    [SerializeField] Light[] m_light;
    [SerializeField] FlashMath m_flashMath;

    internal void Lighting()
    {
        foreach (var light in m_light)
        { 
            light.intensity = Math();
        }
    }

    float Math()
    {
        if (m_flashMath.m_currentValue < m_flashMath.m_target + m_flashMath.m_range && m_flashMath.m_currentValue > m_flashMath.m_target - m_flashMath.m_range)
        {
            return m_flashMath.m_target = UnityEngine.Random.Range(m_flashMath.m_max, -m_flashMath.m_min);
        }
        return m_flashMath.m_currentValue = Mathf.SmoothDamp(m_flashMath.m_currentValue, m_flashMath.m_target, ref m_flashMath.m_currentVelocity, m_flashMath.m_speed);
    }
}

[Serializable]
class FlashMath
{
    [SerializeField] internal float m_currentValue;
    [SerializeField] internal float m_currentVelocity;
    [SerializeField] internal float m_target;
    [SerializeField] internal float m_speed;

    [SerializeField] internal float m_range;
    //[SerializeField] internal float m_randomRange;

    [SerializeField] internal float m_max;
    [SerializeField] internal float m_min;

    internal float m_random => UnityEngine.Random.Range(m_max, m_min);
}
