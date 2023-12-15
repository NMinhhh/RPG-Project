using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float groundRadius;
    private float gravity = -9.81f * 2f;

    [SerializeField] private Transform checkGround;
    [SerializeField] private LayerMask whatIsGround;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    void Jump()
    {
        if(CheckGrounded() && velocity.y < 0) 
        {
            velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && CheckGrounded())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    bool CheckGrounded()
    {
        return Physics.CheckSphere(checkGround.position, groundRadius, whatIsGround);
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);
    }
}
