using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
	
	public float moveSpeed = 7.0f;

	private float horizontal = 0.0f;
	private float vertical = 0.0f;
	
	// Update is called once per frame
	void Update () {
		horizontal = Input.GetAxisRaw("Horizontal");
		vertical = Input.GetAxisRaw("Vertical");

		Vector3 moveDir = (Vector3.up * vertical) + (Vector3.right * horizontal);

		transform.Translate(moveDir.normalized * Time.deltaTime * moveSpeed, Space.Self);
	}
}
