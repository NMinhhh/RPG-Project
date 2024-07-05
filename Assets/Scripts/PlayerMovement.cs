using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    [SerializeField] protected float speed;
    [SerializeField] private Camera cam;

    [SerializeField] private float smoothTimer;
    private float currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(xInput, 0, zInput).normalized;

        if(direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTimer * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            Vector3 motion = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            character.Move(motion * speed * Time.deltaTime);
        }
       
    }
}
