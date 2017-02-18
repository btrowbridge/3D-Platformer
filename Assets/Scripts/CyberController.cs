using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberController : MonoBehaviour {


    // the amount to transition when using head look
    public float jumpSpeed;
    public float turnSpeed;
    public float moveSpeed;
    public float animSpeed = 1.5f;             
    public float lookSmoother = 3f;            
    public float minimumFallingHeight = 0.5f;  

    private bool downRayHit = false;
    private float heightAboveGround = 0f;


    Animator anim;                         
    AnimatorStateInfo currentBaseState;        
    AnimatorStateInfo layer2CurrentState;  
    Collider col;                  
    Rigidbody rb;

    private float netSpeed;
    private float netDirection;
    private bool netFalling;
    private bool netJump;
    private float netStrafe;
    private bool netLoco;

    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int locoState = Animator.StringToHash("Base Layer.Locomotion");          
    static int jumpState = Animator.StringToHash("Base Layer.Jump");               
    static int jumpDownState = Animator.StringToHash("Base Layer.JumpDown");       
    static int fallState = Animator.StringToHash("Base Layer.Fall");
    static int rollState = Animator.StringToHash("Base Layer.Roll");
    static int waveState = Animator.StringToHash("Layer2.Wave");
    


    // Use this for initialization
    void Start () {
        // initialising reference variables
        anim = GetComponent<Animator>();
        col = GetComponentInChildren<Collider>();
        rb = GetComponent<Rigidbody>();
        if (anim.layerCount >= 2)
            anim.SetLayerWeight(1, 1);
        Debug.Log("Anim layer count: " + anim.layerCount);
    }
	
	void Update () {

    }

    void FixedUpdate ()
	{
		if (!anim.avatar || !anim.avatar.isValid)
			return;

		anim.speed = animSpeed;								
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0); 
        if (anim.layerCount == 2)
            layer2CurrentState = anim.GetCurrentAnimatorStateInfo(1);   

        Movement();

		if (currentBaseState.fullPathHash == locoState)
		{
            netJump = false;
			if (Input.GetKeyDown(KeyCode.Space))
			{
                rb.AddForce(Vector3.up * jumpSpeed + rb.velocity);
				netJump = true;
			}
		}
        

        anim.SetLayerWeight(1, 1);

        netLoco = (anim.GetFloat("Speed") > 0 || anim.GetFloat("Strafe") != 0);
        if (Input.GetKeyDown(KeyCode.LeftShift) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
        {
            rb.AddForce(transform.forward * jumpSpeed );

            anim.SetTrigger("Dash");
        }
        
        anim.SetFloat("Direction", netDirection); 
        anim.SetFloat("Speed", netSpeed); 				
        anim.SetBool("Falling", netFalling);
        anim.SetBool("Jump", netJump);
        anim.SetFloat("Strafe", netStrafe);
        anim.SetBool("Loco", netLoco);
            

	}

    private void Movement()
    {
        var mouseDir = 0;
        if (Input.GetAxis("Mouse X") > 0){
            mouseDir = 1;
        }
        else if (Input.GetAxis("Mouse X") < 0)
        {
            mouseDir = -1;
        }

        netDirection = Mathf.Lerp(netDirection,mouseDir,Time.deltaTime);         
        netSpeed = -Input.GetAxis("Vertical");
        netStrafe = Input.GetAxis("Horizontal");
        
        Ray downRay = new Ray( Vector3.up + transform.position, -Vector3.up);
        RaycastHit downHitInfo = new RaycastHit();

        //bool downRayHit = Physics.Raycast(downRay, out downHitInfo);
        downRayHit = Physics.Raycast(downRay, out downHitInfo);
        heightAboveGround = 0f;
        if (downRayHit)
        {
            heightAboveGround = downHitInfo.distance;
            if (heightAboveGround > minimumFallingHeight)
            {
                netFalling = true;
            }
            else
            {
                netFalling = false;
            }
        }
        else
        {
            netFalling = false;
        }
        rb.AddForce(new Vector3(netStrafe, 0.0f, netSpeed) + transform.forward * netSpeed * moveSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up)), Time.deltaTime * turnSpeed);
    }

}
