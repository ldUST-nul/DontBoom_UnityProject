using UnityEngine;
namespace TymeSystem
{
    public class RunTimer : MonoBehaviour
    {
        public float m_elapsed { get; private set; }
        public bool m_isStopped { get; private set; }
        private bool m_hasFired;

        void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            m_elapsed = 0;
            m_isStopped = false;
        }

        public void Update()
        {
            if (m_isStopped || m_hasFired) return;
            m_elapsed += Time.deltaTime;
        }

        /// <summary>指定した時間をセットする。</summary>
        /// <remarks>加算や減算ではなく代入</remarks>
        public void SetTime(float setTime)
        {
            m_elapsed = setTime;
        }

        /// <summary>指定した時間に達したか。</summary>
        /// <param name="limit">指定</param>
        public bool IsFinished(float limit) => m_elapsed >= limit; // 純粋な問い合わせ（毎回true）

        public bool TryFire(float limit) // 1度だけtrue
        {
            if (m_hasFired || m_elapsed < limit) return false;
            m_hasFired = true;
            return true;
        }

        /// <summary>時間を再度1から再生する。。</summary>
        public void Replay()
        {
            m_elapsed = 0;
            m_isStopped = false;
        }

        /// <summary>時間を停止する。</summary>
        public void Pause()
        {
            m_isStopped = true;
        }

        /// <summary>時間を再生する。</summary>
        public void Unpause()
        {
            m_isStopped = false;
        }
    }
}