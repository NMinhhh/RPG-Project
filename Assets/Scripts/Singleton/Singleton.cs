using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    public static T Instance
    {
        get
        {
            instance = (T)FindAnyObjectByType(typeof(T));
            if(instance == null)
            {
                GameObject obj = new GameObject();
                instance = obj.AddComponent<T>();
                obj.name = typeof(T).ToString();
            }

            return instance;
        }

    }

    private void Awake()
    {
        if(Instance != this as T && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this as T;
        }
    }
}
