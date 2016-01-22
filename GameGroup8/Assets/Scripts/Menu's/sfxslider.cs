using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sfxslider : MonoBehaviour
{

    public AudioSource zoomaudio;
    public Toggle mute;

    private Slider slider;

    void Start(){
        slider = gameObject.GetComponent<Slider>();
        var temp = PlayerPrefs.GetFloat("sfx option");
        slider.value = temp;
        mute.isOn = PlayerPrefs.GetInt("sfx mute") == 1 ? true : false;
    }

    // Update is called once per frame
    void Update(){
        var buttonaudio = Camera.main.GetComponent<AudioSource>();
		var zoom = zoomaudio.GetComponent<AudioSource> ();
        if (mute.isOn == true){
            PlayerPrefs.SetInt("sfx mute", 1);
            slider.interactable = false;
            zoom.mute = true;
            buttonaudio.mute = true;
        }else if (mute.isOn == false){
            PlayerPrefs.SetInt("sfx mute", 0);
            slider.interactable = true;
            zoom.mute = false;
            buttonaudio.mute = false;
        }
        float temp = slider.value;
        zoom.volume = temp;
        buttonaudio.volume = temp;
        PlayerPrefs.SetFloat("sfx option", temp);
        PlayerPrefs.Save();
    }
}
