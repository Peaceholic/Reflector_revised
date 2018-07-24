using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item {
	Immune,
	GaugeMult,
	HealthRegen
};

public class ItemMgr : MonoBehaviour {

	public PlayerCtrl player;
	private ItemSpawn itemSpawner;
	
	// Duration(in usage) of items
	public float immuneDuration = 5.0f;
	public float gaugeMultDuration = 5.0f;

	// Multiplier for gauge fill amount
	public float gaugeMultiplier = 2.0f;
	
	// Amount of health to restore
	public int restoreAmount = 1;

	private ItemMgr instance;
	public ItemMgr Instance {
		get{
			return instance;
		}
	}

	void Awake() {

		if(instance == null) {
			instance = this;
		}
		else{
			Destroy(this.gameObject);
		}
		
	}

	void Start() {
		itemSpawner = GetComponent<ItemSpawn>();

	}

	public GameObject SpawnItem() {
		return itemSpawner.SpawnItem();
	}

	public IEnumerator DoImmune() {
		player.SetImmune(true);
		yield return new WaitForSeconds(immuneDuration);
		player.SetImmune(false);
	}

	public IEnumerator DoGaugeMult() {
		float originalFillAmount = player.fillEnergyAmount;
		float multipliedFillAmount = originalFillAmount * gaugeMultiplier;
		player.SetFillMult(multipliedFillAmount);
		yield return new WaitForSeconds(gaugeMultDuration);
		player.SetFillMult(originalFillAmount);

	}

	public void DoHealthRegen() {
		player.RestoreHealth(restoreAmount);
	}

	public void SetPlayer() {
		player = FindObjectOfType<PlayerCtrl>();
	}

	

}