using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour {
	private GameMgr instance;
	public GameMgr Instance{
		get{
			return instance;
		}
	}
	public float spawnTime = 7;
	public int scoreValue = 0;

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

			++scoreValue;

			UIMgr.Instance.ChangeScoreTo(scoreValue);

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
