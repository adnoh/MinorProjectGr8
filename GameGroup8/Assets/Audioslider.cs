using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Audioslider : MonoBehaviour {
	void Start() {
		var slider = gameObject.GetComponent<Slider> ();
		var temp = PlayerPrefs.GetFloat ("sound option");
		slider.value = temp;


	}

	// Update is called once per frame
	void Update () {

		var audio = Camera.main.GetComponent <AudioSource>();

	
		var slider = gameObject.GetComponent<Slider> ();


		float temp = slider.value;

		audio.volume = temp;

		PlayerPrefs.SetFloat("sound option", temp);
		PlayerPrefs.Save();
		Debug.Log (temp);

	}
}
