using UnityEngine;
using static GameStatus;

public class GameResult : GameStates
{
    ResultObjects resultObjs;
    StageDirection gameDirection;
    GameStatus status;

    private float m_timer;
    private bool m_isOpenedResult;

    internal GameResult(GameContext context)
    {
        resultObjs = context.gameResultObjects;
        gameDirection = context.stageDirection;
        status = context.status;
    }
    public override void OnInitialize()
    {
        m_isOpenedResult = false;
        m_timer = 0;
    }

    public override void OnEnter()
    {
        OnInitialize();

        resultObjs.resultUIControl.resultUI.ShutterDown();
        resultObjs.resultUIControl.resultUI.SetTexts(status.ElapsedTime, status.GameScore);

        if (status.Outcome == GameOutcome.Clear) resultObjs.resultUIControl.resultUI.ActiveNextButton(true);
        else resultObjs.resultUIControl.resultUI.ActiveNextButton(false);
    }
    public override void OnUpdate()
    {
        // ボタン判定などの処理を受け付ける。
    }
    public override void OnExit()
    {
        resultObjs.resultUIControl.resultUI.ShutterUp();

        resultObjs.resultUIControl.resultUI.OnExitResult();
    }
}