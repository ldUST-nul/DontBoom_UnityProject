using System;
using TimeSystem;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>プレイ前の待機状態</summary>
public class Standby : GameStates
{
    private StandbyControl gameStandbyControl;
    private PlayingObjects gameObjects;
    private StageDirection direction;

    Timer m_timer;

    internal event Action onStandby;
    internal event Action onLeaveStandby;

    private AudioSource m_audioPlayer;
    private AudioClip m_standbyBGM;

    bool m_isCanClick;
    internal Standby(GameContext context)
    {
        // ロード時に使うデータをここで初期化
        gameStandbyControl = new StandbyControl(context.gameStandbyObjects);
        gameObjects = context.gamePlayObjects;
        direction = context.stageDirection;

        m_audioPlayer = context.audioData.m_audioPlayer;
        m_standbyBGM = context.audioData.m_standbyBGM;

        m_isCanClick = false;

        m_timer = new Timer(false);
    }

    public override void OnInitialize()
    {
        m_timer.Reset();
        m_isCanClick = false;
        onStandby?.Invoke();
    }
    public override void OnEnter()
    {
        OnInitialize();

        direction.stageLightsControll.StageLightsFlash();

        m_audioPlayer.clip = m_standbyBGM;
        m_audioPlayer?.Play();
    }
    public override void OnUpdate()
    {
        m_timer.Tick(Time.deltaTime);

        if (m_timer.TryFire(gameStandbyControl.m_standbyObjects.m_standbyTime))
        {
            UIShowUp();
            m_isCanClick = true;
        }
        else if (Mouse.current.leftButton.wasPressedThisFrame && m_isCanClick == true)
        {
            onLeaveStandby.Invoke();
        }
        //if (Keyboard.current.digit2Key.wasPressedThisFrame) onLeaveStandby.Invoke();
    }
    public override void OnExit()
    {
        gameStandbyControl.CanvasAlpha(0);
        gameStandbyControl.Hide();

        m_audioPlayer?.Stop();
    }
    // --------------------------------------------------------------------------------------------------

    private void UIShowUp()
    {
        gameStandbyControl.ShowAnimation();
        gameStandbyControl.CanvasAlpha(1);
        gameStandbyControl.Show();
    }
}