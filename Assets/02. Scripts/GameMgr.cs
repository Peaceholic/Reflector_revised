using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameModes {Title, Playing, Paused, GameOver};

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
			var prevGamemode = gamemode;
			gamemode = value;
			switch(gamemode){
				case GameModes.Title:

					UIMgr.Instance.StartUI();
					player.SetActive(false);
					break;

				case GameModes.Playing:

					UIMgr.Instance.PlayUI();
					player.SetActive(true);

					if(prevGamemode == GameModes.Title){
						StartCoroutine(StartSpawn());
					}

					StartCoroutine(StartScore());
					break;

				case GameModes.Paused:

					UIMgr.Instance.PauseUI();
					break;

				case GameModes.GameOver:

					UIMgr.Instance.GameOverUI();
					player.SetActive(false);
					break;
			}
		}
	}
	public float spawnTime = 7;
    public float initialspawnTime = 5;
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

	public GameObject player;

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

		GameMgr.Instance.Gamemode = GameModes.Title;

	}

	// normal score increase
	IEnumerator StartScore() {

		while(gamemode == GameModes.Playing) {

			++CurrentScore;

			yield return new WaitForSeconds(0.1f);

		}

	}
	
	IEnumerator StartSpawn() {

		while(true) {
			if(gamemode == GameModes.Playing){
				monSpawner.SpawnMonster();
                spawnTime = initialspawnTime - CurrentScore / 2500;
                if(spawnTime > 1)
                {
                    yield return new WaitForSeconds(spawnTime);
                }
                else
                {
                    spawnTime = 1;
                    yield return new WaitForSeconds(spawnTime);
                }
			}
			else if(gamemode == GameModes.Paused){
				yield return new WaitUntil(()=>gamemode == GameModes.Playing);
			}
			else if(gamemode == GameModes.GameOver){
				break;
			}

		}
	}

}
