using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour {

	public int scoreAmount = 200;
    public GameObject DieEffect;

    void OnTriggerEnter2D(Collider2D other) {
		if((other.gameObject.tag == "Shooter") || (other.gameObject.tag == "Charger")) {
			GameMgr.Instance.CurrentScore += scoreAmount;
			Transform otherTr = other.transform;
			Destroy(other.gameObject);
			Instantiate(DieEffect, otherTr.position, Quaternion.identity);
		} else if(other.gameObject.tag == "EnemyBullet") {
			Destroy(other.gameObject);
		}
	}
}
