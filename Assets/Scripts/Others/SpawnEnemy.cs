using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPos;

    [SerializeField] private List<EnemyToSpawn> enemyToSpawnList;

    [SerializeField] private Transform enemyHolder;

    [SerializeField] private float spawnDelayTime;

    private Dictionary<int, List<GameObject>> enemyDictionary;

    private List<GameObject> currentEnemyList;

    private int currentWave;

    public bool canSpawn;

    public bool isStartSpawn;

    // Start is called before the first frame update
    void Start()
    {
        currentEnemyList = new List<GameObject>();
        enemyDictionary = new Dictionary<int, List<GameObject>>();
        currentWave = 1;
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
                NextWave();
            }
        }
    }

    public void NextWave()
    {
        currentWave++;
        if (currentWave > enemyDictionary.Count)
        {
            canSpawn = false;
        }
        isStartSpawn = true;
    }

    public bool CheckToNextWave()
    {
        int count = 0 ;
        foreach (GameObject enemy in currentEnemyList)
        {
            if (!enemy.activeInHierarchy)
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