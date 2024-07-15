using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : Singleton<SoundFXManager>
{
    [SerializeField] private List<Sound> soundList = new List<Sound>();
    [SerializeField] private AudioSource soundSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(Sound sound, Vector3 pos, float value)
    {
        AudioSource soundS = Instantiate(soundSource, pos, Quaternion.identity);
        soundS.volume = value;
        soundS.clip = sound.clip;
        soundS.Play();
        float lenth = soundS.clip.length;
        Destroy(soundS, lenth);
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
