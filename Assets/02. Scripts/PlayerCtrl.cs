using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
	private int currentHealth;
	private float currentEnergy; // Energy will be used for filling gauge
	public float CurrentEnergy{
		get{
			return currentEnergy;
		}
		set{
			
			if(currentEnergy > value){
				currentEnergy = maxEnergy;
			}
			else{
				currentEnergy = value;
			}
			
			UIMgr.Instance.ChangeGaugeFillAmountTo(currentEnergy / maxEnergy);
		}
	}

	public int maxHealth = 2;
	public float maxEnergy = 3;

	public float moveSpeed = 5.0f;
	public float fillEnergyAmount = 0.1f;

	private JoystickPlayer joystick;

	void Start()
	{
		joystick = FindObjectOfType<JoystickPlayer>();
		ResetStatus();
	}

	void Update()
	{
		Move();
	}

    void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("EnemyBullet")) {
			ReceiveDamage(1);
		}
	}
	void Move() {
		Vector2 moveDir = new Vector2(joystick.Horizontal, joystick.Vertical);

		transform.Translate(moveDir * Time.deltaTime * moveSpeed, Space.Self);
        /*
        horizontal = Input.GetAxisRaw("Horizontal");
		vertical = Input.GetAxisRaw("Vertical");

		Vector3 moveDir = (Vector3.up * vertical) + (Vector3.right * horizontal);

        transform.Translate(moveDir.normalized * Time.deltaTime * moveSpeed, Space.Self);
        */
	}
	void ResetStatus(){
		currentHealth = maxHealth;
	}

	void ReceiveDamage(int amount){
		currentHealth -= 1;
		if(currentHealth >= maxHealth){
			UIMgr.Instance.ChangeVitalityTextTo("NORMAL", Color.white);
		}
		else if(currentHealth == 1){
			UIMgr.Instance.ChangeVitalityTextTo("FATAL", Color.red);
		}
		else if(currentHealth <= 0){
			UIMgr.Instance.ChangeVitalityTextTo("DEAD", Color.red);
			Die();
		}
	}

	void Die(){
		//Player death
	}
}
