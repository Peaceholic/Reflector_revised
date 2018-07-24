using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour {

	private static AudioMgr instance;
	public static AudioMgr Instance { get { return instance; } }

	void Awake() {
		if(instance == null) {
			instance = this;
			DontDestroyOnLoad(instance);
		}
		else{
			Destroy(this.gameObject);
		}
	}
}
