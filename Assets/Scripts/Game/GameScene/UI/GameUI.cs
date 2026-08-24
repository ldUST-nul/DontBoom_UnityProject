using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>プレイ中の画面UI（タイマー・スコア・ライフ）</summary>
[Serializable]
public class GameUI
{
    [SerializeField] private Text m_timer;
    [SerializeField] private Text m_score;

    private Animator m_animator;

    internal void Initialize(Animator animator)
    {
        m_timer.text = "";
        m_score.text = "";
        m_animator = animator;
    }

    internal void Show()
    {
        m_animator.SetTrigger("ShowUp"); // 将来的に数式で移動
        Debug.Log("ShowUp Texts");
    }

    internal void Hide()
    {
        m_animator.SetTrigger("ShowDown"); // 将来的に数式で移動
        Debug.Log("Hide Texts");
    }

    internal void UpdateTexts(float gameTime, int playerScore)
    {
        m_timer.text = $"”{Math.Floor(gameTime)}”";
        m_score.text = $"Score:{playerScore}";
    }

    /// <summary>スコア加算</summary>
    /// <remarks>ゲーム中に表示するスコアに加算する</remarks>
    internal void AddScore(int add)
    {
        //m_score += add;
    }
}
