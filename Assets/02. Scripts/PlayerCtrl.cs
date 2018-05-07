using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

	public int health = 2;

    void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "EnemyBullet") {
			health--;
		}
	}

}
