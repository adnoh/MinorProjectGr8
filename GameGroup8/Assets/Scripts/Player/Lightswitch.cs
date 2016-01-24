using UnityEngine;
using System.Collections;
[RequireComponent(typeof (Light))]

/// <summary>
/// Class to switch the flashlight on/off
/// </summary>
public class Lightswitch : MonoBehaviour {

    public float brightness;

    void Update () {
		var Height = GameObject.Find("SUn").GetComponent<Daynight>().getHeigth();
		GetComponent<Light>().intensity = (Height >= 150f) ? 0f : brightness;
	}
}
