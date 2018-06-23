using UnityEngine;
using UnityEngine.EventSystems;

public class ShieldAttack : MonoBehaviour, IPointerClickHandler {

	public RectTransform canvas;
	private ShieldCtrl shield;

	void Start()
	{
		shield = FindObjectOfType<ShieldCtrl>();
	}
	
	public void OnPointerClick(PointerEventData eventData) {
		shield.Attack();
		
	}
}
