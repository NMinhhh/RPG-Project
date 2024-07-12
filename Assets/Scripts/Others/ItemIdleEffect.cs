using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ItemIdleEffect : MonoBehaviour
{
    [Header("Particle")]
    [SerializeField] private ParticleSystem _particleSystem;

    [Header("Canvas")]
    [SerializeField] private Canvas _canvas;

    [Header("Item Obj")]
    [SerializeField] private GameObject _item;
    [SerializeField] private float _valueRotation;
    private float _currentRotation;

    [Header("Translate Postion Y")]
    [SerializeField] private float _maxTranslateTime;
    [SerializeField] private float _speedTranslate;
    private float _currentTranslateTime;
    private bool isChangDir;

    private Camera _camera;


    void Start()
    {
        _camera = Camera.main;
        _canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ItemRotation();
        TranslatePosY();
    }

    void ItemRotation()
    {
        _currentRotation += _valueRotation * Time.deltaTime;
        if(_currentRotation >= 360)
            _currentRotation = 0;
        _item.transform.localRotation = Quaternion.Euler(0, _currentRotation, 0);
    }

    void TranslatePosY()
    {
        if (isChangDir)
        {
            _currentTranslateTime += _speedTranslate * Time.deltaTime;
            if (_currentTranslateTime >= _maxTranslateTime)
                isChangDir = false;
        }
        else
        {
            _currentTranslateTime -= _speedTranslate * Time.deltaTime;
            if (_currentTranslateTime <= 0)
                isChangDir = true;
        }
        _item.transform.localPosition = new Vector3(0, _currentTranslateTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _particleSystem.Stop();
            _canvas.transform.rotation = Quaternion.LookRotation(_canvas.transform.position - _camera.transform.position);
            _canvas.gameObject.SetActive(true);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canvas.transform.rotation = Quaternion.LookRotation(_canvas.transform.position - _camera.transform.position);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _particleSystem.Play();
            _canvas.gameObject.SetActive(false);

        }
    }
}
