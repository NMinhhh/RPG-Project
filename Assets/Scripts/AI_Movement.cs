using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 randomWaitTime;
    [SerializeField] private Vector2 randomRunTime;
    float waitTime;
    float runTime;
    bool isRunning;

    private Animator anim;

    Vector3 direction;
    Vector3 stopPos;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        runTime = Random.Range(randomRunTime.x, randomRunTime.y);
        ChooseDirection();
        transform.localRotation = Quaternion.Euler(direction);
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            anim.SetBool("isRunning", true);
            runTime -= Time.deltaTime;
            transform.position += transform.forward * speed * Time.deltaTime;

            if(runTime <= 0)
            {
                isRunning = false;
                stopPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
                transform.position = stopPos;
                anim.SetBool("isRunning", false);   
                waitTime = Random.Range(randomWaitTime.x, randomWaitTime.y);
            }
        }
        else
        {
            waitTime -= Time.deltaTime;

            if(waitTime <= 0)
            {
                runTime = Random.Range(randomRunTime.x, randomRunTime.y);
                isRunning = true;
                ChooseDirection();
                transform.localRotation = Quaternion.Euler(direction);
            }
        }
    }

    void ChooseDirection()
    {
        direction = new Vector3(0, Random.Range(0, 360), 0);
    }
}
