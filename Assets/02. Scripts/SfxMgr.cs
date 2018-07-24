using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxMgr : MonoBehaviour {

	private static SfxMgr instance;
	public static SfxMgr Instance { get { return instance; } }

	private AudioSource audioSource;

	public List<AudioClip> sfxList;

	void Awake() {
		if(instance == null) {
			instance = this;
			DontDestroyOnLoad(instance);
		}
		else{
			Destroy(this.gameObject);
		}
	}

	void Start() {
		audioSource = GetComponent<AudioSource>();
	}

	public void PlayEffect(int type) {
		// Plays the sfx on an event
		// 0: charger attack
		// 1: monster defeated
		// 2: player defeated
		// 3: shield absorb enemy bullet
		// 4: shield attack (level 1)
		// 5: shield attack (level 2)
		// 6: shield attack (level 3)
		// 7: on touch event
		audioSource.PlayOneShot(sfxList[type]);
	}
}
