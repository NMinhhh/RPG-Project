using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    public float gravity = -9.81f * 2;
    Vector3 v;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        v.y += gravity;
        characterController.Move(v * Time.deltaTime * Time.deltaTime);
    }
}
