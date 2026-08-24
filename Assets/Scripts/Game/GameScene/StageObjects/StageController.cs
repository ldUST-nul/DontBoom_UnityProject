using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ステージの傾き制御データと処理</summary>
[Serializable]
public class StageController
{
    [SerializeField] private GameObject m_playerStage;
    [SerializeField] private float m_rotePower;
    [SerializeField] private float m_roteLimit;
    [SerializeField] private float m_timescaleSpeed;

    [SerializeField] private bool m_freezX;
    [SerializeField] private bool m_freezY;

    [SerializeField] private bool m_reverseX;
    [SerializeField] private bool m_reverseY;

    private Vector3 m_stageEuler;
    private Vector3? m_saveStageEuler;
    private bool m_initialSaveEuler = false;

    internal float m_testerAmount;

    internal void Initialize(GameSettings gameData)
    {

        ///シリアライズでプロパティを編集できるようにしているので使用しない。
        //m_rotePower = gameData.stageSetting.m_rotePower;
        //m_roteLimit = gameData.stageSetting.m_roteLimit;
        //m_timescaleSpeed = gameData.stageSetting.m_timescaleSpeed;

        if (m_initialSaveEuler == false) 
        {
            m_saveStageEuler = m_playerStage.transform.eulerAngles;
            m_initialSaveEuler = true;
        }
        m_playerStage.transform.eulerAngles = m_saveStageEuler.Value;
        m_stageEuler = m_saveStageEuler.Value;
    }

    internal void Movement()
    {
        float pX = m_freezX ? 0 : Input.GetAxis("Horizontal");
        float pY = m_freezY ? 0 : Input.GetAxis("Vertical");

        pX *= !m_reverseX ? 1 : -1;
        pY *= !m_reverseY ? 1 : -1;
        StageTilt(pX, pY);
    }

    private void StageTilt(float pX, float pY)
    {
        m_stageEuler.z -= pX * m_rotePower * Time.deltaTime * m_timescaleSpeed;
        m_stageEuler.x += pY * m_rotePower * Time.deltaTime * m_timescaleSpeed;

        m_stageEuler.z = Mathf.Clamp(m_stageEuler.z, -m_roteLimit, m_roteLimit);
        m_stageEuler.x = Mathf.Clamp(m_stageEuler.x, -m_roteLimit, m_roteLimit);

        m_playerStage.transform.eulerAngles = m_stageEuler;
    }
}