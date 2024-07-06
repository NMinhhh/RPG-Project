using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMoveable 
{
    CharacterController character { get; set; }

    void Move(Vector3 direction);   

}
