using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public enum SaveType
    {
        Setting,
        GameData
    }

    private string save = "Save";
    [SerializeField] protected SettingData saveSettingData = new SettingData();
    [SerializeField] protected SaveGameData saveGameData = new SaveGameData();

    public static event Action<float> loadSensetivity;

    public bool isLoading;

    void Start()
    {
        SaveSystem.Initilize();
        LoadSettingData();
    }

    private void OnApplicationQuit()
    {
        SaveAllData();
    }


    #region Save

    public string GetSaveName(SaveType saveType)
    {
        return save + "_" + saveType;
    }

    public void SaveAllData()
    {
        SaveData(SaveType.Setting);
        SaveData(SaveType.GameData);
    }

    public void SaveData(SaveType saveType)
    {
        string jsonString;
        switch (saveType)
        {
            default:
                return;
            case SaveType.Setting:
                jsonString = JsonUtility.ToJson(this.saveSettingData);
                SaveSystem.SaveFile(GetSaveName(saveType), jsonString);
                break;
            case SaveType.GameData:
                jsonString = JsonUtility.ToJson(this.saveGameData);
                SaveSystem.SaveFile(GetSaveName(saveType), jsonString);
                break;
        }
       
    }

    public void SaveMusicVolume(float volume)
    {
        saveSettingData.musicVolume = volume;
    }

    public void SaveSoundVolume(float volume)
    {
        saveSettingData.soundVolume = volume;
    }

    public void SaveSensetivity(float sensetivity)
    {
        saveSettingData.sensetivity = sensetivity;
    }

    public void SaveTask(int taskId)
    {
        saveGameData.taskId = taskId;
    }

    public void SaveChestOpenedId(int id)
    {
        saveGameData.chestOpenedId.Add(id);
    }
    
    public void SaveItemWorldId(int id)
    {
        saveGameData.itemWorldId.Add(id);
    }

    public void SaveItem(string name)
    {
        saveGameData.itemName.Add(name);
    }


    public void SaveItemEquipped(string name)
    {
        saveGameData.itemName.Remove(name);
        saveGameData.itemEquippedName = name;
    }

    public void SaveBirdgeID(int id)
    {
        saveGameData.bridgeId.Add(id);
    }

    public void SavePotionAmount(int amount)
    {
        saveGameData.potionAmount = amount;
    }

    #endregion

    #region Setting


    public void LoadSettingData()
    {
        string jsonString = SaveSystem.LoadFile(GetSaveName(SaveType.Setting));
        this.saveSettingData = JsonUtility.FromJson<SettingData>(jsonString);

        if (this.saveSettingData != null)
        {
            LoadVolume(saveSettingData);
            LoadSensetivity(saveSettingData);
        }
        else
        {
            SaveData(SaveType.Setting);
        }
    }

    public void LoadVolume(SettingData saveSettingData)
    {
        SoundFXManager.Instance.LoadSliderValue(saveSettingData.soundVolume, saveSettingData.musicVolume);
    }

    public void LoadSensetivity(SettingData saveSettingData)
    {
        loadSensetivity?.Invoke(saveSettingData.sensetivity);
    }

    public float GetSesetivity()
    {
        return saveSettingData.sensetivity;
    }



    #endregion

    #region Load Game

    public void ResetGameData()
    {
        saveGameData.Reset();
        SaveAllData();
    }

    public void LoadGame()
    {
        isLoading = true;
        string jsonString = SaveSystem.LoadFile(GetSaveName(SaveType.GameData));
        this.saveGameData = JsonUtility.FromJson<SaveGameData>(jsonString);
        if(this.saveGameData != null)
        {
            LoadGameData(saveGameData);
        }
        else
        {
            SaveData(SaveType.GameData);
        }
    }

    void LoadGameData(SaveGameData saveGameData)
    {
        if (saveGameData != null)
        {
            LoadTask(saveGameData);
            LoadChestOpenObj(saveGameData);
            LoadItemWolrd(saveGameData);
            LoadItem(saveGameData);
            LoadItemEquipped(saveGameData);
            LoadBridge(saveGameData);
            LoadPotionAmount(saveGameData);
        }
        isLoading = false;
    }

    public void LoadTask(SaveGameData saveData)
    {
        TaskManager.Instance.Initialize(saveData.taskId);
    }

    public void LoadChestOpenObj(SaveGameData saveData)
    {
        foreach (int id in saveData.chestOpenedId)
        {
            Chest chest = GameManager.Instance.GetChest(id);
            chest.ChestOpened();
        }
    }

    public void LoadItemWolrd(SaveGameData saveData)
    {
        foreach (int id in saveData.itemWorldId)
        {
            ItemWorld chest = GameManager.Instance.GetItemWorld(id);
            chest.ItemWorldPickedUp();
        }
    }


    public void LoadItem(SaveGameData saveData)
    {
        foreach (string itemName in saveData.itemName)
        {
            ItemData itemData = GameManager.Instance.GetItemData(itemName);
            InventorySystem.Instance.AddToInventory(itemData);
        }
    }

    public void LoadItemEquipped(SaveGameData saveData)
    {
        if (saveData.itemEquippedName != null && saveData.itemEquippedName != "")
        {
            ItemData itemData = GameManager.Instance.GetItemData(saveData.itemEquippedName);
            EquipSystem.Instance.EquipWeapon(itemData);
        }
    }

    public void LoadBridge(SaveGameData saveData)
    {
        foreach (int id in saveData.bridgeId)
        {
            Bridge bridge = GameManager.Instance.GetBridge(id);
            bridge.BridgeFall();
        }
    }

    public void LoadPotionAmount(SaveGameData saveData)
    {
        EquipSystem.Instance.AddPotionItem(saveData.potionAmount);
    }

    #endregion

}
