using System;
using UnityEngine;

/// <summary>ゲームステータス</summary>
/// <remarks>ゲームのスコアや時間等の情報</remarks>
[Serializable]
public class GameStatus
{
    internal enum GameOutcome { Clear, Over};
    internal GameOutcome Outcome { get; private set; }

    /// <summary>ゲームタイマー</summary>
    /// <remarks>内部タイマー(外部からの直接代入は不許可)</remarks>
    internal float GameTimer { get; private set; }

    /// <summary>時間制限</summary>
    [SerializeField] private float gameTimeLimit;
    /// <summary>プレイヤースコア</summary>
    internal int GameScore { get; private set; }

    /// <summary>経過時間</summary>
    /// <remarks>制限時間から時間を引いた、startからの経過秒数</remarks>
    internal float ElapsedTime => gameTimeLimit - GameTimer;

    internal void Initialize()
    {
        GameTimer = gameTimeLimit;
        GameScore = 0;
    }

    ///

    internal void GameOutCome(GameOutcome game)
    {
        Outcome = game;
    }

    /// <summary>タイマーを減らす</summary>
    /// <remarks>毎フレーム呼び起こし</remarks>
    /// <param name="time">経過時間(Time.deltaTime)を代入</param>
    internal void ReduceTime(float time)
    {
        if (GameTimer <= 0) return;
        GameTimer -= time;
        if (GameTimer < 0) GameTimer = 0;
    }
    internal bool IsTimeUp()
    {
        if (GameTimer <= 0) return true;
        return false;
    }

    /// <summary>時間追加</summary>
    /// <remarks>時間を追加</remarks>
    internal void AddTime(float add)
    {
        GameTimer += add;
    }

    /// <summary>タイマーリセット</summary>
    /// <remarks>時間をリセットする。</remarks>
    internal void ResetTimer()
    {
        GameTimer = gameTimeLimit;
    }

    /// <summary>スコア加算</summary>
    /// <param name="add">加算分</param>
    internal void AddScore(int add)
    {
        GameScore += add;
    }
    /// <summary>スコアリセット</summary>
    /// <remarks>スコア初期化</remarks>
    internal void ResetScore()
    {
        GameScore = 0;
    }
}
