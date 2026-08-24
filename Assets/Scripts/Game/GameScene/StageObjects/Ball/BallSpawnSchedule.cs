using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
internal class BallSpawnWave
{
    [SerializeField] internal List<BallSpawnEntry> entries;
    [SerializeField] internal int m_wave;
}

[Serializable]
internal class BallSpawnEntry
{
    [SerializeField] internal Ball.BallKind kind;
    [SerializeField] internal int generatorIndex; // ジェネの種類
    [SerializeField] internal int positionIndex; // BallGeneratorのm_generatePositionsの番号
    [SerializeField] internal int count; // 生成数
}

[CreateAssetMenu(menuName = "SceneData/BallSpawnSchedule")]
internal class BallSpawnSchedule : ScriptableObject
{
    [SerializeField] internal List<BallSpawnWave> waveEntries = new();

    internal int TotalWaveCount => waveEntries.Count;
}
