using UnityEngine;
using System.Collections;

public class SoundOutside : MonoBehaviour {

    private float Volume;
    private bool mute;

	void Start () {
        Volume = PlayerPrefs.GetFloat("sound option");
        mute = PlayerPrefs.GetInt("sound mute") == 1 ? true : false;

        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.volume = Volume;
        audio.mute = mute;
    }
}
