using UnityEngine;
using System.Collections;

/// <summary>
/// Class to change the volume of the sound to the player prefs
/// </summary>
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
