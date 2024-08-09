using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>   
{
    [SerializeField] private List<Chest> chestList;
    [SerializeField] private List<ItemData> dataList;
    [SerializeField] private List<Transform> respawnPoint;
    public bool isLoss {  get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void RespawnPlayer(Player player)
    {
        player.gameObject.transform.position = respawnPoint[TaskManager.Instance.currentTask - 1].position;
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
}
