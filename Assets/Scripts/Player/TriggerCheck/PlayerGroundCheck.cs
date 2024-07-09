using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    private Player _player;

    [SerializeField] private float radius;

    [SerializeField] private LayerMask whatIsGround;

    private bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics.CheckSphere(transform.position, radius, whatIsGround);


        _player.CheckGround(isGround);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
