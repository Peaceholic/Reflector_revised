using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour {

	public Text scoreText; //ScroeText
	private int score = 0; //inital score

	// Use this for initialization
	void Start () {

		AddScore(0); //initialize score
		StartCoroutine(TimerScore());
		
	}
	
	// Update is called once per frame
	void Update () {
		

	}

	// Add value to score
	void AddScore(int value) {

		score += value;
		scoreText.text = score.ToString(); //set new value

	}

	// Score added by 1 every 1 seconds
	IEnumerator TimerScore() {

		while(true) {

			yield return new WaitForSeconds(1.0f);
			AddScore(1);
			
		}

	}

	public void OnClickPauseBtn() {

		

		
	}
}
