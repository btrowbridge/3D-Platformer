using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour {

    public int attackDamage = 1;
    public float attackRange = 3;
    public float _animThreshold = 1.0f;

    [NonSerialized]
    public bool attacking = false;

    private bool canDamage = false;
    Vector3 previousPosition;

    Animator anim;
    Rigidbody rb;
    GameObject player;
    NavMeshAgent agent;

    
	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        previousPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
	}

    void FixedUpdate()
    {
        float speed = agent.velocity.magnitude;

        if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
        {
            Attack();
            anim.SetFloat("Walking", 0);
            anim.SetBool("Walk", false);
        }
        else
        {
            
            canDamage = false;
            anim.SetFloat("Walking", speed) ;
            anim.SetBool("Walk", speed > 0);
            
        }
        previousPosition = transform.position;
        attacking = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    }
	

    void Attack()
    {
        if (!attacking)
        {
            anim.SetTrigger("Attack");
            canDamage = true;
        }
        
    }
    
    void OnCollisionStay (Collision other)
    {
        if (canDamage && other.gameObject.tag == "Player")
        {
            Health playerHealth = other.gameObject.GetComponent<Health>();
            playerHealth.TakeDamage(attackDamage);
            canDamage = false;
        }
    }
    //void OnCollisionEnter(Collision other)
    //{
    //    if (canDamage && other.gameObject.tag == "Player")
    //    {
    //        Health playerHealth = other.gameObject.GetComponent<Health>();
    //        playerHealth.TakeDamage(attackDamage);
    //        canDamage = false;
    //    }
    //}
}
