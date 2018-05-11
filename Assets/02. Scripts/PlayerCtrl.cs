using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
	private int currentHealth;
	public int maxHealth = 2;

	void Start()
	{
		ResetStatus();
	}

    void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("EnemyBullet")) {
			ReceiveDamage(1);
		}
	}

	void ResetStatus(){
		currentHealth = maxHealth;
	}

	void ReceiveDamage(int amount){
		currentHealth -= 1;
		if(currentHealth <= 0){
			Die();
		}
	}

	void Die(){
		//Player death
	}
}
