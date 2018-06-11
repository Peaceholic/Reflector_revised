using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour {
	private static UIMgr instance;
	public static UIMgr Instance{
		get{
			if(instance != null){
				return instance;
			}
			else{
				instance = FindObjectOfType<UIMgr>();
				return instance;
			}
		}
	}

	public Text scoreText;
	public Text vitalityText;
	public Image reflectorGauge;

	void Awake()
	{
		if(instance == null){
			instance = this;
		}
		else{
			Destroy(gameObject);
		}
	}

	public void ChangeScoreTo(int value) {
		scoreText.text = value.ToString();
	}

	public void ChangeVitalityTextTo(string text, Color color){
		vitalityText.text = text;
		vitalityText.color = color;
	}

	public void ChangeGaugeFillAmountTo(float amount){
		reflectorGauge.fillAmount = amount;
	}

	public void OnClickPauseButton() {	
		
	}
}
