using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Audioslider : MonoBehaviour {

    public AudioSource backaudio;
    public Toggle mute;

    private Slider slider;

	void Start() {
        slider = gameObject.GetComponent<Slider> ();
		var temp = PlayerPrefs.GetFloat ("sound option");
		slider.value = temp;
        mute.isOn = PlayerPrefs.GetInt("sound mute") == 1 ? true : false;

    }

	// Update is called once per frame
	void Update () {

        
        var back = backaudio.GetComponent<AudioSource>();

		float temp = slider.value;
             
        back.volume = temp;

        if (mute.isOn == true)
        {
            back.mute = true;
            slider.interactable = false;
            PlayerPrefs.SetInt("sound mute", 1);
        }
        else if (mute.isOn == false)
        {
            back.mute = false;
            slider.interactable = true;
            PlayerPrefs.SetInt("sound mute", 0);
        }

        PlayerPrefs.SetFloat("sound option", temp);
		PlayerPrefs.Save();
		// Debug.Log (temp);
	}
}
