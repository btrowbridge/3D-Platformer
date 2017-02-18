using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    public float EnemyPowerUpRatio = 2;
    public float PlayerPowerUpRatio = 1.5f;
    public int PowerUpIncrement = 100;
    public Text txtScore;
    private int Score = 0;

    private int powerUPCounter = 100;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Score > powerUPCounter)
        {
            PowerUp();
            powerUPCounter += PowerUpIncrement;
        }
	}

    public void ScorePoints(int points)
    {
        Score += points;
        txtScore.text = "Score: " + Score.ToString();
    }

    void PowerUp()
    {
        var Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var player = GameObject.FindGameObjectWithTag("Player");

        foreach(var enemy in Enemies)
        {
            var enemyHealth = enemy.GetComponent<Health>();
            enemyHealth.health = Mathf.RoundToInt(enemyHealth.health * EnemyPowerUpRatio);
            enemyHealth.maxHealth = Mathf.RoundToInt(enemyHealth.maxHealth * EnemyPowerUpRatio);
            enemyHealth.UpdateHealthBar();
           
        }

        var playerHealth = player.GetComponent<Health>();
        playerHealth.health = Mathf.RoundToInt(playerHealth.health * EnemyPowerUpRatio);
        playerHealth.maxHealth = Mathf.RoundToInt(playerHealth.maxHealth * EnemyPowerUpRatio);
        playerHealth.UpdateHealthBar();

    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("High Score", Score);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOver");
    }
}
