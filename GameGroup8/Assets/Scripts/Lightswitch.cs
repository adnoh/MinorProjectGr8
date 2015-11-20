using UnityEngine;
using System.Collections;
[RequireComponent(typeof (Light))]
public class Lightswitch : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		var Height = Daynight.Height;

		GetComponent<Light>().intensity = (Height > 0f) ? 0f : 3f;
		
	}
}
