using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

    public int collisionDamage = 5;
    public GameObject explosion;
    // Use this for initialization

    private float invulnerabilityTime = 0.01f;
	void Start () {
        invulnerabilityTime += Time.time;
        Destroy(gameObject, 10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        if (Time.time < invulnerabilityTime) return;

        var other = collision.gameObject;

        if (other.tag == "Enemy" || other.tag == "Player")
        {
            Health health = other.GetComponent<Health>();
            health.TakeDamage(collisionDamage);
        }
        Destroy(gameObject);
    }

    void OnDisable()
    {
        if (!this.enabled)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
