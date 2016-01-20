using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SirenBase : MonoBehaviour {
    
    private AudioSource Sound;
    private float Volume;
    private bool play = false;
    
    private List<GameObject> Enemies;

	void Start () {
        Volume = PlayerPrefs.GetFloat("sfx option");
        Sound = gameObject.GetComponent<AudioSource>();
        Sound.volume = Volume;
        Enemies = new List<GameObject>(0);
	}

    void Update()
    {
        if(Enemies.Count == 0)
        {
            Sound.Stop();
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
