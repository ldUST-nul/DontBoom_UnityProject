using System;
using UnityEngine;

/// <summary>ゲームオーバー時</summary>
public class GameOver : GameStates
{
    StageDirection direction;

    internal event Action m_stateChanged;

    bool m_isOpenedResult;
    float m_timer;
    internal GameOver(GameContext context)
    {
        // ロード時に使うデータをここで初期化
        direction = context.stageDirection;
    }

    public override void OnInitialize()
    {
        m_isOpenedResult = false;
        m_timer = 0;
    }

    public override void OnEnter()
    {
        OnInitialize();
        direction.stageLightsControll.ChangeColor(Color.red);
        Debug.Log("GameOver : OnEnter");
    }

    public override void OnUpdate()
    {
        Debug.Log("GameOver : OnUpdate");

        direction.cameraControll.CameraMoving();

        if (m_isOpenedResult == false) OpenResult();
    }

    public override void OnExit()
    {
        direction.stageLightsControll.TurnOffTheLight();
        Debug.Log("GameOver : OnExit");
    }

    /// <summary>リザルトを開く。</summary>
    private void OpenResult()
    {
        m_timer += Time.deltaTime;
        if (m_isOpenedResult == false && m_timer >= 2f)
        {
            m_stateChanged?.Invoke();
            m_isOpenedResult = true;
        }
    }
}