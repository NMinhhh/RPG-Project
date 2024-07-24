using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppearEffect : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] skinnedMeshRenderers;

    [SerializeField] private float dissolveSpeed = 0.5f;

    [SerializeField] private Enemy enemy;
    [SerializeField] private GameObject healthBar;

    private Material[] material;

    private bool isAppear;

    private float dissolveAmount;


    private void Start()
    {
        enemy.enabled = false;
        healthBar.SetActive(false);
        GetMaterial();
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

    private void Update()
    {
        if (isAppear)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
            Appear();
            if(dissolveAmount == 0)
            {
                healthBar.SetActive(true);
                enemy.enabled = true;
                isAppear = false;
            }
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
        this.isAppear = true;
        dissolveAmount = 1;
    }
    
}
