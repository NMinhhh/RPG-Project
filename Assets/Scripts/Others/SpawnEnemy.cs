using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Pos to spawn enemy melee attack")]
    [SerializeField] private List<Transform> meleeSpawnPos;
    private List<Transform> currentMeleeSpawnPos = new List<Transform>(); 
    
    [Header("Pos to spawn enemy range attack")]
    [SerializeField] private List<Transform> rangeSpawnPos;
    private List<Transform> currentRangeSpawnPos = new List<Transform>();

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
    //Enemy melee attack
    private Dictionary<int, List<GameObject>> enemyWaveDictionary;

    //Enemy in current wave
    private List<GameObject> currentEnemyWave;

    private int currentWave;

    public bool canSpawn;

    public bool isStartSpawn;

    public bool isFinishSpawn {  get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        currentEnemyWave = new List<GameObject>();
        enemyWaveDictionary = new Dictionary<int, List<GameObject>>();
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

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void StartSpawn()
    {
        currentWave = 1;
        canSpawn = true;
        isStartSpawn = true;
        isFinishSpawn = false;
    }

    public void NextWave()
    {
        currentWave++;
        if (currentWave > enemyWaveDictionary.Count)
        {
            canSpawn = false;
            isFinishSpawn = true;
            Invoke(nameof(Disable), 8);

        }
        isStartSpawn = true;
    }

    public bool CheckToNextWave()
    {
        int count = 0 ;
        foreach (GameObject enemy in currentEnemyWave)
        {
            if (enemy.GetComponentInChildren<Enemy>().isDie)
            {
                count++;
            }
        }


        return currentEnemyWave.Count == count;
    }

    void CreateWave()
    {
        currentMeleeSpawnPos.Clear();
        currentRangeSpawnPos.Clear();
        currentEnemyWave.Clear();
        currentEnemyWave = enemyWaveDictionary[currentWave];
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        for (int i = 0; i < currentEnemyWave.Count; i++)
        {
            if (currentEnemyWave[i].GetComponentInChildren<Enemy>().enemyType == Enemy.EnemyType.MeleeAttack)
                currentEnemyWave[i].transform.position = GetMeleeSpawnPos();
            else
                currentEnemyWave[i].transform.position = GetRangeSpawnPos();
            currentEnemyWave[i].GetComponentInChildren<EnemyAppearEffect>().StartAppear();
            currentEnemyWave[i].SetActive(true);
            yield return new WaitForSeconds(spawnDelayTime);
        }
        yield return null;
    }

    Vector3 GetRangeSpawnPos()
    {
        if(currentRangeSpawnPos.Count == 0)
        {
            currentRangeSpawnPos.AddRange(rangeSpawnPos);
        }
        int i = Random.Range(0,currentRangeSpawnPos.Count);
        Vector3 pos = currentRangeSpawnPos[i].position;
        currentRangeSpawnPos.RemoveAt(i);
        return pos;
    } 
    
    Vector3 GetMeleeSpawnPos()
    {
        if(currentMeleeSpawnPos.Count == 0)
        {
            currentMeleeSpawnPos.AddRange(meleeSpawnPos);
        }
        int i = Random.Range(0,currentMeleeSpawnPos.Count);
        Vector3 pos = currentMeleeSpawnPos[i].position;
        currentMeleeSpawnPos.RemoveAt(i);
        return pos;
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
            enemyWaveDictionary.Add(enemyToSpawnList.IndexOf(enemys) + 1, enemySpawnList);

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