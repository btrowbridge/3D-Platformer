using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomAgent : SimpleAgent {


    public float radiusOfChange;

    public float randomTimeout = 5;

    private float randomTimer;
    // Use this for initialization
    protected override void Start () {
        base.Start();
	}

    protected override void PassiveTask()
    {


        if (FollowingActiveTarget() || ArrivedAt(currentTargetPosition) || Timeout())
        {
            randomTimer = Time.time;
            SetRandomNavLocation();
        }

    }

    private bool Timeout()
    {
        return Time.time - randomTimer > randomTimeout;
    }

    private void SetRandomNavLocation()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radiusOfChange;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, radiusOfChange, 1);
        Vector3 finalPosition = hit.position;

        SetDestination(finalPosition);
    }
}
