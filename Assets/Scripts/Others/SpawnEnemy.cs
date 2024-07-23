using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private Door door;

    [Header("Pos to spawn enemy")]
    [SerializeField] private Transform[] spawnPos;

    [Header("Enemy list to spawn")]
    [SerializeField] private List<EnemyToSpawn> enemyToSpawnList;

    [Header("Enemy Holder")]
    [SerializeField] private Transform enemyHolder;

    [Header("Delay time next wave")]
    [SerializeField] private float waveDelayTime;
    protected float currentWDT;

    [Header("delay time next enemy spawn")]
    [SerializeField] private float spawnDelayTime;

    //Current all wave to spawn
    private Dictionary<int, List<GameObject>> enemyDictionary;

    //Enemy in current wave
    private List<GameObject> currentEnemyList;

    private int currentWave;

    public bool canSpawn;

    public bool isStartSpawn;

    // Start is called before the first frame update
    void Start()
    {
        door.CloseDoor();
        currentEnemyList = new List<GameObject>();
        enemyDictionary = new Dictionary<int, List<GameObject>>();
        currentWave = 1;
        currentWDT = waveDelayTime;
        CreateAllEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSpawn) return;
        if (isStartSpawn)
        {
            CreateWave();
            isStartSpawn = false;
        }
        else
        {
            if (CheckToNextWave())
            {
                currentWDT -= Time.deltaTime;
                if (currentWDT <= 0)
                {
                    currentWDT = waveDelayTime;
                    NextWave();
                }
            }
        }
    }

    public void NextWave()
    {
        currentWave++;
        if (currentWave > enemyDictionary.Count)
        {
            canSpawn = false;
            door.OpenDoor();
            gameObject.SetActive(false);

        }
        isStartSpawn = true;
    }

    public bool CheckToNextWave()
    {
        int count = 0 ;
        foreach (GameObject enemy in currentEnemyList)
        {
            if (enemy.GetComponentInChildren<Enemy>().isDie)
            {
                count++;
            }
        }


        return currentEnemyList.Count == count;
    }

    void CreateWave()
    {
        currentEnemyList.Clear();
        currentEnemyList = enemyDictionary[currentWave];
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        for (int i = 0; i < currentEnemyList.Count; i++)
        {
            currentEnemyList[i].transform.position = spawnPos[Random.Range(0, spawnPos.Length)].position;
            currentEnemyList[i].GetComponentInChildren<EnemyAppearEffect>().StartAppear();
            currentEnemyList[i].SetActive(true);
            yield return new WaitForSeconds(spawnDelayTime);
        }
    }

    void CreateAllEnemy()
    {
        foreach (EnemyToSpawn enemys in enemyToSpawnList)
        {
            List<GameObject> enemySpawnList = new List<GameObject>();
            for (int i = 0; i < enemys.GetLength; i++)
            {
                for(int j = 0; j < enemys.GetSizeEnemyObject(i); j++)
                {
                    GameObject enemy = Instantiate(enemys.GetEnemy(i));
                    enemy.SetActive(false);
                    enemy.transform.SetParent(enemyHolder);
                    enemySpawnList.Add(enemy);
                }
            }
            enemyDictionary.Add(enemyToSpawnList.IndexOf(enemys) + 1, enemySpawnList);

        }
    }
}

[System.Serializable]
public class EnemyToSpawn
{
    public EnemyObject[] enemyObjects;

    public int GetLength
    {
        get
        {
            return enemyObjects.Length;
        }
    }

    public int GetSizeEnemyObject(int index)
    {
        return enemyObjects[index].size;
    }

    public GameObject GetEnemy(int index)
    {
        return enemyObjects[index].obj;
    }
}

[System.Serializable]
public class EnemyObject
{
    public GameObject obj;
    public int size;
}