using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGameData 
{
    public int taskId = 0;
    public List<int> chestOpenedId = new List<int>();
    public List<int> itemWorldId = new List<int>();
    public List<string> itemName = new List<string>();
    public List<int> bridgeId = new List<int>();
    public string itemEquippedName;
    public int potionAmount;
}

[System.Serializable]
public class SettingData
{
    public float musicVolume = 1;
    public float soundVolume = 1;
    public float sensetivity = 60;
}