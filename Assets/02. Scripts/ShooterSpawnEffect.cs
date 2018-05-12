using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterSpawnEffect : MonoBehaviour {
	
	public GameObject shooterPrefab;

	public void Spawn() {
		Instantiate(shooterPrefab, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}
}
