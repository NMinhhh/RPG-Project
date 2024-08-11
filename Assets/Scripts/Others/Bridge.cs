using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour, IDamageable
{
    public int id;
    [SerializeField] private MeshRenderer[] skinnedMeshRenderers;
    [SerializeField] private float dissolveSpeed = 0.5f;
    private Material[] material;
    [SerializeField] private GameObject brigde;
    [SerializeField] private GameObject blockHolder;
    [SerializeField] private GameObject block;
    [SerializeField] private ParticleSystem fireEffect;
    [SerializeField] private GameObject explodeEffect;
    [SerializeField] private int damageAmount;
    private int currentDamageAmount;

    private Animator anim;
    private CapsuleCollider capsuleCollider;

    private bool isAppear;

    private float dissolveAmount;

    public bool isFinishFall { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        currentDamageAmount = damageAmount;
        dissolveAmount = 1;
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        GetMaterial();
        Appear();
        isFinishFall = false;
        capsuleCollider.enabled = false;
    }

    private void Update()
    {
        if (isAppear)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
            Appear();
            if (dissolveAmount == 0)
            {
                capsuleCollider.enabled = true;
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
        currentDamageAmount--;
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Damage Hit"), transform.position);
        if (currentDamageAmount <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isFinishFall = true;
        SaveManager.Instance.SaveBirdgeID(id);
        capsuleCollider.enabled = false;
        blockHolder.SetActive(false);
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Explosion"), transform.position);
        Instantiate(explodeEffect, blockHolder.transform.position, Quaternion.identity);
        block.GetComponent<Rigidbody>().isKinematic = false;
        anim.SetTrigger("Fall");
        Invoke(nameof(BlockDisable), 5f);
    }

    public void BlockDisable()
    {
        block.SetActive(false);
    }

    public void ResetBridge()
    {
        currentDamageAmount = damageAmount;
        fireEffect.Stop();
        dissolveAmount = 1;
        Appear();
        anim.SetTrigger("Idle");
        isFinishFall = false;
        capsuleCollider.enabled = false;
    }

    public void BridgeFall()
    {
        dissolveAmount = 0;
        Appear();
        capsuleCollider.enabled = false;
        blockHolder.SetActive(false);
        block.SetActive(false);
        anim.SetTrigger("Fall");
    }
}
