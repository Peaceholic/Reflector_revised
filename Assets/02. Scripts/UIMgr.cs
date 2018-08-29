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
	public GameObject guideUI;
	public GameObject highscoresUI;
	public GameObject creditsUI;
	public GameObject gameManager;

	public Text scoreText;
	public Text bestScoreText;
	public Text highScoreText;
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

	public void ChangeGaugeColor(Color color){
		reflectorGauge.color = color;
	}

	public void OnClickTransparentBackground() {

		if (GameMgr.Instance.Gamemode == GameModes.Paused) {

			GameMgr.Instance.Gamemode = GameModes.Playing;

		} else if (GameMgr.Instance.Gamemode == GameModes.GameOver) {

			SceneManager.LoadSceneAsync(0);

		} else if (GameMgr.Instance.Gamemode == GameModes.Guide) {

			GameMgr.Instance.Gamemode = GameModes.Title;

		} else if (GameMgr.Instance.Gamemode == GameModes.Highscores) {

			GameMgr.Instance.Gamemode = GameModes.Title;

		} else if (GameMgr.Instance.Gamemode == GameModes.Credits) {

			GameMgr.Instance.Gamemode = GameModes.Title;

		}

	}

	public void OnClickPauseButton() {

		GameMgr.Instance.Gamemode = GameModes.Paused;

	}

	public void OnClickPlayButton() {
		GameMgr.Instance.Gamemode = GameModes.Playing;
	}

	public void OnClickGuideButton() {
		GameMgr.Instance.Gamemode = GameModes.Guide;
	}

	public void OnClickHighscoresButton() {
		GameMgr.Instance.Gamemode = GameModes.Highscores;
	}

	public void OnclickCreditsButton() {
		GameMgr.Instance.Gamemode = GameModes.Credits;
	}

	public void StartUI() { 

		titleUI.SetActive(true);
		pauseUI.SetActive(false);
		gameOverUI.SetActive(false);
		guideUI.SetActive(false);
		highscoresUI.SetActive(false);
		creditsUI.SetActive(false);

		playingUI.SetActive(false);

		Time.timeScale = 1;

	}

	public void PlayUI() {

		titleUI.SetActive(false);
		pauseUI.SetActive(false);
		gameOverUI.SetActive(false);
		guideUI.SetActive(false);
		highscoresUI.SetActive(false);
		creditsUI.SetActive(false);

		playingUI.SetActive(true);

		Time.timeScale = 1;
	}

	public void PauseUI() {

		titleUI.SetActive(false);
		pauseUI.SetActive(true);
		gameOverUI.SetActive(false);
		guideUI.SetActive(false);
		highscoresUI.SetActive(false);
		creditsUI.SetActive(false);

		playingUI.SetActive(false);

		Time.timeScale = 0;

	}

	public void GuideUI() {

		titleUI.SetActive(false);
		pauseUI.SetActive(false);
		gameOverUI.SetActive(false);
		guideUI.SetActive(true);
		highscoresUI.SetActive(false);
		creditsUI.SetActive(false);

		playingUI.SetActive(false);

		Time.timeScale = 1;
	}

	public void CreditsUI() {

		titleUI.SetActive(false);
		pauseUI.SetActive(false);
		gameOverUI.SetActive(false);
		guideUI.SetActive(false);
		highscoresUI.SetActive(false);
		creditsUI.SetActive(true);

		playingUI.SetActive(false);

		Time.timeScale = 1;
	}

	public void HighscoresUI() {
		
		titleUI.SetActive(false);
		pauseUI.SetActive(false);
		gameOverUI.SetActive(false);
		guideUI.SetActive(false);
		highscoresUI.SetActive(true);
		creditsUI.SetActive(false);

		if(PlayerPrefs.HasKey("GameScore")){
			highScoreText.text = "Best: " + PlayerPrefs.GetInt("GameScore").ToString();
		}

		playingUI.SetActive(false);

		Time.timeScale = 1;
	}

	public void GameOverUI() {

		titleUI.SetActive(false);
		pauseUI.SetActive(false);
		gameOverUI.SetActive(true);
		guideUI.SetActive(false);
		highscoresUI.SetActive(false);
		creditsUI.SetActive(false);

		if(PlayerPrefs.HasKey("GameScore")){
			bestScoreText.text = "Best: " + PlayerPrefs.GetInt("GameScore").ToString();
		}
		else{
			bestScoreText.text = "Best: " + scoreText.text;
		}
		gameOverScoreText.text = scoreText.text;

		playingUI.SetActive(false);

		Time.timeScale = 1;

	}

}
