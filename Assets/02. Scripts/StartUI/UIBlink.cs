using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlink : MonoBehaviour {
	public float duration = 1f;
	MaskableGraphic uiGraphic;
	void Awake(){
		uiGraphic = GetComponent<MaskableGraphic>();
	}

	void Start(){
		StartCoroutine(Blink());
	}

	IEnumerator Blink(){

		while(true){
			uiGraphic.CrossFadeAlpha(0, duration, true);
			yield return new WaitForSecondsRealtime(duration);

			uiGraphic.CrossFadeAlpha(1, duration, true);
			yield return new WaitForSecondsRealtime(duration);
		}
	}
	
	void OnEnable()
	{
		uiGraphic.color = Color.white;
	}
}
