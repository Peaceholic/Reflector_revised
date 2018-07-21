using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item {
	Immune,
	GaugeMult,
	HealthRegen
};

public class ItemMgr : MonoBehaviour {

	public Item feature;
	// Total number of items
	private int numOfItems = System.Enum.GetNames(typeof(Item)).Length;

	private PlayerCtrl player;

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

	IEnumerator SpawnItem() {
		while(true) {
			
			int seconds = Random.Range(0, 6) + 5;
			int mseconds = Random.Range(0, 100);
			float itemSpawnTime = seconds + (float)mseconds / 100.0f;

			yield return new WaitForSeconds(itemSpawnTime);

			if(player == null) {
				yield break;
			}

			int itemType = Random.Range(0, numOfItems);
			switch(itemType) {
				case 0:
				feature = Item.Immune;
				/* Instantiate immune item */
				break;

				case 1:
				feature = Item.GaugeMult;
				/* Instantiate gaugemult item */
				break;
				
				case 2: 
				feature = Item.HealthRegen;
				/* Instantiate healthregen item */
				break;
			}			
		}
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