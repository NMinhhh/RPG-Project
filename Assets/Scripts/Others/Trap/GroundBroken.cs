using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBroken : MonoBehaviour, IPooledObject
{
    [SerializeField] private Transform[] groundTiles;
    private Vector3[] startPos;
    private Rigidbody[] rids;

    private void Start()
    {
        startPos = new Vector3[groundTiles.Length];
        rids = new Rigidbody[groundTiles.Length];
        for(int i = 0; i < groundTiles.Length; i++)
        {
            startPos[i] = groundTiles[i].localPosition;
            rids[i] = groundTiles[i].GetComponent<Rigidbody>();
        }
    }
 
    void ReGroundBroken()
    {
        this.gameObject.SetActive(false);
        ObjectPool.Instance.AddInPool("GroundBroken", this.gameObject);
    }

    public void OnObjectSpawn()
    {
        Invoke(nameof(ReGroundBroken), 4);
        if (startPos == null) return;
        for (int i = 0; i < groundTiles.Length; i++)
        {
            rids[i].velocity = Vector3.zero;
            rids[i].angularVelocity = Vector3.zero;
            groundTiles[i].localPosition = startPos[i];
        }
    }
}
