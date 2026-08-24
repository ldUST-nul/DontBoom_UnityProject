using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CameraController
{
    [SerializeField] private Camera m_camera;

    [SerializeField] private Transform m_initialTransform;  // 俯瞰用固定位置
    private Transform m_overviewTarget;

    [SerializeField] private Vector3 m_centerOffset;
    [SerializeField] private float m_defaultFollowSpeed;
    [SerializeField] private float m_defaultFollowAmount;

    [Header("Zoom")]
    [SerializeField] private float m_zoomPerUnit  = 0.5f;  // 広がり1unitあたりの引き距離
    [SerializeField] private float m_minZoom      = 0f;
    [SerializeField] private float m_maxZoom      = 12f;
    [SerializeField] private float m_zoomSpeed    = 3f;

    private float m_followSpeed;
    private float m_followAmount;
    private float m_currentZoom;

    private Vector3 m_basePosition;
    private bool m_baseSaved;
    private bool m_isOverView;

    internal void Initialize(GameSettings gameData)
    {
        m_centerOffset = gameData.camSetting.m_centerOffset;

        if (!m_baseSaved)
        {
            m_basePosition = m_camera.transform.position;
            m_baseSaved = true;
        }

        m_camera.transform.position = m_basePosition + m_centerOffset;

        m_followSpeed  = m_defaultFollowSpeed;
        m_followAmount = m_defaultFollowAmount;
        m_currentZoom  = 0f;
        m_isOverView   = false;
        m_overviewTarget = m_initialTransform;
    }

    /// <summary>ゲームオーバー・クリア時に俯瞰へ切り替え</summary>
    internal void SwitchToOverView()
    {
        m_followAmount = 1f;
        m_isOverView   = true;
    }

    /// <summary>プレイ中：全ボールを画角に収めながら追従</summary>
    internal void CameraMoving(IReadOnlyList<Ball> balls)
    {
        if (m_isOverView)
        {
            MoveToward(m_overviewTarget.position, 0f);
            return;
        }

        Vector3 centroid = CalcCentroid(balls);
        float   spread   = CalcSpread(balls, centroid);

        float targetZoom = Mathf.Clamp(spread * m_zoomPerUnit, m_minZoom, m_maxZoom);
        m_currentZoom = Mathf.Lerp(m_currentZoom, targetZoom, Time.deltaTime * m_zoomSpeed);

        // カメラ後方向にズームオフセットを加える（角度に依存しない）
        Vector3 zoomOffset = -m_camera.transform.forward * m_currentZoom;

        MoveToward(centroid, m_followAmount, zoomOffset);
    }

    /// <summary>俯瞰モード専用（GameClear / GameOver から呼ぶ）</summary>
    internal void CameraMoving()
    {
        MoveToward(m_overviewTarget.position, m_followAmount);
    }

    // -------------------------------------------------------

    private void MoveToward(Vector3 worldTarget, float amount, Vector3 extraOffset = default)
    {
        Vector3 targetPos = m_basePosition + m_centerOffset
                          + (worldTarget - m_basePosition) * amount
                          + extraOffset;

        m_camera.transform.position = Vector3.Lerp(
            m_camera.transform.position,
            targetPos,
            Time.deltaTime * m_followSpeed
        );
    }

    private Vector3 CalcCentroid(IReadOnlyList<Ball> balls)
    {
        if (balls.Count == 0) return m_basePosition;

        Vector3 sum  = Vector3.zero;
        int alive = 0;
        foreach (var ball in balls)
        {
            if (ball == null || !ball.gameObject.activeInHierarchy) continue;
            sum += ball.transform.position;
            alive++;
        }
        return alive > 0 ? sum / alive : m_basePosition;
    }

    private float CalcSpread(IReadOnlyList<Ball> balls, Vector3 centroid)
    {
        float maxDist = 0f;
        foreach (var ball in balls)
        {
            if (ball == null || !ball.gameObject.activeInHierarchy) continue;
            maxDist = Mathf.Max(maxDist, Vector3.Distance(ball.transform.position, centroid));
        }
        return maxDist;
    }
}
