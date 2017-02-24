using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public int health = 20;
    public Image healthBar;
    public Text healthAmt;
    [NonSerialized]
    public int maxHealth;

    private GameManager gm;
    private Animator anim;
    private NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        maxHealth = health;
        UpdateHealthBar();
        gm = GameObject.FindObjectOfType<GameManager>();
        agent = GetComponent<NavMeshAgent>();
	}

    public void UpdateHealthBar()
    {
        healthAmt.text = health.ToString();
        healthBar.fillAmount = (float)health / maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthBar();
        if (health <= 0)
        {

            if (tag == "Enemy" && anim != null)
            {
                agent.Stop();
                anim.SetTrigger("Die");
                StartCoroutine(_Death());
                gm.ScorePoints(maxHealth);
            }
            else if (tag == "Player")
            {
                gm.GameOver();
            }
            else 
            {
                Destroy(gameObject);
                gm.ScorePoints(maxHealth);
            }
        }
    }

    private IEnumerator _Death()
    {
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Death")) { }
        yield return null;
        DestroyObject(gameObject, 1.0f);
       
    }
}
