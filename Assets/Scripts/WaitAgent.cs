﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAgent : SimpleAgent {


    public bool returnToStart;
    Transform initialTransform;

    
     protected override void Start()
    {
        base.Start();
        if (initialTransform == null)
        {
            initialTransform = GameObject.Instantiate(Waypoint,transform.position,Quaternion.identity).transform;
        }

    }

    protected override void PassiveTask()
    {
        if (returnToStart)
        {
            if (ArrivedAt(initialTransform.position))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, initialTransform.rotation, Time.deltaTime);
            }
            else
            {
                SetDestination(initialTransform.position);
            }
        }
        else
        {
            SetDestination(transform.position);
        }
    }

}
