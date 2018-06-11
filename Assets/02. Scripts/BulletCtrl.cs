using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour {

    public GameObject DieEffect;

    void OnTriggerEnter2D(Collider2D other) {
		if((other.gameObject.tag == "Shooter") || (other.gameObject.tag == "Charger")) {
			Transform otherTr = other.transform;
			Destroy(other.gameObject);
			Instantiate(DieEffect, otherTr.position, Quaternion.identity);
		}
	}
}
