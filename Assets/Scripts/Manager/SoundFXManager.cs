using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SoundFXManager : Singleton<SoundFXManager>
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Music Slider")]
    [SerializeField] private Slider musicSlider;

    [Header("Sound Slider")]
    [SerializeField] private Slider soundSlider;

    [Header("Music")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private List<Sound> musicList;
     
    [Header("Sound Source")]
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private List<Sound> soundList = new List<Sound>();

    private Dictionary<string, float> soundDictionary;

    [SerializeField] private string textSound;

    private void Start()
    {
        soundDictionary = new Dictionary<string, float>();
        soundDictionary["Damage Hit"] = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlaySound(GetSound(textSound),transform.position);
        }
    }

    #region Slider

    public void LoadSliderValue(float soundVolume, float musicVolume)
    {
        //Sound
        soundSlider.value = soundVolume;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(soundVolume) * 20);
        //Music
        musicSlider.value = musicVolume;
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(musicVolume) * 20);
    }


    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        SaveManager.Instance.SaveMusicVolume(volume);
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(volume) * 20);
        SaveManager.Instance.SaveSoundVolume(volume);
    }

    #endregion

    #region Sound

    public void PlaySound(Sound sound, Vector3 pos)
    {
        if(sound == null) return;
        if (CanPlayerSound(sound.soundName))
        {
            AudioSource soundS = ObjectPool.Instance.SpawnFromPool("Sound", pos, Quaternion.identity).GetComponent<AudioSource>();
            soundS.volume = sound.volume;
            soundS.PlayOneShot(sound.clip);
        }
   
    }


    bool CanPlayerSound(string soundName)
    {
        switch (soundName)
        {
            default:
                return true;
            case "Damage Hit":
                return CheckCanPlayerSound(soundName);
        }
    }

    bool CheckCanPlayerSound(string soundName) 
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            float lastTime = soundDictionary[soundName];
            float soundLength = GetSound(soundName).clip.length;
            if(Time.time > lastTime + soundLength - soundLength - 0.1f)
            {
                soundDictionary[soundName] = Time.time;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public Sound GetSound(string soundName)
    {
        foreach (var sound in soundList) 
        {
            if(sound.soundName == soundName) return sound;
        }
        Debug.Log("Don't have " + soundName);
        return null;
    }

    #endregion

    #region Music

    public void ChangeMusic()
    {
        LeanTween.value(0, 1, 2).setOnUpdate((value) =>
        {
            musicSource.volume = Mathf.Lerp(musicSource.volume, 0, value);
        });

    }

    public void PlayMusic(Sound music)
    {
        musicSource.volume = 0;
        musicSource.clip = music.clip;
        musicSource.volume = music.volume;
        musicSource.Play();
    }

    public Sound GetMusic(string musicName)
    {
        foreach (var music in musicList)
        {
            if (music.soundName == musicName) return music;
        }
        Debug.Log("Don't have " + musicName);
        return null;
    }

    #endregion
}

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
    [Range(0, 1)]
    public float volume = .5f;
}
