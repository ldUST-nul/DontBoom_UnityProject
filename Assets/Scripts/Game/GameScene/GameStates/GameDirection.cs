using System;
using UnityEngine;

[Serializable]
public class StageLightController
{
    [SerializeField] private Light[] lights;
    [SerializeField] private Animator m_animator;

    internal void Initialize(GameSettings gameData)
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].color = gameData.lightSettings.lightsColor;
        }

        StageLightsFlash();
    }
    internal void ChangeColor(Color color)
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].color = color;
        }
    }
    /// <summary>ライトをつける演出</summary>
    internal void StageLightsFlash()
    {
        m_animator.SetTrigger("Flash");
    }

    /// <summary>ライトを消す演出</summary>
    internal void TurnOffTheLight()
    {
        m_animator.SetTrigger("TurnOff");
    }
}