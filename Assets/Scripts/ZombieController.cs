using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    public float accelleration;
    public float maxVelocity; 

    protected float m_Threshold = 0.1f;
    private Rigidbody rb;
    private Animator anim;
    
    private bool isAttacking = false;
    private Vector3 movementDir;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }
    public void FixedUpdate()
    {

        Vector3 forceDirecton = Input.GetAxis("Horizontal") * Camera.main.transform.right + Input.GetAxis("Vertical") * Camera.main.transform.forward;


        Move(forceDirecton);

        if (isAttacking)
            rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forceDirecton), Time.fixedDeltaTime * accelleration);

    }

    protected void Move(Vector3 forceDirecton)
    {
        forceDirecton = Vector3.ProjectOnPlane(forceDirecton, transform.up);

        if (forceDirecton.magnitude > m_Threshold && rb.velocity.magnitude <= maxVelocity)
        {

            rb.velocity = transform.forward * accelleration * Time.fixedDeltaTime;
            anim.SetBool("Walk", true);
            movementDir = forceDirecton;
        }
        else
        {
            anim.SetBool("Walk", false);
        }

    }
    protected void Attack()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {   
            anim.SetTrigger("Attack");
        }
    }
    
}

