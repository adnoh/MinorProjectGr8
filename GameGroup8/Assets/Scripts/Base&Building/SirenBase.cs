using UnityEngine;
using System.Collections;

public class SirenBase : MonoBehaviour {

    public AudioClip Siren;

    private AudioSource Sound;
    private float Volume;
    private bool play = false;

	void Start () {
        Volume = PlayerPrefs.GetFloat("sfx option");
        Sound = gameObject.AddComponent<AudioSource>();
        Sound.clip = Siren;
        Sound.volume = Volume;
	}
	
	void onTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            play = true;
            Sound.Play();
        }
    }

    void onTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            play = false;
            Sound.Stop();
        }
    }
}
