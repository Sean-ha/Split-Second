using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        DoorSound,
        DeathSound,
        UISound,
        WinSound,
        JumpSound,
        ExitLevel,
        StartLevel,
        ClockTick,
        UISoundReverse
    }

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

        // Set volume of sfx here
        audioSource.volume = PlayerPrefs.GetFloat("SFXVolume", 75);

        audioSource.PlayOneShot(GetAudioClip(sound));

        GameObject.Destroy(soundGameObject, 5);

    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.instance.soundAudioClips)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        return null;
    }
}
