using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrlWithJoystick : MonoBehaviour {

	protected JoystickPlayer joystick;
	public float moveSpeed = 5.0f;

	// Use this for initialization
	void Start () {
		joystick = FindObjectOfType<JoystickPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

	void Move() {
		Vector2 moveDir = new Vector2(joystick.Horizontal, joystick.Vertical);

		transform.Translate(moveDir * Time.deltaTime * moveSpeed, Space.Self);
        /*
        horizontal = Input.GetAxisRaw("Horizontal");
		vertical = Input.GetAxisRaw("Vertical");

		Vector3 moveDir = (Vector3.up * vertical) + (Vector3.right * horizontal);

        transform.Translate(moveDir.normalized * Time.deltaTime * moveSpeed, Space.Self);
        */
	}
}
