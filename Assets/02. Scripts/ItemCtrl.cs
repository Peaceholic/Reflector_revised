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

	public void ApplyItemEffect(){
		switch(itemType){
			case ItemType.Immune:

			break;

			case ItemType.GaugeMult:

			break;

			case ItemType.HealthRegen:

			break;

			default:

			break;
		}
	}
}
