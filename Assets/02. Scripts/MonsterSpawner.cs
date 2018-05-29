using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

	public GameObject shooterPrefab;
	public GameObject chargerPrefab;
	
	public void SpawnMonster(){
		int r = Random.Range(0, 2);
		if(r == 0) {
			SpawnShooter();
		} else if(r == 1) {
			SpawnCharger();
		}
	}

	private void SpawnShooter(){
		Vector3 screenPointPos = new Vector3(0, 0, 0);

		int r = Random.Range(0, 3);
		float position = (Random.Range(0, 9) + 1) / 10.0f;
		// 0 == Spawn Hozizontal shooter
		// 1 == Spawn left Vertical shooter
		// 2 == Spawn right Vertical shooter
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
		Vector3 screenPointPos = new Vector3(0, 0, 0);

		int r = Random.Range(0, 3);
		float position = (Random.Range(0, 9) + 1) / 10.0f;
		// 0 == Spawn up shooter
		// 1 == Spawn left shooter
		// 2 == Spawn right charger
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
