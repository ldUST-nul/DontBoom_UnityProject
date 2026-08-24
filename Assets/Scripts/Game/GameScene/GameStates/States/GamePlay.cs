using System;
using UnityEngine;

/// <summary>ゲームプレイ中</summary>
public class GamePlay : GameStates
{
    PlayingObjects gameObjects;
    StageDirection direction;
    GameStatus gameStatus;

    internal event Action onTimeUp;
    internal event Action m_onPlayEnter;

    private bool m_timeUpHandled;   // 時間切れ処理を1度だけにするためのフラグ

    private AudioSource m_audioPlayer;
    private AudioClip m_playBGM;

    internal GamePlay(GameContext context)
    {
        gameObjects = context.gamePlayObjects;
        direction = context.stageDirection;
        gameStatus = context.status;

        m_audioPlayer = context.audioData.m_audioPlayer;
        m_playBGM = context.audioData.m_playBGM;
    }

    public override void OnInitialize()
    {
        m_timeUpHandled = false;
    }

    public override void OnEnter()
    {
        OnInitialize();
        gameObjects.ballManager.AllWakeUp();
        gameObjects.uiControl.gameUI.Show();

        m_audioPlayer.clip = m_playBGM;
        m_audioPlayer?.Play();

        m_onPlayEnter?.Invoke();
    }

    public override void OnUpdate()
    {
        var ballManager = gameObjects.ballManager;

        // カメラ：全ボールを画角に収める処理
        direction.cameraControll.CameraMoving(ballManager.AllBalls());

        // 時間を進める処理
        gameStatus.ReduceTime(Time.deltaTime);

        // ステージ操作の処理
        foreach (var stage in gameObjects.stageControl) stage.Movement();

        // UI更新
        gameObjects.uiControl.gameUI.UpdateTexts(
            gameStatus.GameTimer, gameStatus.GameScore);

        // 時間切れ：全爆破＋通知を1度のみ
        if (gameStatus.IsTimeUp() && !m_timeUpHandled)
        {
            m_timeUpHandled = true;
            ballManager.AllDeath(); // 見た目だけ全爆破（Deathは通知しない）
            onTimeUp?.Invoke(); // ゲームオーバー通知は1回
        }
    }
    public override void OnExit()
    {
        gameObjects.ballManager.AllSleep();
        gameObjects.uiControl.gameUI.Hide();

        m_audioPlayer?.Stop();
    }
}
