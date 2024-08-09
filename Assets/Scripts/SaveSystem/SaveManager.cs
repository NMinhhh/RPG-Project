using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private string save = "Save_1";
    [SerializeField] protected SaveData saveData = new SaveData();

    public static event Action<float> loadSensetivity;

    public bool isLoading;

    void Start()
    {
        SaveSystem.Initilize();
        LoadGame();
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

    public void SaveChestOpenedId(int id)
    {
        saveData.chestOpenedId.Add(id);
    }

    public void SaveItem(string name)
    {
        saveData.itemName.Add(name);
    }

    public void SaveItemEquipped(string name)
    {
        saveData.itemName.Remove(name);
        saveData.itemEquippedName = name;
    }

    public void SavePotionAmount(int amount)
    {
        saveData.potionAmount = amount;
    }

    #endregion

    #region Load

    public void LoadGame()
    {
        isLoading = true;
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
            LoadSensetivity(saveData);
            LoadTask(saveData);
            LoadChestOpenObj(saveData);
            LoadItem(saveData);
            LoadItemEquipped(saveData);
        }
        isLoading = false;
    }

    public void LoadVolume(SaveData saveData)
    {
        SoundFXManager.Instance.LoadSliderValue(saveData.soundVolume, saveData.musicVolume);
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
    }

    public void LoadChestOpenObj(SaveData saveData)
    {
        foreach (int id in saveData.chestOpenedId)
        {
            Chest chest = GameManager.Instance.GetChest(id);
            chest.ChestOpened();
        }
    }

    public void LoadItem(SaveData saveData)
    {
        foreach(string itemName in saveData.itemName)
        {
            ItemData itemData = GameManager.Instance.GetItemData(itemName);
            InventorySystem.Instance.AddToInventory(itemData);
        }
    }

    public void LoadItemEquipped(SaveData saveData)
    {
        if(saveData.itemEquippedName != null  && saveData.itemEquippedName != "")
        {
            ItemData itemData = GameManager.Instance.GetItemData(saveData.itemEquippedName);
            EquipSystem.Instance.EquipWeapon(itemData);
        }
    }

    public void LoadPotionAmount(SaveData saveData)
    {
        EquipSystem.Instance.AddPotionItem(saveData.potionAmount - 2);
    }

    #endregion
}
