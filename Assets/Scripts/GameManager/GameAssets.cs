﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets instance
    {
        get
        {
            if(_i == null)
            {
                _i = Instantiate(Resources.Load<GameAssets>("Prefabs/GameAssets"));
            }
            return _i;
        }
    }

    public SoundAudioClip[] soundAudioClips;

    public float sfxVolume;
    public float bgmVolume;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
