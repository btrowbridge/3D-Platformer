using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform target;
    public float Damping = 1;

    private Vector3 offset;         
    // Use this for initialization
    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        offset = target.transform.position - transform.position;
        
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        //Look at and dampen the rotation
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = Vector3.Lerp(transform.position, target.transform.position - (rotation * offset), Time.deltaTime * Damping);
        transform.LookAt(target.transform);

    }
}
