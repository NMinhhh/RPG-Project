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
        //LoadGame();
        LoadSettingData();
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
    
    public void SaveItemWorldId(int id)
    {
        saveData.itemWorldId.Add(id);
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

    public void SaveBirdgeID(int id)
    {
        saveData.bridgeId.Add(id);
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
        StartCoroutine(LoadGameData());
    }

    IEnumerator LoadGameData() 
    { 
        yield return new WaitForSeconds(0);
        if (this.saveData != null)
        {
            LoadTask(saveData);
            LoadChestOpenObj(saveData);
            LoadItemWolrd(saveData);
            LoadItem(saveData);
            LoadItemEquipped(saveData);
            LoadBridge(saveData);
        }
        isLoading = false;
    }

    public void LoadSettingData()
    {
        string jsonString = SaveSystem.LoadFile(this.save);
        this.saveData = JsonUtility.FromJson<SaveData>(jsonString);

        if (this.saveData != null)
        {
            LoadVolume(saveData);
            LoadSensetivity(saveData);
        }
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

    public void LoadItemWolrd(SaveData saveData)
    {
        foreach (int id in saveData.itemWorldId)
        {
            ItemWorld chest = GameManager.Instance.GetItemWorld(id);
            chest.ItemWorldPickedUp();
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

    public void LoadBridge(SaveData saveData)
    {
        foreach (int id in saveData.bridgeId)
        {
            Bridge bridge = GameManager.Instance.GetBridge(id);
            bridge.BridgeFall();
        }
    }

    public void LoadPotionAmount(SaveData saveData)
    {
        EquipSystem.Instance.AddPotionItem(saveData.potionAmount - 2);
    }

    #endregion
}
