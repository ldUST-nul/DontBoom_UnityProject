using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

// ボールオブジェクトに付与するためのスクリプト。
public class Ball : MonoBehaviour
{
    internal event Action actionClear;
    internal event Action actionDeath;

    internal enum BallKind { Red, Blue }
    [SerializeField] internal BallKind m_kind;

    internal int ballLife { get; private set; } // ボールの体力
    private bool m_isDead;
    private float m_speedThreshold;

    internal int m_score { get; private set; }

    private Rigidbody m_rigid;
    [SerializeField] private ParticleSystem m_particle;
    private MeshRenderer m_meshRenderer;

    private Vector3 m_startPos;
    private bool m_saveThePosCheck;

    private AudioSource m_audio;
    private AudioClip m_audioClip_deathSound;

    /// <summary>
    /// 初期化処理の記述</summary>
    /// <param name="gameData">初期化に必要なデータ</param>
    /// <remarks>初期化に有するデータの代入をしていく。</remarks>

    internal void Initialize(GameSettings gameData)
    {
        if (!m_saveThePosCheck)
        {
            m_startPos = this.transform.position;
            m_saveThePosCheck = true;
        }
        this.transform.position = m_startPos;

        m_score = gameData.ballSetting.m_score;
        ballLife = gameData.ballSetting.m_maxHP;
        m_isDead = false;
        m_speedThreshold = gameData.ballSetting.m_speedDamageThreshold;

        m_audioClip_deathSound = gameData.ballSetting.m_deathSound;

        m_audio = this.GetComponent<AudioSource>();
        m_rigid = this.GetComponent<Rigidbody>();
        //m_particle = this.GetComponent<ParticleSystem>();
        m_meshRenderer = this.GetComponent<MeshRenderer>();
        if (m_rigid == null || m_particle == null || m_meshRenderer == null) return;
        m_meshRenderer.enabled = true;

        m_particle.Stop();
        m_particle.Clear();
    }

    // Collisionを所持しているオブジェクトへの衝突時(殆ど)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (m_rigid.linearVelocity.magnitude >= m_speedThreshold || collision.gameObject.tag == "Obstruct")
        {
            Debug.Log($"Damaged!\nSpeed:{m_rigid.linearVelocity.magnitude}\nHitObject:{collision.gameObject.name}");
            Damaged();
        }
        if (collision.gameObject.tag == "Underline")
        {
            Death();
            DeathEvent();
        }
    }

    private void Update()
    {
        BallSounds();
    }

    /// <summary>ダメージを受ける処理</summary>
    internal void Damaged()
    {
        /// ボールライフ仕様廃止予定
        //ballLife--;
        if (ballLife <= 0)
        {
            Death();
            DeathEvent();
        }
    }

    internal void PlayDeathSounds()
    {
        if (!m_audio || !m_audioClip_deathSound) return;
        m_audio.clip = m_audioClip_deathSound;
        m_audio.Play();
    }

    /// <summary>死亡確認処理</summary>
    internal void Death()
    {
        if (m_isDead == true) return;
        m_isDead = true;
        ballLife = 0;
        m_rigid.Sleep(); // 物理演算処理を停止
        m_particle.Play();
        m_meshRenderer.enabled = false;

        PlayDeathSounds();
        // 死亡通知
    }

    internal void DeathEvent()
    {
        actionDeath?.Invoke();
    }

    // ゴール到達時処理
    internal void TryGoal()
    {
        // ゴール処理を伝える
        actionClear?.Invoke(); // 到着通知
        m_rigid.Sleep(); // 物理演算処理を停止
    }

    internal void Active(bool activetion)
    {
        this.gameObject.SetActive(activetion);
    }

    internal void Delete()
    {
        //this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    internal void BallWakeUp()
    {
        m_rigid.WakeUp();
    }

    /// <summary>ボールを止める為の処理</summary>
    internal void BallSleep()
    {
        m_rigid.Sleep();
    }

    /// 
    internal void BallSounds()
    {
        if (m_rigid.IsSleeping()) return;

        var ballSpeed = m_rigid.linearVelocity.sqrMagnitude;

        if (ballSpeed >= 3f) Debug.Log("Speed 3");
        else if (ballSpeed >= 2f) Debug.Log("Speed 2");
        else if (ballSpeed >= 1f) Debug.Log("Speed 1");
        else if (ballSpeed >= 0f) Debug.Log("Speed 0");
    }
}
