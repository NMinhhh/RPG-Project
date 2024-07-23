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

    public bool strongAttackInput {  get; private set; }

    public bool blockInput {  get; private set; }

    public float mouseX {  get; private set; }

    public float mouseY { get; private set; }

    public bool aimInput { get; private set; }

    public bool canGetInput {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        CanGetInput();
    }

    // Update is called once per frame
    void Update()
    {
        if (!InventorySystem.instance.isOpen && canGetInput)
        {
            xInput = Input.GetAxisRaw("Horizontal");
            zInput = Input.GetAxisRaw("Vertical");
            jumpInput = Input.GetKey(KeyCode.Space);
            attackInput = Input.GetMouseButtonDown(0);
            pressTKey = Input.GetKeyDown(KeyCode.T);
            strongAttackInput = Input.GetMouseButton(1);
            blockInput = Input.GetKey(KeyCode.LeftShift);
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            aimInput = Input.GetKey(KeyCode.Tab);
        }
        else
        {
            xInput = 0;
            zInput = 0;
            jumpInput = false;
            attackInput = false;
            pressTKey = false;
            strongAttackInput = false;
            blockInput = false;
            mouseX = mouseY = 0;
            aimInput = false;
        }
        pressEKey = Input.GetKeyDown(KeyCode.E);

    }

    public void UseAttackInput() => attackInput = false;

    public void SetCanGetInput(bool state) => canGetInput = state;

    public void CanNotGetInput() => canGetInput = false;
    public void CanGetInput() => canGetInput = true;
}
