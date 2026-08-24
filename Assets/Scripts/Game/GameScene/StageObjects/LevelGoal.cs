using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] private Ball.BallKind m_acceptedKind;
    internal Ball.BallKind AcceptedKind => m_acceptedKind; // 読み取り用

    [SerializeField] internal AudioSource m_audioSource;
    [SerializeField] internal AudioClip m_goalSound;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Ball>(out var ball))
        {
            if (ball.m_kind == m_acceptedKind)
            {
                ball.TryGoal();
                ball.Active(false);

                GoalSound();
            }
            else
            {
                ball.Death();
                ball.DeathEvent();
            } 
        }
    }

    void GoalSound()
    {
        m_audioSource.clip = m_goalSound;
        m_audioSource.Play();
    }
}
