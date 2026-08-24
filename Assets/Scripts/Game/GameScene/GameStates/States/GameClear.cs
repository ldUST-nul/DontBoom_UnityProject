using System;
using UnityEngine;

/// <summary>ゲームクリア時</summary>
public class GameClear : GameStates
{
    StageDirection direction;

    internal event Action m_stateChanged;

    bool m_isOpenedResult;
    float m_timer;
    internal GameClear(GameContext context)
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

        Debug.Log("GameClear : OnEnter");

        direction.stageLightsControll.ChangeColor(Color.green);
    }

    public override void OnUpdate()
    {
        Debug.Log("GameClear : OnUpdate");

        direction.cameraControll.CameraMoving();

        if (m_isOpenedResult == false) OpenResult();
    }

    public override void OnExit()
    {
        Debug.Log("GameClear : OnExit");
    }

    /// <summary>リザルトを開く</summary>
    private void OpenResult()
    {
        m_timer += Time.deltaTime;
        if (m_isOpenedResult == false && m_timer >= 2f)
        {
            Debug.Log("OpenResult");
            m_stateChanged?.Invoke();
            m_isOpenedResult = true;
        }
    }
}