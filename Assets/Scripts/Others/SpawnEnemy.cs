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
    [SerializeField] private List<Wave> wavaList;

    [Header("Enemy Holder")]
    [SerializeField] private Transform enemyHolder;

    [Header("Delay time next wave")]
    [SerializeField] private float waveDelayTime;
    protected float currentWDT;

    [Header("delay time next enemy spawn")]
    [SerializeField] private float spawnDelayTime;

    //Enemy in current wave
    private List<Enemy> currentEnemyInWave;

    private int currentWave;

    public bool canSpawn;

    public bool isStartSpawn;

    public bool isFinishSpawn {  get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        currentEnemyInWave = new List<Enemy>();
        currentWDT = waveDelayTime;
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
        currentWave = 0;
        canSpawn = true;
        isStartSpawn = true;
        isFinishSpawn = false;
    }

    public void NextWave()
    {
        currentWave++;
        if (currentWave >= wavaList.Count)
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
        foreach (Enemy enemy in currentEnemyInWave)
        {
            if (enemy.isDie)
            {
                count++;
            }
        }


        return currentEnemyInWave.Count == count;
    }

    void CreateWave()
    {
        currentMeleeSpawnPos.Clear();
        currentRangeSpawnPos.Clear();
        currentEnemyInWave.Clear();
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        Wave wave = wavaList[currentWave];
        foreach (EnemyObject enemy in wave.enemyObjects)
        {
            for (int i = 0; i < enemy.size; i++)
            {
                Vector3 spawnPos;
                if(enemy.enemyType == EnemyObject.EnemyType.MeleeAttack)
                    spawnPos = GetMeleeSpawnPos();
                else
                    spawnPos = GetRangeSpawnPos();

                GameObject enemySpawn = ObjectPool.Instance.SpawnFromPool(enemy.name, spawnPos, Quaternion.identity);
                enemySpawn.name = enemy.name;
                enemySpawn.transform.SetParent(enemyHolder);

                if(!enemySpawn.GetComponent<Enemy>())
                    currentEnemyInWave.Add(enemySpawn.GetComponentInChildren<Enemy>());
                else
                    currentEnemyInWave.Add(enemySpawn.GetComponent<Enemy>());
                yield return new WaitForSeconds(spawnDelayTime);
            }
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

    public void ResetSpawn()
    {
        canSpawn = false;
        isStartSpawn = false;
        foreach (Enemy enemy in currentEnemyInWave)
        {
            if(!enemy.isDie)
                ObjectPool.Instance.AddInPool(enemy.transform.parent.name, enemy.transform.parent.gameObject);
            enemy.ResetEnemy();
        }
    }
}

[System.Serializable]
public class Wave
{
    public string name;

    public EnemyObject[] enemyObjects;

}

[System.Serializable]
public class EnemyObject
{
    public string name;
    public int size;
    public EnemyType enemyType;

    public enum EnemyType
    {
        MeleeAttack,
        RangeAttack
    }
}