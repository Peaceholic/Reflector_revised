using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour {
	private static GameMgr instance;
	public static GameMgr Instance{
		get{
			return instance;
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
		StartCoroutine(StartScore());
		StartCoroutine(StartSpawn());
	}

	// normal score increase
	IEnumerator StartScore() {

		while(true) {

			++CurrentScore;

			yield return new WaitForSeconds(0.1f);

		}

	}
	
	IEnumerator StartSpawn() {
		
		while(true) {
			monSpawner.SpawnMonster();

			yield return new WaitForSeconds(spawnTime);
		}
	}
}
