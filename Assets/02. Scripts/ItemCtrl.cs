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
	public float immuneDuration = 8.0f;
	public float gaugeMultDuration = 6.0f;

	// Multiplier for gauge fill amount
	public float gaugeMultiplier = 2.0f;
	
	// Amount of health to restore
	public int restoreAmount = 1;
	
	// Immune coroutine
	private static Coroutine immuneCoroutine;
	private static Coroutine multCoroutine;

	public void ApplyItemEffect(PlayerCtrl player){
		switch(itemType){
			case ItemType.Immune:
				if(player.immune == true) {
					player.StopCoroutine(immuneCoroutine);
				}
				immuneCoroutine = player.StartCoroutine(player.ApplyImmune(immuneDuration));
			break;

			case ItemType.GaugeMult:
				if(player.multiplied == true) {
					player.StopCoroutine(multCoroutine);
				}
				multCoroutine = player.StartCoroutine(player.ApplyGaugeMult(gaugeMultiplier, gaugeMultDuration));
			break;

			case ItemType.HealthRegen:
				player.ApplyHealthRegen(restoreAmount);
			break;

			default:
			break;
		}
		Destroy(this.gameObject);
	}
}
