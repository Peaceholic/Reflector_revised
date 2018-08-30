using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerExplosionCtrl : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "EnemyBullet") {
			ObjectPool.Instance.DestroyObject(coll.gameObject);
		} else if(coll.gameObject.tag == "Player") {
			coll.GetComponent<PlayerCtrl>().ReceiveDamage(1);
        }
    }

}
