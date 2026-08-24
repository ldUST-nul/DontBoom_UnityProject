using System;
using UnityEngine;

/// <summary>ゲームが動作するのに必要な参照・状態をまとめた文脈(コンテキスト)</summary>
[Serializable]
internal class GameContext
{
    [SerializeField] internal StandbyObjects gameStandbyObjects;
    [SerializeField] internal PlayingObjects gamePlayObjects;
    [SerializeField] internal ResultObjects gameResultObjects;

    [SerializeField] internal GameAudioData audioData;
    [SerializeField] internal StageDirection stageDirection;

    /// <summary>
    /// スコア・時間・ライフを保持する状態データ。
    /// 他がシーン内オブジェクトの参照なのに対し、これだけは「数値の状態」を持つ特別枠。
    /// </summary>
    [SerializeField] internal GameStatus status;

    internal void Initialize(GameSettings gameData)
    {
        gameStandbyObjects.Initialize();
        gamePlayObjects.Initialize(gameData);
        gameResultObjects.Initialize();
        stageDirection.Initialize(gameData);
        status.Initialize();
    }
}

[Serializable]
internal class StageDirection
{
    [SerializeField] internal CameraController cameraControll; // カメラ
    [SerializeField] internal StageLightController stageLightsControll; // ライト演出用データ

    internal void Initialize(GameSettings gameData)
    {
        cameraControll.Initialize(gameData);
        stageLightsControll.Initialize(gameData);
    }
}