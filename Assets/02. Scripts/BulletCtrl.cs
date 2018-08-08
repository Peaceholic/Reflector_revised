using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour {

	public int scoreAmount = 200;
    public GameObject DieEffect;
	private BoxCollider2D boxCollider;
	private Animator anim;
	private PlayerCtrl player;
	private Renderer renderer;

	void Start() {
		boxCollider = GetComponent<BoxCollider2D>();
		anim = GetComponent<Animator>();
		player = GetComponentInParent<PlayerCtrl>();
		renderer = GetComponent<Renderer>();


		StartCoroutine(this.CheckEnd());
	}

	void Update() {
		CheckBound();
		
		Debug.Log(renderer.bounds.min);
		Debug.Log(renderer.bounds.max);
	}

	void CheckBound() {
		Vector3 screenWorldPoints = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

		

		//boxCollider.offset = new Vector2((newCoordX + 1) / 2, 0);
		//boxCollider.size = new Vector2((newCoordX + 1), 0.5f);
		
		float playerTrX = player.transform.position.x;
		float playerTrY = player.transform.position.y;
		float laserEndOfScreenDist = playerTrX > 0 ? playerTrX + screenWorldPoints.x : screenWorldPoints.x - playerTrX;
		boxCollider.offset = new Vector2((laserEndOfScreenDist + 1) / 2, 0);
		boxCollider.size = new Vector2((laserEndOfScreenDist + 1), 0.5f);
		
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
