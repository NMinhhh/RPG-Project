using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public List<Pool> pools;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
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
            poolDictionary.Add(pool.name, queue);
        }
    }


    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(name))
        {
            Debug.Log(name);
            return null;
        }
        GameObject objectSpawn;
        if (poolDictionary[name].Count == 0) 
        {
            objectSpawn = Instantiate(GetPool(name).objectPref);
        }
        else
        {
            objectSpawn = poolDictionary[name].Dequeue();
        }
        objectSpawn.transform.SetParent(transform.root);
        objectSpawn.transform.position = position; 
        objectSpawn.transform.rotation = rotation;

        //Trigger fuction
        IPooledObject[] pooledObjectParent = objectSpawn.GetComponents<IPooledObject>();
        IPooledObject[] pooledObjectChild= objectSpawn.GetComponentsInChildren<IPooledObject>();
        List<IPooledObject> pooledObjects = new List<IPooledObject>();

        if(pooledObjectParent.Length > 0) 
            pooledObjects.AddRange(pooledObjectParent);
        if(pooledObjectChild.Length > 0)
            pooledObjects.AddRange(pooledObjectChild);

        foreach(IPooledObject pooledObject in pooledObjects)
        {
            pooledObject.OnObjectSpawn();
        }

        objectSpawn.SetActive(true);

        return objectSpawn;
    }

    public void AddInPool(string name, GameObject objectPool)
    {
        if (!poolDictionary.ContainsKey(name))
            return;
        objectPool.SetActive(false);
        objectPool.transform.SetParent(GetPool(name).objectHolder);
        poolDictionary[name].Enqueue(objectPool);

    }

    public Pool GetPool(string name)
    {
        foreach (Pool pool in pools)
        {
            if(pool.name == name)
                return pool;
        }
        return null;
    }
}
[System.Serializable]
public class Pool
{
    public string name;
    public GameObject objectPref;
    public int size;
    public Transform objectHolder;
}