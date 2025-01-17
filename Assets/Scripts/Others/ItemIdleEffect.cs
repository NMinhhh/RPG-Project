using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ItemIdleEffect : MonoBehaviour
{
    [Header("Particle")]
    [SerializeField] private ParticleSystem particleEffect;

    [Header("Item Obj")]
    [SerializeField] private GameObject[] items;
    [SerializeField] private float valueRotation;
    private float currentRotation;

    [Header("Translate Postion Y")]
    [SerializeField] private float maxTranslateTime;
    [SerializeField] private float speedTranslate;
    private float currentTranslateTime;
    private bool isChangDir;

    private Camera cam;


    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ItemRotation();
        TranslatePosY();
    }

    void ItemRotation()
    {
        currentRotation += valueRotation * Time.deltaTime;
        if(currentRotation >= 360)
            currentRotation = 0;
        foreach(GameObject item in items)
        {
            item.transform.localEulerAngles = new Vector3(item.transform.localEulerAngles.x, currentRotation, item.transform.localEulerAngles.z);
        }
    }

    void TranslatePosY()
    {
        if (isChangDir)
        {
            currentTranslateTime += speedTranslate * Time.deltaTime;
            if (currentTranslateTime >= maxTranslateTime)
                isChangDir = false;
        }
        else
        {
            currentTranslateTime -= speedTranslate * Time.deltaTime;
            if (currentTranslateTime <= 0)
                isChangDir = true;
        }
        foreach (GameObject item in items)
        {
            item.transform.localPosition = new Vector3(item.transform.localPosition.x, currentTranslateTime, item.transform.localPosition.z);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particleEffect.Stop();
        }
    }

   

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particleEffect.Play();
        }
    }
}
