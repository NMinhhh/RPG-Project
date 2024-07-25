using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemyBomerang : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float moveTowardTime;
    [SerializeField] private float rotationZValue;
    [SerializeField] private float damage = 10f;
    private float currentRotationZ;
    private float startRotaionZ;
    private bool canMove;
    private Vector3 startPos;
    private bool isChangeDir;
    public bool isFinsishMove {  get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        startRotaionZ = transform.rotation.z;
        currentRotationZ = startRotaionZ;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            moveTowardTime -= Time.deltaTime;
            if(moveTowardTime > 0)
            {
                transform.position += direction * speed * Time.deltaTime;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position,startPos,speed * Time.deltaTime);
                float distance = Vector3.Distance(transform.position, startPos);
                if (distance <= .1f)
                {
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, startRotaionZ);
                    transform.position = startPos;
                    isFinsishMove = true;
                    canMove = false;
                }
            }
        }
        RoationZ();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable damagaeble = other.GetComponent<IDamageable>();
            if (damagaeble != null)
            {
                damagaeble.Damage(damage);
            }
        }
    }

    void RoationZ()
    {
        currentRotationZ += rotationZValue;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y, currentRotationZ);
    }

    public void SetBomerang(Vector3 direction, float damage, float speed, float moveTowardTime, Vector3 startPos)
    {
        isFinsishMove = false;
        canMove = true;
        this.direction = direction;
        this.damage = damage;
        this.speed = speed;
        this.moveTowardTime = moveTowardTime;
        this.startPos = startPos;
    }

}
