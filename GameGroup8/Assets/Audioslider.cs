using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Audioslider : MonoBehaviour {

  
    public AudioSource backaudio;
	void Start() {
		var slider = gameObject.GetComponent<Slider> ();
		var temp = PlayerPrefs.GetFloat ("sound option");
		slider.value = temp;


	}

	// Update is called once per frame
	void Update () {

        
        var back = backaudio.GetComponent<AudioSource>();


        var slider = gameObject.GetComponent<Slider> ();


		float temp = slider.value;
      
       
        back.volume = temp;

        PlayerPrefs.SetFloat("sound option", temp);
		PlayerPrefs.Save();
		// Debug.Log (temp);

	}
}
