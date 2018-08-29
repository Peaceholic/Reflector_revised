using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour {

	public int scoreAmount = 200;
    public GameObject DieEffect;
	private BoxCollider2D boxCollider;
	private Animator anim;
	private PlayerCtrl player;
	private ShieldCtrl shield;

	void Start() {
		boxCollider = GetComponent<BoxCollider2D>();
		anim = GetComponent<Animator>();
		player = GetComponentInParent<PlayerCtrl>();
		shield = GetComponentInParent<ShieldCtrl>();

		StartCoroutine(this.CheckEnd());
	}

	void Update() {
		CheckBound();
	}

	void CheckBound() {
		Vector3 screenWorldPoints = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

		float eulerZ = shield.transform.eulerAngles.z;
		float sizeY = 1;
		float sizeX = 1;
		float newSize = 1;
		if((0 <= eulerZ) && (eulerZ < 180)) {
			sizeY = screenWorldPoints.y - shield.transform.position.y;
		} else {
			sizeY = screenWorldPoints.y + shield.transform.position.y;
		}

		
		if(((0 <= eulerZ) && (eulerZ < 90)) || ((270 <= eulerZ) && (eulerZ < 360))) {
			sizeX = screenWorldPoints.x - shield.transform.position.x;
		} else {
			sizeX = screenWorldPoints.x + shield.transform.position.x;
		}

		sizeY++; sizeX++;

		newSize = Mathf.Lerp(sizeY, sizeX, Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * eulerZ)));

		boxCollider.offset = new Vector2((newSize + 0.5f) / 2, 0);
		boxCollider.size = new Vector2((newSize + 0.5f), 0.5f);
	}

	IEnumerator CheckEnd() {

		yield return new WaitUntil(() => (player.CurrentEnergy < 0.1) == true);
		anim.SetTrigger("Disappear");
	}


	void DestroyOnEnd() {
		Destroy(this.gameObject);
	}

    void OnTriggerEnter2D(Collider2D other) {
		if((other.gameObject.tag == "Shooter") || (other.gameObject.tag == "Charger")) {
			GameMgr.Instance.CurrentScore += scoreAmount;
			Transform otherTr = other.transform;
			Destroy(other.gameObject);
			Instantiate(DieEffect, otherTr.position, Quaternion.identity);
		} else if(other.gameObject.tag == "EnemyBullet") {
			Destroy(other.gameObject);
		}
	}
}
