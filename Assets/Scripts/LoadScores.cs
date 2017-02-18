using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadScores : MonoBehaviour {

    Text scoreText;
	// Use this for initialization
	void Start () {
        scoreText = GetComponent<UnityEngine.UI.Text>();

        scoreText.text = (PlayerPrefs.HasKey("HighScore")) ? PlayerPrefs.GetInt("High Score").ToString() : "No Scores Loaded!";

	}
	

}
