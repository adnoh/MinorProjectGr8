using UnityEngine;
using System.Collections;

public class Tutorialcontrol : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var audio = Camera.main.GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("sound option");

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt ("grahpics option"), true);
    }
}
