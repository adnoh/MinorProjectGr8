using UnityEngine;
using System.Collections;
[RequireComponent(typeof (Light))]

/// <summary>
/// Class to switch the flashlight on/off
/// </summary>
public class Lightswitch : MonoBehaviour {

	void Update () {
		var Height = GameObject.Find("SUn").GetComponent<Daynight>().Height;
		GetComponent<Light>().intensity = (Height >= 100f) ? 0f : 3f;
	}
}
