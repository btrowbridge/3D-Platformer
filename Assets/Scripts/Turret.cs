using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public float targetDistance = 20.0f;
    public GameObject Ammo;
    public Transform AmmoSpawnPoint;
    //public AudioClip Audio;

    [Range(0, 1)]
    public float FireRate;
    [Range(2, 1000.0f)]
    public float FireForce;

    float nextShot = 0;
    GameObject player;
    Vector3 playerDir;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        playerDir = player.transform.position - AmmoSpawnPoint.position;

        if (playerDir.magnitude < targetDistance )
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.ProjectOnPlane(-playerDir,Vector3.up)), Time.deltaTime);
            if (Time.time >= nextShot && Vector3.Dot(playerDir.normalized, (-transform.forward).normalized) > 0)
            {
                //Debug.Log("Time: " + Time.time + ", nextDig: " + nextDig);
                RaycastHit hit;
                if (Physics.Raycast(AmmoSpawnPoint.position, playerDir, out hit))
                {
                    if (hit.collider.tag == "Player")
                    {
                        nextShot = Time.time + FireRate;
                        Shoot();
                    }
                }
            }
        }
    }
    void Shoot()
    {
        var bullet = GameObject.Instantiate(Ammo, AmmoSpawnPoint.position, Quaternion.LookRotation(playerDir));
        bullet.GetComponent<Rigidbody>().AddForce(playerDir * FireForce);
    }
}
