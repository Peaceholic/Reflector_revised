using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCtrl : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "EnemyBullet") {
			Destroy(other.gameObject);
		}
	}
}
