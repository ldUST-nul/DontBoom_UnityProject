using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>リザルト画面のUI（シャッター演出込みのやつ）</summary>
[Serializable]
public class ResultUI
{
    [Header("---------- リザルト画面ボタン ----------"), Space(10)] // AI君いいこと教えてくれるネ～
    [SerializeField] private Button m_retryButton;
    [SerializeField] private Button m_titleButton;

    [Header("---------- シーン変更用 ----------"), Space(10)]
    [SerializeField] private Button m_nextLevelButton;
    [SerializeField] private int m_typeNextSceneIndex;
    internal int m_nextSceneIndex => m_typeNextSceneIndex;

    internal event Action onRetry;
    internal event Action onTitle;
    internal event Action onNextLevel;

    [SerializeField] private GameObject m_shutter;

    [Header("---------- リザルト表示用テキスト ----------"), Space(10)]
    [SerializeField] private Text m_timer;
    [SerializeField] private Text m_score;

    private Animator m_animator;

    internal void Initialize(Animator animator)
    {
        m_animator = animator;
        m_shutter.SetActive(false);

        m_retryButton.interactable = true;
        m_titleButton.interactable = true;

        // 一旦全解除
        m_retryButton.onClick.RemoveAllListeners(); // 登録解除
        m_titleButton.onClick.RemoveAllListeners(); // 登録解除

        // 再登録
        m_retryButton.onClick.AddListener(() => onRetry?.Invoke()); // ボタンが押された時に処理を追加
        m_titleButton.onClick.AddListener(() => onTitle?.Invoke()); // ボタンが押された時に処理を追加

        if (!m_nextLevelButton) return;
        m_nextLevelButton.interactable = true;
        m_nextLevelButton.onClick.AddListener(() => onNextLevel.Invoke());
    }

    internal void ActiveNextButton(bool active)
    {
        if (!m_nextLevelButton) return;
        m_nextLevelButton.gameObject.SetActive(active);
    }

    internal void OnExitResult()
    {
        m_retryButton.interactable = false;
        m_titleButton.interactable = false;

        if (!m_nextLevelButton) return;
        m_nextLevelButton.interactable = false;
    }

    /// <summary>テキストセット</summary>
    internal void SetTexts(float time, int score)
    {
        if (m_timer == null || m_score == null) return;
        // 前まで頭がおかしかったようでスコアの表示方法きもかったので修正
        m_timer.text = $"Time : {time:F2}";
        m_score.text = $"Score : {score}";
    }

    internal void ShutterUp()
    {
        m_animator?.SetTrigger("ShutterUp");
    }

    internal void ShutterDown()
    {
        m_animator?.SetTrigger("ShutterDown");
    }
}