using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public float xInput {  get; private set; }

    public float zInput {  get; private set; }

    public bool jumpInput {  get; private set; }

    public bool attackInput {  get; private set; }

    public bool pressEKey {  get; private set; }

    public bool pressTKey {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!InventorySystem.instance.isOpen)
        {
            xInput = Input.GetAxisRaw("Horizontal");
            zInput = Input.GetAxisRaw("Vertical");
            jumpInput = Input.GetKey(KeyCode.Space);
            attackInput = Input.GetMouseButtonDown(0);
            pressTKey = Input.GetKeyDown(KeyCode.T);
        }
        else
        {
            xInput = 0;
            zInput = 0;
            jumpInput = false;
            attackInput = false;
            pressTKey = false;
        }
        pressEKey = Input.GetKeyDown(KeyCode.E);

    }

    public void UseAttackInput() => attackInput = false;
}
