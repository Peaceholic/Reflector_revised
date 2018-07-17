using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	public Text gameOverScoreText;
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

			SceneManager.LoadSceneAsync(0);

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

		Time.timeScale = 1;

	}

	public void PlayUI() {

		titleUI.SetActive(false);
		pauseUI.SetActive(false);
		gameOverUI.SetActive(false);

		playingUI.SetActive(true);

		Time.timeScale = 1;
	}

	public void PauseUI() {

		titleUI.SetActive(false);
		pauseUI.SetActive(true);
		gameOverUI.SetActive(false);

		playingUI.SetActive(false);

		Time.timeScale = 0;

	}

	public void GameOverUI() {

		titleUI.SetActive(false);
		pauseUI.SetActive(false);
		gameOverUI.SetActive(true);

		gameOverScoreText.text = scoreText.text;

		playingUI.SetActive(false);

		Time.timeScale = 1;

	}

}
