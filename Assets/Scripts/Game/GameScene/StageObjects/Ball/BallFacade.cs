using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class BallManager
{
    [SerializeField] internal List<BallGenerator> ballGenerator;
    
    /// <summary>恐らくball以外を使用すると思われる。</summary>
    /// <param name="gameData"></param>
    internal void Initialize(GameSettings gameData)
    {
        foreach (var gene in ballGenerator)
        {
            gene.Initialize(gameData);

            foreach (var ball in gene.m_balls)
            {
                ball?.Initialize(gameData);
            }
        }
    }

    internal void AllDeath()
    {
        foreach (var gene in ballGenerator)
        {
            foreach (var ball in gene.m_balls)
            {
                ball?.Death();
            }
        }
    }
    
    internal void AllWakeUp()
    {
        foreach (var gene in ballGenerator)
        {
            foreach (var ball in gene.m_balls)
            {
                ball?.BallWakeUp();
            }
        }
    }
    internal void AllSleep()
    {
        foreach (var gene in ballGenerator)
        {
            foreach (var ball in gene.m_balls)
            {
                ball?.BallSleep();
            }
        }
    }

    /// <summary>全Generator横断で、生きているボールを集めて返す</summary>
    internal IReadOnlyList<Ball> AllBalls()
    {
        var list = new List<Ball>();
        foreach (var gene in ballGenerator)
            foreach (var ball in gene.ActiveBalls)
                if (ball != null) list.Add(ball);
        return list;
    }
}