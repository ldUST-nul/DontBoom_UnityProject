using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static GameStatus;

/// <summary>
/// デバッグ・オーケストレーション用マネージャ。
/// 責務は厳密に分けず、ゲームの進行に関わる操作を集約する。
/// </summary>
public class GameManager_Tester : MonoBehaviour
{
    [SerializeField] GameContext context;
    [SerializeField] GameSettings gameData;
    [SerializeField] BallSpawnSchedule spawnSchedule;

    GameStateMachine statemachine;
    StatePatterns statePatterns;
    GameDebugger gameDebugger;

    int m_clearedCount;
    int m_nextSpawnIndex;
    int m_nextSpawnWave;
    int m_spawnedCount;

    void Awake()
    {
        if (gameData == null || spawnSchedule == null)
        {
            this.gameObject.SetActive(false);
            return;
        }

        statePatterns = new StatePatterns(context);
        SetSubscribeAction();
        statemachine  = new GameStateMachine(statePatterns.standby);
        gameDebugger  = new GameDebugger(statemachine, statePatterns);
    }

    void Update()
    {
        gameDebugger.OnUpdate();
    }

    // -------------------------------------------------------
    // 購読・ステート管理
    // -------------------------------------------------------

    void SetSubscribeAction()
    {
        // それぞれの状態ごとに必要なイベントを登録
        statePatterns.standby.onStandby += OnStandbyEnter;
        statePatterns.standby.onLeaveStandby += GamePlayStart;
        statePatterns.gameClear.m_stateChanged += OpenResult;
        statePatterns.gameOver.m_stateChanged += OpenResult;
        statePatterns.playNow.m_onPlayEnter += SpawnWaveNext;
        // ゲームオーバー時のイベント
        statePatterns.playNow.onTimeUp += GameOverEvent;

        var resultUI = context.gameResultObjects.resultUIControl.resultUI;
        resultUI.onRetry  += Retry;
        resultUI.onTitle  += GoBackTitle;

        // SceneChangeのイベント
        resultUI.onNextLevel += OnChangeScene;
    }

    void OnChangeScene()
    {
        SceneManager.LoadScene(context.gameResultObjects.resultUIControl.resultUI.m_nextSceneIndex);
    }

    void OnStandbyEnter()
    {
        m_clearedCount   = 0;
        m_nextSpawnIndex = 0;
        m_spawnedCount = 0;
        context.Initialize(gameData);
    }

    void OnOneBallCleared()
    {
        context.status.AddScore(gameData.ballSetting.m_score);
        m_clearedCount++;

        // 今出ているボールが「全部」ゴールしたか？
        bool waveCleared = m_clearedCount >= m_spawnedCount;
        if (!waveCleared) return;   // まだ場にボールが残っている → 次は出さず待つ

        // この波は片付いた → 次の波(entry)があれば生成、無ければクリア
        if (m_nextSpawnIndex < spawnSchedule.waveEntries.Count)
            SpawnWaveNext();            // 次のentry（波）を生成
        else
            GameClearEvent();       // 全部出し切った＆全部ゴール → クリア
    }

    void SpawnWaveNext()
    {
        if (m_nextSpawnIndex >= spawnSchedule.waveEntries.Count) return;

        var waveEntry = spawnSchedule.waveEntries[m_nextSpawnIndex];

        foreach (var entry in waveEntry.entries)
        {
            var gene = context.gamePlayObjects.ballManager.ballGenerator[entry.generatorIndex];
            for (int i = 0; i < entry.count; i++)
            {
                var ball = gene.Spawn(entry);
                ball.actionClear += OnOneBallCleared;
                ball.actionDeath += GameOverEvent;
                m_spawnedCount++;
            }
        }
        m_nextSpawnIndex++;
    }
    /// Old Spawner Func
    //void SpawnNext()
    //{
    //    if (m_nextSpawnIndex >= spawnSchedule.entries.Count) return;

    //    var entry = spawnSchedule.entries[m_nextSpawnIndex];
    //    var gene = context.gamePlayObjects.ballManager.ballGenerator[entry.generatorIndex];  // ★指定の1台

    //    for (int i = 0; i < entry.count; i++) // ★count個だけ出す
    //    {
    //        var ball = gene.Spawn(entry);
    //        ball.actionClear += OnOneBallCleared;
    //        ball.actionDeath += GameOverEvent;
    //        m_spawnedCount++;
    //    }

    //    m_nextSpawnIndex++;
    //}

    void GameStandby()
    {
        statemachine.TransitionTo(statePatterns.standby);
    }

    void GamePlayStart()
    {
        statemachine.TransitionTo(statePatterns.playNow);
    }

    void GameClearEvent()
    {
        context.stageDirection.cameraControll.SwitchToOverView();
        context.status.GameOutCome(GameOutcome.Clear);
        statemachine.TransitionTo(statePatterns.gameClear);
    }

    void GameOverEvent()
    {
        context.gamePlayObjects.ballManager.AllSleep();
        context.stageDirection.cameraControll.SwitchToOverView();
        context.status.GameOutCome(GameOutcome.Over);
        statemachine.TransitionTo(statePatterns.gameOver);
    }

    void OpenResult() => statemachine.TransitionTo(statePatterns.gameResult);
    void Retry() => GameStandby();

    /// <summary>仮置き</summary>
    void GoBackTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}

[Serializable]
public class GameDebugger
{
    GameStateMachine statemachine;
    StatePatterns statePatterns;

    internal GameDebugger(GameStateMachine machine, StatePatterns pattern)
    {
        statemachine  = machine;
        statePatterns = pattern;
    }

    internal void OnUpdate()
    {
        if (statemachine == null) return;

        //if (Keyboard.current.digit1Key.wasPressedThisFrame) statemachine.TransitionTo(statePatterns.standby);
        //else if (Keyboard.current.digit2Key.wasPressedThisFrame) statemachine.TransitionTo(statePatterns.playNow);

        statemachine.OnUpdate();
    }
}
