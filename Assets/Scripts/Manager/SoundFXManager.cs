using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundFXManager : Singleton<SoundFXManager>
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Music Slider")]
    [SerializeField] private Slider musicSlider;

    [Header("Sound Slider")]
    [SerializeField] private Slider soundSlider;

    [Header("Sound Source")]
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private List<Sound> soundList = new List<Sound>();


    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(volume) * 20);
    }


    public void PlaySound(Sound sound, Vector3 pos, float value = 1)
    {
        if(sound == null) return;
        AudioSource soundS = ObjectPool.Instance.SpawnFromPool(Pool.Type.Sound, pos, Quaternion.identity).GetComponent<AudioSource>();
        soundS.PlayOneShot(sound.clip, value);
    }

    public Sound GetSound(Sound.SoundType type)
    {
        foreach (var sound in soundList) 
        {
            if(sound.soundType == type) return sound;
        }
        return null;
    }
}

[System.Serializable]
public class Sound
{
    public enum SoundType
    {
        Hit,
        Swing,
        Arrow,
        Healing,
        Click,
        OpenChest
    }

    public SoundType soundType;
    public AudioClip clip;
}
