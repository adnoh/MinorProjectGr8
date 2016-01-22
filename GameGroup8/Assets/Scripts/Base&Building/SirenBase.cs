using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Sound for the siren of the base.
/// The siren sounds when an enemy is near
/// </summary>
public class SirenBase : MonoBehaviour {
    
    private AudioSource Sound;
    private float Volume;
    private bool mute;
    private bool play = false;
    
    private List<GameObject> Enemies;

	void Start () {
        Volume = PlayerPrefs.GetFloat("sfx option");
        mute = PlayerPrefs.GetInt("sfx mute") == 1 ? true : false;

        Sound = gameObject.GetComponent<AudioSource>();
        Sound.volume = Volume;
        Sound.mute = mute;
        Enemies = new List<GameObject>(0);
	}

    /// <summary>
    /// Check if there are enemies in range.
    /// If none the siren stops
    /// </summary>
    void Update()
    {
        if(Enemies.Count == 0)
        {
            Sound.Stop();
            play = false;
        }
        else if (Enemies.Count != 0)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                GameObject enemy = Enemies[i];
                if (enemy == null)
                {
                    Enemies.Remove(enemy);
                }
            }
        }
    }

    /// <summary>
    /// When an enemy is close the list of enemies grows.
    /// WHen the list is not 0, the siren sounds
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Enemies.Add(col.gameObject);
            if (play == false)
            {
                play = true;
                Sound.Play();
            }
        }
    }

    /// <summary>
    /// Remove enemy from the list if it walks away from the base
    /// </summary>
    /// <param name="col"></param>
    void onTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Enemies.Remove(col.gameObject);
            play = false;
            Sound.Stop();
        }
    }
}
