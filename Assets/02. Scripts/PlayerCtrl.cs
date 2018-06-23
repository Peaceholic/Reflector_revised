using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
	private int currentHealth;
	private float currentEnergy; // Energy will be used for filling gauge
	private float curX;
	private float prevX;

	private SpriteRenderer spriteRenderer;

	public float CurrentEnergy{
		get{
			return currentEnergy;
		}
		set{
			
			if(currentEnergy > maxEnergy){
				currentEnergy = maxEnergy;
			}
			else{
				currentEnergy = value;
			}
			
			UIMgr.Instance.ChangeGaugeFillAmountTo(currentEnergy / maxEnergy);
		}
	}

	[HideInInspector]
	public bool isDead;
	public int maxHealth = 2;
	public float maxEnergy = 3;

	public float moveSpeed = 5.0f;
	public float fillEnergyAmount = 0.1f;
	public GameObject deathEffect;

	private JoystickPlayer joystick;

	void Start()
	{
		joystick = FindObjectOfType<JoystickPlayer>();
		ResetStatus();

		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		prevX = transform.position.x;
	}

	void DoFlip() {
		curX = transform.position.x;
		float dir = curX - prevX;
		if(dir < 0) {
			spriteRenderer.flipX = true;
		} else if(dir > 0) {
			spriteRenderer.flipX = false;
		} else {
			// Do nothing
		}
		prevX = curX;
	}

	void Update()
	{
		Move();
		DoFlip();
	}

    void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("EnemyBullet")) {
		if(other.gameObject.CompareTag("EnemyBullet") && !isDead) {
			ReceiveDamage(1);
		}
	}
	void Move() {
		Vector2 moveDir = new Vector2(joystick.Horizontal, joystick.Vertical);
		Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

		if(pos.x < 0 && moveDir.x < 0) {
			moveDir.x = 0;
		}
		if(pos.x > Screen.width && moveDir.x > 0) {
			moveDir.x = 0;
		}
		if(pos.y < 0 && moveDir.y < 0) {
			moveDir.y = 0;
		}
		if(pos.y > Screen.height && moveDir.y > 0) {
			moveDir.y = 0;
		}

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
		// Player death
		//Player death
		isDead = true;
		Instantiate(deathEffect, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}
}
