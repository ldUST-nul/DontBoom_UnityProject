namespace TimeSystem
{
    public class Timer
    {
        public float m_elapsed { get; private set; }
        public bool m_isStopped { get; private set; }
        private bool m_hasFired;

        public Timer(bool Pause)
        {
            m_elapsed = 0;
            m_isStopped = Pause;
        }

        /// <summary>毎フレーム呼ぶ用の機能</summary>
        /// <remarks>Updateで読ませる、引数にはtime/deltaTimeを代入</remarks>
        public void Tick(float time)
        {
            if (m_isStopped || m_hasFired) return;
            m_elapsed += time;
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

        /// <summary>削除予定</summary>
        public void FireSwitch(bool s)
        {
            m_hasFired = s;
        }

        /// <summary>時間を再度1から再生する。</summary>
        public void Replay()
        {
            m_elapsed = 0;
            m_isStopped = false;
        }

        /// <summary>リセット。</summary>
        public void Reset()
        {
            m_elapsed = 0;
            m_isStopped = false;
            m_hasFired = false;
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