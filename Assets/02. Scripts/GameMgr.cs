using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour {

	public int spawnTime = 7;

	private ShooterSpawnEffect shooterSpawn;

	// Use this for initialization
	void Start () {
		shooterSpawn = GetComponent<ShooterSpawnEffect>();
		StartCoroutine(StartSpawn());
	}
	
	IEnumerator StartSpawn() {
		
		while(true) {
			shooterSpawn.RandomSpawn();

			yield return new WaitForSeconds(spawnTime);
		}
	}
}
