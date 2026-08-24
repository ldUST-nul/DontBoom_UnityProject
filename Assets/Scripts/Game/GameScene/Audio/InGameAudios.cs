using System;
using UnityEngine;

[Serializable]
public class GameAudioData
{
    [SerializeField] internal AudioSource m_audioPlayer;

    [SerializeField] internal AudioClip m_standbyBGM;
    [SerializeField] internal AudioClip m_playBGM;
    [SerializeField] internal AudioClip m_tickSound;

    [SerializeField] internal AudioClip m_shutterSound;
    [SerializeField] internal AudioClip m_clickSound;
}