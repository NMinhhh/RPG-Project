using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour, IDamageable
{
    [SerializeField] private MeshRenderer[] skinnedMeshRenderers;
    [SerializeField] private float dissolveSpeed = 0.5f;
    private Material[] material;
    [SerializeField] private GameObject brigde;
    [SerializeField] private GameObject blockHolder;
    [SerializeField] private GameObject block;
    [SerializeField] private ParticleSystem fireEffect;
    [SerializeField] private GameObject explodeEffect;
    [SerializeField] private int amountOfDamage;

    private Animator anim;
    private CapsuleCollider capsuleCollider;

    private bool isAppear;

    private float dissolveAmount;

    public bool isFinishFall { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        GetMaterial();
        isFinishFall = false;
    }

    private void Update()
    {
        if (isAppear)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
            Appear();
            if (dissolveAmount == 0)
            {
                fireEffect.Play();
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

    void GetMaterial()
    {
        dissolveAmount = 1;
        material = new Material[skinnedMeshRenderers.Length];
        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            material[i] = skinnedMeshRenderers[i].material;
            material[i].SetFloat("_Dissolve", 1);
        }
    }

    void FinishAnimation()
    {
        isFinishFall = true;
    }

    public void Damage(float damage)
    {
        amountOfDamage--;
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound(Sound.SoundType.Hit), transform.position, .5f);
        if (amountOfDamage == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        capsuleCollider.enabled = false;
        blockHolder.SetActive(false);
        Instantiate(explodeEffect, blockHolder.transform.position, Quaternion.identity);
        block.GetComponent<Rigidbody>().isKinematic = false;
        anim.SetTrigger("Fall");
        Invoke(nameof(BlockDisable), 5f);
    }

    public void BlockDisable()
    {
        block.SetActive(false);
    }
}
