using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

	public GameObject shooterPrefab;
	public GameObject chargerPrefab;
	
	// Counts number of chargers after shooter has spawned
	private int chargerCount = 0;
	private int shooterCount = 0;

	public void SpawnMonster() {
		int r = Random.Range(0, 2);
		if((chargerCount >= 2) || (r == 0)) {
			SpawnShooter();
		} else if((r == 1) || (chargerCount >= 2)) {
			SpawnCharger();
		}
	}

	private void SpawnShooter(){
		
		// Reset charger count
		chargerCount = 0;
		shooterCount += 1;

		Vector3 screenPointPos = new Vector3(0, 0, 0);

		int r = Random.Range(0, 3);
		float position = (float)(Random.Range(0, 8) + 1.5) / 10.0f;
		// 0 == Spawn up
		// 1 == Spawn left 
		// 2 == Spawn right
		if(r == 0) {
			screenPointPos = new Vector3(position * Screen.width, 0.9f * Screen.height, 10.0f);
		} else if(r == 1) {
			screenPointPos = new Vector3(0.1f * Screen.width, position * Screen.height, 10.0f);
		} else if(r == 2) {
			screenPointPos = new Vector3(0.9f * Screen.width, position * Screen.height, 10.0f);
		}
		
		Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(screenPointPos);
		Instantiate(shooterPrefab, screenToWorld, Quaternion.identity);

	}

	private void SpawnCharger() {

		// Add 1 to charger counter
		chargerCount += 1;
		shooterCount = 0;

		Vector3 screenPointPos = new Vector3(0, 0, 0);

		int r = Random.Range(0, 3);
		float position = (float)(Random.Range(0, 8) + 1.5) / 10.0f;
		// 0 == Spawn up
		// 1 == Spawn left
		// 2 == Spawn right
		if(r == 0) {
			screenPointPos = new Vector3(position * Screen.width, 1.1f * Screen.height, 10.0f);
		} else if(r == 1) {
			screenPointPos = new Vector3(-0.1f * Screen.width, position * Screen.height, 10.0f);
		} else if(r == 2) {
			screenPointPos = new Vector3(1.1f * Screen.width, position * Screen.height, 10.0f);
		}
		
		Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(screenPointPos);
		Instantiate(chargerPrefab, screenToWorld, Quaternion.identity);
	}
}
