using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public int explosionDamage = 1;
    public float explosionDuration = 2;
    public float explosiveForce = 1000;
    void Awake()
    {
        Destroy(gameObject, explosionDuration);
    }

    void OnTriggerEnter(Collider other)
    {
        var go = other.gameObject;

        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Health health = go.GetComponent<Health>();
            health.TakeDamage(explosionDamage);

            Vector3 appliedForce = (go.transform.position - transform.position) * explosiveForce + Vector3.up;

            go.GetComponentInParent<Rigidbody>().AddForce(appliedForce);
            
        }
    }

}
