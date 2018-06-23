using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameModes {start, play, pause, gameover};

public class GameMgr : MonoBehaviour {
	private static GameMgr instance;
	public static GameMgr Instance{
		get{
			return instance;
		}
	}

	private GameModes gamemode;
	public GameModes Gamemode{
		get{
			return gamemode;
		}
		set{
			gamemode = value;
			switch(gamemode){
				case GameModes.start:

					UIMgr.Instance.StartUI();

					break;

				case GameModes.play:

					UIMgr.Instance.PlayUI();

					break;

				case GameModes.pause:

					UIMgr.Instance.PauseUI();

					break;

				case GameModes.gameover:

					UIMgr.Instance.GameOverUI();

					break;
			}
		}
	}
	public float spawnTime = 7;
	private int currentScore = 0;
	public int CurrentScore{
		get{
			return currentScore;
		}
		set{
			currentScore = value;
			UIMgr.Instance.ChangeScoreTo(currentScore);
		}
	}

	private MonsterSpawner monSpawner;

	void Awake(){
		if(instance == null){
			instance = this;
		}
		else{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		monSpawner = GetComponent<MonsterSpawner>();

		GameMgr.Instance.Gamemode = GameModes.start;

		StartCoroutine(StartScore());
		StartCoroutine(StartSpawn());
	}

	// normal score increase
	IEnumerator StartScore() {

		while(true && gamemode == GameModes.play) {

			++CurrentScore;

			yield return new WaitForSeconds(0.1f);

		}

	}
	
	IEnumerator StartSpawn() {
		
		while(true && gamemode == GameModes.play) {
			monSpawner.SpawnMonster();

			yield return new WaitForSeconds(spawnTime);
		}
	}

}
