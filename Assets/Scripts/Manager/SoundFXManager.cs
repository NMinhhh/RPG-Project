using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : Singleton<SoundFXManager>
{
    [SerializeField] private List<Sound> soundList = new List<Sound>();
    [SerializeField] private AudioSource soundSource;

    public void PlaySound(Sound sound, Vector3 pos, float value)
    {
        AudioSource soundS = ObjectPool.Instance.SpawnFromPool(Pool.Type.Sound, pos, Quaternion.identity).GetComponent<AudioSource>();
        soundS.volume = value;
        soundS.clip = sound.clip;
        soundS.Play();
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
    }

    public SoundType soundType;
    public AudioClip clip;
}
