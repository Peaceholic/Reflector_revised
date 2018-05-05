using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCtrl : MonoBehaviour {

	public float rotSpeed = 20.0f;

	private Transform parentTr;
	private float mouseX;
	private float mouseY;
	private const float distance = 1.7f;

	// Use this for initialization
	void Start () {
		// get parent's transform component
		parentTr = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		mouseX = Input.GetAxisRaw("Mouse X");
		mouseY = Input.GetAxisRaw("Mouse Y");
		float distBetweenTwo = Vector3.SqrMagnitude(parentTr.position - transform.position);

		Vector3 moveDir = (new Vector3(mouseX, mouseY ,0));
		transform.Translate(moveDir * Time.deltaTime * rotSpeed, Space.Self);

		if(distance != distBetweenTwo) {
			transform.position = (transform.position - parentTr.position).normalized * distance + parentTr.transform.position;
		}
	}
}
