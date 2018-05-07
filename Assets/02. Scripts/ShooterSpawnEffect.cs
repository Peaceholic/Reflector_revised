using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterSpawnEffect : MonoBehaviour {
	
	public GameObject shooterPrefab;
	public GameObject _shooterSpawnEffect;

	public void RandomSpawn() {
		Vector3 screenPointPos;

		int r = Random.Range(0, 3);
		float position = (Random.Range(0, 9) + 1) / 10.0f;
		// 0 == Spawn Vertical shooter
		// 1 == Spawn Horizontal shooter
		if(r == 0) {
			screenPointPos = new Vector3(position * Screen.width, 0.9f * Screen.height, 10.0f);
			Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(screenPointPos);
			Instantiate(_shooterSpawnEffect, screenToWorld, Quaternion.identity);
		} else if(r == 1) {
			screenPointPos = new Vector3(0.1f * Screen.width, position * Screen.height, 10.0f);
			Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(screenPointPos);
			Instantiate(_shooterSpawnEffect, screenToWorld, Quaternion.identity);
		} else if(r == 2) {
			screenPointPos = new Vector3(0.9f * Screen.width, position * Screen.height, 10.0f);
			Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(screenPointPos);
			Instantiate(_shooterSpawnEffect, screenToWorld, Quaternion.identity);
		}
	}

	public void Spawn() {
		Instantiate(shooterPrefab, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}
}
