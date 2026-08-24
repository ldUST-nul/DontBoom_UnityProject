using System.Collections.Generic;
using UnityEngine;

/// <summary>ボールを生成するだけのファクトリ。タイミング管理は持たない。</summary>
public class BallGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_ballPrefabs;       // index = BallKind
    [SerializeField] internal List<Transform> m_generatePositions;

    internal List<Ball> m_balls = new();
    internal IReadOnlyList<Ball> ActiveBalls => m_balls;

    private GameSettings m_gameData;

    internal void Initialize(GameSettings gameData)
    {
        foreach (var ball in m_balls)
            if (ball != null) Destroy(ball.gameObject);
        m_balls.Clear();

        m_gameData = gameData;
    }
    
    internal Ball Spawn(BallSpawnEntry entry)
    {
        var prefab = m_ballPrefabs[(int)entry.kind];
        var pos = m_generatePositions[entry.positionIndex];
        var ball = Instantiate(prefab, pos.position, Quaternion.identity);

        var ballComponent = ball.GetComponent<Ball>();

        ballComponent.Initialize(m_gameData);
        m_balls.Add(ballComponent);
        return ballComponent;
    }
}
