using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData",menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Health")]
    public float maxHealth;

    [Header("Speed")]
    public float speed;

    [Header("Jump")]
    public float gravity = -9.81f * 2;
    public float jumpHeight;

    [Header("Check ground")]
    public float radius;
    public LayerMask whatIsGround;
}
