using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class ButtonManager
{
    ButtonDatas buttonData;

    public event Action onStart;
    public event Action m_Level1;
    public event Action m_Level2;
    public event Action m_Level3;
    public event Action m_Exit;

    public ButtonManager(ButtonDatas data)
    {
        buttonData = data;

        buttonData.m_startButton.interactable = true;
        SetLevelSelectInteractable(false);
    }

    public void SetAction()
    {
        buttonData.m_startButton.onClick.RemoveAllListeners();
        buttonData.m_level1Button.onClick.RemoveAllListeners();
        buttonData.m_level2Button.onClick.RemoveAllListeners();
        buttonData.m_level3Button.onClick.RemoveAllListeners();
        buttonData.m_exitButton.onClick.RemoveAllListeners();

        buttonData.m_startButton.onClick.AddListener(() => onStart?.Invoke());
        buttonData.m_level1Button.onClick.AddListener(() => m_Level1?.Invoke());
        buttonData.m_level2Button.onClick.AddListener(() => m_Level2?.Invoke());
        buttonData.m_level3Button.onClick.AddListener(() => m_Level3?.Invoke());
        buttonData.m_exitButton.onClick.AddListener(() => m_Exit?.Invoke());

        onStart += TitleStartButton;
        m_Level1 += StartLevel1;
        m_Level2 += StartLevel2;
        m_Level3 += StartLevel3;
        m_Exit += () => {
                #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
                #else
            Application.Quit();
                #endif
            };
    }

    void TitleStartButton()
    {
        Debug.Log("start");
        buttonData.m_animator.SetTrigger("Scroll");
        buttonData.m_startButton.interactable = false;
        SetLevelSelectInteractable(true);
    }

    void StartLevel1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage1");
    }
    void StartLevel2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage2");
    }
    void StartLevel3()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage3");
        return;
    }

    public void SetLevelSelectInteractable(bool s)
    {
        buttonData.m_level1Button.interactable = s;
        buttonData.m_level2Button.interactable = s;
        buttonData.m_level3Button.interactable = s;
        buttonData.m_exitButton.interactable = s;
    }
}
[Serializable]
public class ButtonDatas
{
    [SerializeField] public Button m_startButton;

    [SerializeField] public Button m_level1Button;
    [SerializeField] public Button m_level2Button;
    [SerializeField] public Button m_level3Button;

    [SerializeField] public Button m_exitButton;

    [SerializeField] internal Animator m_animator;
}
[Serializable]
public class ButtonContext
{
    [NonSerialized] public ButtonManager buttonManager;
    [SerializeField] public ButtonDatas buttonDatas;

    public void Initialize()
    {
        buttonManager = new ButtonManager(buttonDatas);
        buttonManager.SetAction();
    }
}