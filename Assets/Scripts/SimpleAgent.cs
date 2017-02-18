using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//public class Redirection
//{
//    public bool interrupt;
//    public Vector3 target;
//    public Func<bool> conditionSatisfied;
//    public Func<object, object> callback;
//    private float startTime = Time.time;
//    public float timeLimit;

//    public bool TimedOut()
//    {
//        return (Time.time - startTime) >= timeLimit;
//    }

//    Redirection(Transform target, Func<bool> Condition, AsyncCallback)
//};

[RequireComponent(typeof(NavMeshAgent))]
public abstract class SimpleAgent : MonoBehaviour {

    public GameObject Waypoint;
    public Transform activeTarget;
    public bool autoTargetPlayer;
    public float detectionDistance;
    public bool useLineOfSight;
    public bool useFOV;
    [ Range( 1 , 180 ) ]
    public float FOV;
    public float seekTime = 10;
    //[NonSerialized]
    //public Queue<Redirection> redirects;
    //private bool interuptinosHandled = false;

    protected NavMeshAgent agent;
    protected Vector3 currentTargetPosition;
    protected AgentController agentControl;

    private float timeLastSeen;
    private float _arrivalThreshold = 0.5f;
    
    // Use this for initialization
    protected virtual void Start () {
        agent = GetComponent<NavMeshAgent>();
        agentControl = GetComponentInChildren<AgentController>();
        if (autoTargetPlayer) activeTarget = GameObject.FindGameObjectWithTag("Player").transform;
 //       redirects = new Queue<Redirection>();
	}
	
	// Update is called once per frame
	 protected virtual void Update () {

        //if (redirects.Count > 0)
        //{
        //    HandleRedirects();
        //}

        if(agentControl.attacking)
        {
            SetDestination(transform.position);
        }
        else if (TargetAquired() || !PlayerSeekTimeout() /*&& redirects.Peek().interrupt*/)
        {
            SetDestination(activeTarget.position);
        }
        else
        {
            PassiveTask();
        }
        
	}

    private bool PlayerSeekTimeout()
    {
        return Time.time - timeLastSeen > seekTime;
    }

    private bool TargetAquired()
    {
        //Target Direction & magnitude
        var targetDirection = activeTarget.position - transform.position;
        if (targetDirection.magnitude > detectionDistance) return false;

        //If we want them to have direct sight do raycast to check
        RaycastHit hit = new RaycastHit();
        if (useLineOfSight && !Physics.Raycast(transform.position, targetDirection, out hit)) return false;
        else if (hit.transform != activeTarget) return false;

        //If we want the player to cross their FOV find angle
        if (useFOV && Mathf.Asin(Vector3.Dot(transform.forward.normalized, targetDirection.normalized)) > FOV) return false;

        //if above constraints are false then we have found our player
        timeLastSeen = Time.time;
        return true;
    }

    protected void SetDestination( Vector3 newTargetPos)
    {
        if (newTargetPos != currentTargetPosition)
        {
            agent.SetDestination(newTargetPos);
            currentTargetPosition = newTargetPos;
        }
    }

    protected bool ArrivedAt(Vector3 destination)
    {
        return Vector3.Distance(destination, transform.position) <= _arrivalThreshold;
    }
    protected bool FollowingActiveTarget()
    {
        return currentTargetPosition == activeTarget.position;
    }

    //when not seeking target
    protected abstract void PassiveTask();



    //private void HandleRedirects()
    //{
    //    var r = redirects.Peek();
    //    if (r.interrupt)
    //    {
    //        SetDestination(r.target);
    //    }

    //    //remove
    //    if (r.TimedOut() || r.conditionSatisfied())
    //    {
    //        r.interrupt = false;
    //        r.callback(this);
    //        redirects.Dequeue();
    //    }
    //}

}
