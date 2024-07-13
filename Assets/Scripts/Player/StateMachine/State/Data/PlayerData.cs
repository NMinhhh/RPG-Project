using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData",menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Health")]
    public float maxHealth;

    [Header("Speed")]
    public float speed = 8;

    [Header("Jump")]
    public float gravity = -9.81f * 2;
    public float jumpSpeed = 10f; 
    public float jumpHeight = 2f;

    [Header("Check ground")]
    public float radius;
    public LayerMask whatIsGround;

    [Header("Attack")]
    public float attackMoveTime;

    [Header("Dodge")]
    public float dashSpeed = 30;
    public float dashTime = .5f;

    [Header("Strong Attack")]
    public float strongAttackMoveTime = .5f;
    public float readyTime = .5f;
}
