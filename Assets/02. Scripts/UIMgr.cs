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

	public GameObject titleUI;
	public GameObject pauseUI;
	public GameObject gameOverUI;
	public GameObject playingUI;
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

	public void OnClickTransparentBackground() {

		if (GameMgr.Instance.Gamemode == GameModes.Title) {

			GameMgr.Instance.Gamemode = GameModes.Playing;

		} else if (GameMgr.Instance.Gamemode == GameModes.Paused) {

			GameMgr.Instance.Gamemode = GameModes.Playing;

		} else if (GameMgr.Instance.Gamemode == GameModes.GameOver) {

			GameMgr.Instance.Gamemode = GameModes.Title;

		}

	}

	public void OnClickPauseButton() {

		GameMgr.Instance.Gamemode = GameModes.Paused;

	}

	public void StartUI() { 

		titleUI.SetActive(true);
		pauseUI.SetActive(false);
		gameOverUI.SetActive(false);


		playingUI.SetActive(false);

	}

	public void PlayUI() {

		titleUI.SetActive(false);
		pauseUI.SetActive(false);
		gameOverUI.SetActive(false);

		playingUI.SetActive(true);

	}

	public void PauseUI() {

		titleUI.SetActive(false);
		pauseUI.SetActive(true);
		gameOverUI.SetActive(false);

		playingUI.SetActive(false);

	}

	public void GameOverUI() {

		titleUI.SetActive(false);
		pauseUI.SetActive(false);
		gameOverUI.SetActive(true);

		playingUI.SetActive(false);

	}

}
