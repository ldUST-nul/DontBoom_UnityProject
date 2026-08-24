using UnityEngine;

/// <summary>
/// ゲーム、ゲーム内オブジェクトの初期状態を設定する為のクラス
/// </summary>
[CreateAssetMenu(menuName = "SceneData/GameSetting")]
internal class GameSettings : ScriptableObject
{
    [SerializeField] internal StageLightsSettings lightSettings;
    [SerializeField] internal CameraSettings camSetting;
    [SerializeField] internal StageSettings stageSetting;
    [SerializeField] internal BallSettings ballSetting;
}