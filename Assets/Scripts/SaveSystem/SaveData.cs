using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public float musicVolume;
    public float soundVolume;
    public float sensetivity;
    public int taskId;
    public List<int> chestOpenedId = new List<int>();
    public List<string> itemName = new List<string>();
    public string itemEquippedName;
    public int potionAmount;
}
