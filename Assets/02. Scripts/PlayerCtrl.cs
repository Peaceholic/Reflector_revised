using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
	private int currentHealth;
	private float currentEnergy; // Energy will be used for filling gauge
	private float curX;
	private float prevX;

	private SpriteRenderer spriteRenderer;

	public int CurrentHealth{
		get{
			return currentHealth;
		}
		set{
			currentHealth = value;
			if(currentHealth >= maxHealth){
				currentHealth = maxHealth;
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
	}
	public float CurrentEnergy{
		get{
			return currentEnergy;
		}
		set{
			
			if(currentEnergy <= maxEnergy){
				currentEnergy = value;
			}
			else{
				currentEnergy = maxEnergy;
			}
			
			UIMgr.Instance.ChangeGaugeFillAmountTo(currentEnergy / maxEnergy);
		}
	}

	[HideInInspector]
	public bool isDead;
	[HideInInspector]
	public bool immune;
	public int maxHealth = 2;
	public float maxEnergy = 3.0f;

	public float moveSpeed = 5.0f;
	public float fillEnergyAmount = 0.1f;
	public GameObject deathEffect;

	private JoystickPlayer joystick;
	private ItemMgr itemMgr;

	void Start()
	{
		joystick = FindObjectOfType<JoystickPlayer>();
		ResetStatus();

		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		itemMgr = FindObjectOfType<GameMgr>().ItemMgr.instance;
		prevX = transform.position.x;
		isDead = false;
		immune = false;
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
		if(other.gameObject.CompareTag("EnemyBullet") && !isDead) {
			ReceiveDamage(1);
			Destroy(other.gameObject);
		} else if(other.gameObject.CompareTag("Charger") && !isDead) {
			ReceiveDamage(currentHealth);
		} else if(other.gameObject.CompareTag("Shooter") && !isDead) {
			ReceiveDamage(currentHealth);
		} else if(other.gameObject.CompareTag("Item") && !isDead) {
			switch(other.gameObject.name) {
				case "Item_Immune":
				StartCoroutine(itemMgr.DoImmune());
				Destroy(other.gameObject);
				break;
				
				case "Item_GaugeMult":
				StartCoroutine(itemMgr.DoGaugeMult());
				Destroy(other.gameObject);
				break;

				case "Item_HealthRegen":
				itemMgr.DoHealthRegen();
				Destroy(other.gameObject);
				break;
			}
		}
	}
	
	void Move() {

		Vector2 moveDirJoystick, moveDirKeyboard;
		Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

        float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");

		moveDirJoystick = new Vector2(joystick.Horizontal, joystick.Vertical);
		moveDirKeyboard = (Vector3.up * vertical) + (Vector3.right * horizontal);

		if(pos.x < 0 && moveDirJoystick.x < 0) {
			moveDirJoystick.x = 0;
		}
		if(pos.x > Screen.width && moveDirJoystick.x > 0) {
			moveDirJoystick.x = 0;
		}
		if(pos.y < 0 && moveDirJoystick.y < 0) {
			moveDirJoystick.y = 0;
		}
		if(pos.y > Screen.height && moveDirJoystick.y > 0) {
			moveDirJoystick.y = 0;
		}

		if(moveDirKeyboard.magnitude > 0) {
			transform.Translate(moveDirKeyboard.normalized * Time.deltaTime * moveSpeed, Space.Self);
		} else {
			transform.Translate(moveDirJoystick * Time.deltaTime * moveSpeed, Space.Self);
		}
        
	}
	void ResetStatus(){
		currentHealth = maxHealth;
	}

	void ReceiveDamage(int amount){
		if(immune) {
			return;
		}
		currentHealth -= amount;
		
	}

	void Die(){
		// Player death
		isDead = true;
		GameMgr.Instance.Gamemode = GameModes.GameOver;
		Instantiate(deathEffect, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}

	public void SetImmune(bool status) {
		immune = status;
	}

	public void SetFillMult(float fillAmnt) {
		fillEnergyAmount = fillAmnt;
	}

	public void RestoreHealth(int restoreAmount) {
		int tempHealth = currentHealth + restoreAmount;
		if(tempHealth > maxHealth) {
			currentHealth = maxHealth;
		} else {
			currentHealth = tempHealth;
		}
	}
}
