﻿using UnityEngine;

public class SoundsEnemies : MonoBehaviour {

    public AudioClip[] Sharky;
    public AudioClip[] Firefox;
    public AudioClip[] DesertEagle;
    public AudioClip[] PolarBear;
    public AudioClip[] Oilfant;
    public AudioClip[] Meep;

    private float Volume;

    void Start()
    {
        Volume = PlayerPrefs.GetFloat("sfx option");
    }

    public AudioSource[] loadSharkSounds(GameObject enemy)
    {
        AudioSource[] sounds = new AudioSource[Sharky.Length];

        for (int i = 0; i < Sharky.Length; i++)
        {
            sounds[i] = enemy.AddComponent<AudioSource>();
            sounds[i].clip = Sharky[i];
            sounds[i].volume = Volume;
        }

        return sounds;
    }

    public AudioSource[] loadFireFoxSounds(GameObject enemy)
    {
        AudioSource[] sounds = new AudioSource[Firefox.Length];

        for (int i = 0; i < Firefox.Length; i++)
        {
            sounds[i] = enemy.AddComponent<AudioSource>();
            sounds[i].clip = Firefox[i];
            sounds[i].volume = Volume;
        }

        return sounds;
    }

    public AudioSource[] loadEagleSounds(GameObject enemy)
    {
        AudioSource[] sounds = new AudioSource[DesertEagle.Length];

        for (int i = 0; i < DesertEagle.Length; i++)
        {
            sounds[i] = enemy.AddComponent<AudioSource>();
            sounds[i].clip = DesertEagle[i];
            sounds[i].volume = Volume;
        }

        return sounds;
    }

    public AudioSource[] loadSPolarBearSounds(GameObject enemy)
    {
        AudioSource[] sounds = new AudioSource[PolarBear.Length];

        for (int i = 0; i < PolarBear.Length; i++)
        {
            sounds[i] = enemy.AddComponent<AudioSource>();
            sounds[i].clip = PolarBear[i];
            sounds[i].volume = Volume;
        }

        return sounds;
    }

    public AudioSource[] loadOilfantSounds(GameObject enemy)
    {
        AudioSource[] sounds = new AudioSource[Oilfant.Length];

        for (int i = 0; i < Oilfant.Length; i++)
        {
            sounds[i] = enemy.AddComponent<AudioSource>();
            sounds[i].clip = Oilfant[i];
            sounds[i].volume = Volume;
        }

        return sounds;
    }

    public AudioSource[] loadMeepSounds(GameObject enemy)
    {
        AudioSource[] sounds = new AudioSource[Meep.Length];

        for (int i = 0; i < Meep.Length; i++)
        {
            sounds[i] = enemy.AddComponent<AudioSource>();
            sounds[i].clip = Meep[i];
            sounds[i].volume = Volume;
        }

        return sounds;
    }
    
}
