using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{

    public Dictionary<Pool.Type, Queue<GameObject>> poolDictionary;

    public List<Pool> pools;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<Pool.Type, Queue<GameObject>>();
        CreatePool();
    }

    void CreatePool()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject objectPool = Instantiate(pool.objectPref);
                objectPool.SetActive(false);
                objectPool.transform.SetParent(pool.objectHolder);
                queue.Enqueue(objectPool);
            }
            poolDictionary.Add(pool.type, queue);
        }
    }


    public GameObject SpawnFromPool(Pool.Type type, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            return null;
        }
        GameObject objectSpawn;
        if (poolDictionary[type].Count == 0) 
        {
            objectSpawn = Instantiate(GetPool(type).objectPref);
        }
        else
        {
            objectSpawn = poolDictionary[type].Dequeue();
        }
        objectSpawn.transform.SetParent(transform.root);
        objectSpawn.transform.position = position; 
        objectSpawn.transform.rotation = rotation;
        IPooledObject pooledObject = objectSpawn.GetComponent<IPooledObject>();
        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }
        objectSpawn.SetActive(true);
        return objectSpawn;
    }

    public void AddInPool(Pool.Type type, GameObject objectPool)
    {
        objectPool.SetActive(false);
        objectPool.transform.SetParent(GetPool(type).objectHolder);
        poolDictionary[type].Enqueue(objectPool);

    }

    public Pool GetPool(Pool.Type type)
    {
        foreach (Pool pool in pools)
        {
            if(pool.type == type)
                return pool;
        }
        return null;
    }
}
[System.Serializable]
public class Pool
{
    public enum Type
    {
        BloodParticle,
        Sound,
        Arrow,
        NoticePickUp,
        GhoulProjectile,
        SwordSlash,
    }

    public Type type;
    public GameObject objectPref;
    public int size;
    public Transform objectHolder;
}