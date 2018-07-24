using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
	Immune,
	GaugeMult,
	HealthRegen
};

public class ItemCtrl : MonoBehaviour {
	public ItemType itemType;

	// Duration(in usage) of items
	public float immuneDuration = 5.0f;
	public float gaugeMultDuration = 5.0f;

	// Multiplier for gauge fill amount
	public float gaugeMultiplier = 2.0f;
	
	// Amount of health to restore
	public int restoreAmount = 1;

	public void ApplyItemEffect(PlayerCtrl player){
		switch(itemType){
			case ItemType.Immune:
				player.StartCoroutine(player.ApplyImmune(immuneDuration));
			break;

			case ItemType.GaugeMult:
				player.StartCoroutine(player.ApplyGaugeMult(gaugeMultiplier, gaugeMultDuration));
			break;

			case ItemType.HealthRegen:
				player.ApplyHealthRegen(restoreAmount);
			break;

			default:
			break;
		}
		Destroy(this.gameObject);
	}

	private IEnumerator ApplyGaugeMult(PlayerCtrl player){
		float originalFillAmount = player.fillEnergyAmount;
		float multipliedFillAmount = originalFillAmount * gaugeMultiplier;
		player.SetFillMult(multipliedFillAmount);
		yield return new WaitForSeconds(gaugeMultDuration);
		player.SetFillMult(originalFillAmount);
	}
}
