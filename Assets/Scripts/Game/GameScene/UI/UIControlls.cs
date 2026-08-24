using System;
using UnityEngine;

/// <summary>ゲーム内UIのまとめ役</summary>
[Serializable]
public class GameUIController
{
    [SerializeField] private Animator m_animator;
    [SerializeField] internal GameUI gameUI;

    internal void Initialize()
    {
        gameUI.Initialize(m_animator);
    }
}

[Serializable]
internal class ResultUIController
{
    [SerializeField] private Animator m_animator;
    [SerializeField] internal ResultUI resultUI;

    internal void Initialize()
    {
        resultUI.Initialize(m_animator);
    }
}