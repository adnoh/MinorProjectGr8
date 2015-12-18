﻿using UnityEngine;
using System.Collections;
[RequireComponent(typeof (Light))]
public class Lightswitch : MonoBehaviour {

	void Update () {
		var Height = Daynight.Height;
		GetComponent<Light>().intensity = (Height > 30f) ? 0f : 3f;
	}
}
