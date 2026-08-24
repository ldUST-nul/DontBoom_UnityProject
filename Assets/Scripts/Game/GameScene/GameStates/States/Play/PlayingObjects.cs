using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class StandbyControl
{
    internal StandbyObjects m_standbyObjects;

    internal StandbyControl(StandbyObjects standbyobjs)
    {
        m_standbyObjects = standbyobjs;
    }

    internal void Initialize()
    {
        m_standbyObjects.Initialize();
    }
    internal void CanvasAlpha(float alpha)
    {
        m_standbyObjects.m_standbyCanvasGroup.alpha = alpha;
    }

    internal void Show()
    {
        m_standbyObjects.m_standbyCanva.gameObject.SetActive(true);
    }
    internal void Hide()
    {
        m_standbyObjects.m_standbyCanva.gameObject.SetActive(false);
    }

    internal void ShowAnimation()
    {
        m_standbyObjects.m_standbyAnimator?.SetTrigger("standby");
    }
}

/// <summary>スタンバイ中のオブジェクト達</summary>
[Serializable]
public class StandbyObjects
{
    [SerializeField] internal GameObject m_standbyCanva;
    [SerializeField] internal CanvasGroup m_standbyCanvasGroup;
    [SerializeField] internal Animator m_standbyAnimator;
    [SerializeField] internal UnityEngine.UI.Text m_clickStart;

    [SerializeField] internal float m_standbyTime;
    
    internal void Initialize()
    {
        m_standbyCanvasGroup.alpha = 0f;
        m_standbyCanva.gameObject.SetActive(true);
    }
}

/// <summary>ゲームプレイ中に使用するオブジェクト参照</summary>
[Serializable]
public class PlayingObjects
{
    [SerializeField] internal BallManager ballManager;
    [SerializeField] internal List<StageController> stageControl;
    [SerializeField] internal List<LevelGoal> goalControl;
    [SerializeField] internal GameUIController uiControl;

    internal void Initialize(GameSettings gameData)
    {
        foreach (var stage in stageControl)
        {
            stage.Initialize(gameData);
        }

        foreach(var ballGene in ballManager.ballGenerator)
        {
            ballGene.Initialize(gameData);
        }
        uiControl.Initialize();
    }
}

[Serializable]
internal class ResultObjects
{
    [SerializeField] internal ResultUIController resultUIControl;
    internal void Initialize()
    {
        resultUIControl.Initialize();
    }
}
