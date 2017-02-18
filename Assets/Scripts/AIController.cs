
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class AIController : ZombieController 
{
    [Range(0, 10)]
    public float RandomizeRate = 10.0f;
    [Range(0, 1)]
    public float followPlayerVarience = 0.2f;
    [Range(0, 1000)]
    public float followRange = 100;
    public float attackRange;

    private Vector3 mMoveDireciton;
    private float mNextTurn = 0;
    private Transform player;
    private Vector3 playerDirection;
    private Vector3 randomOffset;

    void Start()
    {
        mMoveDireciton = transform.forward * accelleration;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        playerDirection = player.position - transform.position;
    }
    void Update()
    {
        if (player && Time.time >= mNextTurn)
        {
            mNextTurn = Time.time + RandomizeRate;
            RandomizeDirection();
        }

        if (playerDirection.magnitude <= attackRange)
            Attack();

    }

    new void FixedUpdate()
    {
        playerDirection = player.position - transform.position;
        if (Physics.Raycast(transform.position, playerDirection, followRange))
        {
            Move(mMoveDireciton);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(mMoveDireciton), Time.fixedDeltaTime);


        }
        else
        {
            Move(Vector3.zero);
        }



    }

    private void RandomizeDirection()
    {
        float x = Random.Range(-followPlayerVarience, followPlayerVarience);
        float y = 0.0f;
        float z = Random.Range(-followPlayerVarience, followPlayerVarience);

        randomOffset = new Vector3(x, y, z).normalized;

        mMoveDireciton = Vector3.ProjectOnPlane(playerDirection,Vector3.up) + randomOffset;
    }

    
}

