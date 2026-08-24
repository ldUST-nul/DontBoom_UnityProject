using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] ButtonContext buttonsContexts;

    // タイトル画面で唯一ランタイムを持つオブジェクト。
    // ここから主に動作をするようにする。(Mono継承による、独自ランタイム)

    void Awake()
    {
        // 初期化
        buttonsContexts.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetAction()
    {

    }
}
