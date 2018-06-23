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

	public GameObject startUI;
	public GameObject pauseUI;
	public GameObject gameOverUI;
	public GameObject gameCanvas;
	public GameObject menuEvent;
	public GameObject gameEvent;
	public GameObject player;
	public GameObject gameManager;

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

	public void OnClickTouchButton() {

		if (GameMgr.Instance.Gamemode == GameModes.start) {

			GameMgr.Instance.Gamemode = GameModes.play;

		} else if (GameMgr.Instance.Gamemode == GameModes.pause) {

			GameMgr.Instance.Gamemode = GameModes.play;

		} else if (GameMgr.Instance.Gamemode == GameModes.gameover) {

			GameMgr.Instance.Gamemode = GameModes.start;

		}

	}

	public void OnClickPauseButton() {

		GameMgr.Instance.Gamemode = GameModes.pause;

	}

	public void StartUI() { 

		startUI.SetActive(true);
		pauseUI.SetActive(false);
		gameOverUI.SetActive(false);
		menuEvent.SetActive(true);

		player.SetActive(false);
		gameCanvas.SetActive(false);
		gameEvent.SetActive(false);

	}

	public void PlayUI() {

		startUI.SetActive(false);
		pauseUI.SetActive(false);
		gameOverUI.SetActive(false);
		menuEvent.SetActive(false);

		player.SetActive(true);
		gameCanvas.SetActive(true);
		gameEvent.SetActive(true);

	}

	public void PauseUI() {

		startUI.SetActive(false);
		pauseUI.SetActive(true);
		gameOverUI.SetActive(false);
		menuEvent.SetActive(true);

		player.SetActive(false);
		gameCanvas.SetActive(false);
		gameEvent.SetActive(false);

	}

	public void GameOverUI() {

		startUI.SetActive(false);
		pauseUI.SetActive(false);
		gameOverUI.SetActive(true);
		menuEvent.SetActive(true);

		player.SetActive(false);
		gameCanvas.SetActive(false);
		gameEvent.SetActive(false);

	}

}
