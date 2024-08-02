using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppearEffect : MonoBehaviour, IPooledObject
{
    [SerializeField] private SkinnedMeshRenderer[] skinnedMeshRenderers;
    [SerializeField] private float startDissolveAmount = 1;
    [SerializeField] private float dissolveSpeed = 0.5f;

    [SerializeField] private Enemy enemy;

    private Material[] material;

    private bool isAppear;

    private float dissolveAmount;

    private void Start()
    {
        GetMaterial();
    }

    private void Update()
    {
        if (isAppear)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
            Appear();
            if (dissolveAmount <= 0)
            {
                StopAppear();
            }
        }

    }

    void GetMaterial()
    {
        material = new Material[skinnedMeshRenderers.Length];
        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            material[i] = skinnedMeshRenderers[i].material;
            material[i].SetFloat("_Dissolve", 1);
        }
    }

    public void Appear()
    {
        foreach (Material m in material)
        {
            m.SetFloat("_Dissolve", dissolveAmount);
        }
    }

    public void StartAppear()
    {
        enemy.enabled = false;
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        dissolveAmount = startDissolveAmount;
        this.isAppear = true;
    }

    public void StopAppear()
    {
        enemy.enabled = true;
        isAppear = false;
        Collider collider = GetComponent<Collider>();
        collider.enabled = true;
    }

    public void OnObjectSpawn()
    {
        GetMaterial();
        StartAppear();
    }
}
