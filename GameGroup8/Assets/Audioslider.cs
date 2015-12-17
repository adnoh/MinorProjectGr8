using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Audioslider : MonoBehaviour {


	// Update is called once per frame
	void Update () {

		var audio = Camera.main.GetComponent <AudioSource>();

	
		var slider = gameObject.GetComponent<Slider> ();

		Debug.Log (slider.value);
		float temp = slider.value;

		audio.volume = temp;

	}
}
