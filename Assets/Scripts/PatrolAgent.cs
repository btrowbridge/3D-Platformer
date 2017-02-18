using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAgent : SimpleAgent {


    public List<Transform> patrolLocations;


    private Transform patrolTarget;

    private int patrolIndex = 0;

    // Use this for initialization
    protected override void Start() {
        base.Start();
        patrolTarget = patrolLocations[patrolIndex];
        SetDestination(patrolTarget.position);
    }

    protected override void PassiveTask() {


        if (ArrivedAt(patrolTarget.position))
        {
            GetNextPatrolSpot();
        }
        else
        {
            SetDestination(patrolTarget.position);
        }

    }

    //Circular List
    private void GetNextPatrolSpot()
    {
        var i = patrolIndex++ % patrolLocations.Count;
        patrolTarget = patrolLocations[i];
        SetDestination(patrolTarget.position);
    }
}
