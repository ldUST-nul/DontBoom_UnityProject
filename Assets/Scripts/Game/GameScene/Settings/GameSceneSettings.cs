using NUnit.Framework.Internal;
using System;
using UnityEngine;

[Serializable]
internal class CameraSettings
{
    [SerializeField] internal Vector3 m_centerOffset;
}

[Serializable]
internal class StageSettings
{

    /// ------------------------------------------------------------------------------------
    /// おそらく不要になるクラスである。
    /// ------------------------------------------------------------------------------------
    //[SerializeField] internal float m_rotePower;
    //[SerializeField] internal float m_roteLimit;
    //[SerializeField] internal float m_timescaleSpeed;
}
[Serializable]
internal class StageLightsSettings
{
    [SerializeField] internal Color lightsColor;
    [SerializeField] internal float lightsIntensity;
}

[Serializable]
internal class BallSettings
{
    [SerializeField] internal int m_score;
    [SerializeField] internal int m_maxHP;
    [SerializeField] internal float m_speedDamageThreshold;

    [SerializeField] internal AudioClip m_deathSound;
}