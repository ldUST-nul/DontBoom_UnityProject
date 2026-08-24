using UnityEngine;

/// <summary>
/// アプリ起動時にフレームレートを60固定する。
/// GameObjectへのアタッチ不要。起動時に自動で1度だけ走る。
/// </summary>
public static class FrameRateSetup
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Apply()
    {
        // targetFrameRate は VSync がオンだと無視されるので、先に切る。
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}
