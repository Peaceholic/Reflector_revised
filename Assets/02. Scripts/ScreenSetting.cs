using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSetting : MonoBehaviour {

	void Awake()
	{
		Screen.orientation = ScreenOrientation.Landscape;
		Screen.SetResolution(Screen.width, Screen.width * 10 / 16, true);
	}
}
