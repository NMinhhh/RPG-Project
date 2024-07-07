using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public float XInput {  get; private set; }

    public float ZInput {  get; private set; }

    public bool JumpInput {  get; private set; }

    public bool AttackInput {  get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        XInput = Input.GetAxisRaw("Horizontal");
        ZInput = Input.GetAxisRaw("Vertical");
        JumpInput = Input.GetKey(KeyCode.Space);
        AttackInput = Input.GetMouseButtonDown(0);

    }


}
