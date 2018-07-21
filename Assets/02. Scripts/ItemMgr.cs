using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item {
	Immune,
	GaugeMult,
	HealthRegen
};

public class ItemMgr : MonoBehaviour {

	// Total number of items
	private int numOfItems = System.Enum.GetNames(typeof(Item)).Length;

	private PlayerCtrl player;
	private ItemSpawn itemSpawner;

	// Lifespan of items
	public float immuneLifespan = 7.0f;
	public float gaugeMultLifespan = 7.0f;
	public float healthRegenLifespan = 7.0f;
	
	// Duration(in usage) of items
	public float immuneDuration = 5.0f;
	public float gaugeMultDuration = 5.0f;

	// Multiplier for gauge fill amount
	public float gaugeMultiplier = 2.0f;
	
	// Amount of health to restore
	public int restoreAmount = 1;

	public ItemMgr instance { get; set; }

	void Start() {
		itemSpawner = GetComponent<ItemSpawn>();
		if(instance == null) {
			instance = this;
		}
	}

	public void SpawnItem() {
		itemSpawner.SpawnItem(numOfItems);
	}

	IEnumerator DoImmune() {
		player.SetImmune(true);
		yield return new WaitForSeconds(immuneDuration);
		player.SetImmune(false);
	}

	IEnumerator DoGaugeMult() {
		float originalFillAmount = player.fillEnergyAmount;
		float multipliedFillAmount = originalFillAmount * gaugeMultiplier;
		player.SetFillMult(multipliedFillAmount);
		yield return new WaitForSeconds(gaugeMultDuration);
		player.SetFillMult(originalFillAmount);

	}

	void DoHealthRegen() {
		player.RestoreHealth(restoreAmount);
	}

}