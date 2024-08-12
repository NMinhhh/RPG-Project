using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>   
{
    [Header("Player")]
    [SerializeField] private Player player;
    [Header("Credit")]
    [SerializeField] private Credit credit;
    [Header("Respawn Point")]
    [SerializeField] private List<Transform> respawnPoint;
    [Header("Item Data")]
    [SerializeField] private List<ItemData> dataList;
    [Header("Object")]
    [SerializeField] private List<Chest> chestList;
    [SerializeField] private List<ItemWorld> itemWorldList;
    [SerializeField] private List<Bridge> bridgeList;

    public bool isPlaying {  get; private set; }
    public bool isLoss {  get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        isLoss = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowCredit()
    {
        credit.ShowCredit();
    }

    public void GamePlaying(bool state)
    {
        isPlaying = state;
    }

    public void SetGameState(bool state)
    {
        isLoss = state;
    }

    public void RespawnPlayer()
    {
        player.Respawn();
        SetGameState(false);
    }


    public Vector3 GetRespawnPoint()
    {
        return respawnPoint[TaskManager.Instance.currentTask].position;
    }

    public Chest GetChest(int id)
    {
        foreach (Chest chest in chestList)
        {
            if(chest.id == id)
                return chest;
        }
        return null;
    }

    public ItemData GetItemData(string name)
    {
        foreach (ItemData item in dataList)
        {
            if(item.name == name)
            {
                return item;
            }
        }
        return null;
    }

    public ItemWorld GetItemWorld(int id)
    {
        foreach (ItemWorld item in itemWorldList)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }
    
    public Bridge GetBridge(int id)
    {
        foreach (Bridge bridge in bridgeList)
        {
            if (bridge.id == id)
            {
                return bridge;
            }
        }
        return null;
    }
}
