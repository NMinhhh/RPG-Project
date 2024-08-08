using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private string save = "Save_1";
    [SerializeField] protected SaveData saveData = new SaveData();

    public static event Action<float> loadSensetivity;

    void Start()
    {
        SaveSystem.Initilize();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public string GetFileName()
    {
        return save;
    }

    #region Save

    public void SaveGame()
    {
        string jsonString = JsonUtility.ToJson(this.saveData);
        SaveSystem.SaveFile(save, jsonString);
    }

    public void SaveMusicVolume(float volume)
    {
        saveData.musicVolume = volume;
    }

    public void SaveSoundVolume(float volume)
    {
        saveData.soundVolume = volume;
    }

    public void SaveSensetivity(float sensetivity)
    {
        saveData.sensetivity = sensetivity;
    }

    public void SaveTask(int taskId)
    {
        saveData.taskId = taskId;
    }

    #endregion

    #region Load

    public void LoadGame()
    {
        StartCoroutine(LoadData());
    }

    IEnumerator LoadData() 
    { 
        yield return new WaitForSeconds(1f);
        string jsonString = SaveSystem.LoadFile(this.save);

        this.saveData = JsonUtility.FromJson<SaveData>(jsonString);

        if (this.saveData != null)
        {
            LoadVolume(saveData);
        }
    }

    public void LoadVolume(SaveData saveData)
    {
        SoundFXManager.Instance.SetMusicVolume(saveData.musicVolume);
        SoundFXManager.Instance.SetSoundVolume(saveData.soundVolume);
    }

    public void LoadSensetivity(SaveData saveData)
    {
        loadSensetivity?.Invoke(saveData.sensetivity);
    }

    public float GetSesetivity()
    {
        return saveData.sensetivity;
    }

    public void LoadTask(SaveData saveData)
    {
        TaskManager.Instance.Initialize(saveData.taskId);
        TaskManager.Instance.GetMainTask().InitializeTaskStep(0);
    }

    #endregion
}
