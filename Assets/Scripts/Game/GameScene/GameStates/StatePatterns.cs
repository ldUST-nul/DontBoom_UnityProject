using UnityEngine;

/// <summary>全ステートのインスタンスを管理するクラス</summary>
public class StatePatterns
{
    internal Standby standby;
    internal GamePlay playNow;
    internal GameClear gameClear;
    internal GameOver gameOver;
    internal GameResult gameResult;
    internal StatePatterns(GameContext context)
    {
        standby = new Standby(context);
        playNow = new GamePlay(context);
        gameClear = new GameClear(context);
        gameOver = new GameOver(context);
        gameResult = new GameResult(context);
    }
}