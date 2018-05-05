using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieEffect : MonoBehaviour {

	void Die() {
		Destroy(this.gameObject);
	}
}