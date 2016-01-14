using UnityEngine;
using System.Collections;

public class SoundsWeapons : MonoBehaviour {

    public AudioClip[] soundsBaseBat = new AudioClip[3];
    public AudioClip[] soundsPistol = new AudioClip[1];
    public AudioClip[] soundsRam = new AudioClip[2];
    public AudioClip[] soundsStinger = new AudioClip[1];
    public AudioClip[] soundsSwordFish = new AudioClip[3];
    public AudioClip[] soundsEel = new AudioClip[2];
    public AudioClip[] soundsWunderWaffen = new AudioClip[1];

    private AudioSource[] BaseBat;
    private AudioSource[] Pistol;
    private AudioSource[] Ram;
    private AudioSource[] Stinger;
    private AudioSource[] Swordfish;
    private AudioSource[] Eel;
    private AudioSource[] Wunder;

    private AudioSource[][] Sounds = new AudioSource[8][];

    private bool playing = false;
    private int soundNr;
    private AudioSource WeaponFire;

    AudioSource[] loadBatSounds(GameObject weapon)
    {
        AudioSource[] sounds = new AudioSource[soundsBaseBat.Length];

        for (int i = 0; i < soundsBaseBat.Length; i++)
        {
            sounds[i] = weapon.AddComponent<AudioSource>();
            sounds[i].clip = soundsBaseBat[i];
        }

        return sounds;
    }

    AudioSource[] loadPistolSounds(GameObject weapon)
    {
        AudioSource[] sounds = new AudioSource[soundsPistol.Length];

        for (int i = 0; i < soundsPistol.Length; i++)
        {
            sounds[i] = weapon.AddComponent<AudioSource>();
            sounds[i].clip = soundsPistol[i];
        }

        return sounds;
    }

    AudioSource[] loadRamSounds(GameObject weapon)
    {
        AudioSource[] sounds = new AudioSource[soundsRam.Length];

        for (int i = 0; i < soundsRam.Length; i++)
        {
            sounds[i] = weapon.AddComponent<AudioSource>();
            sounds[i].clip = soundsRam[i];
        }

        return sounds;
    }

    AudioSource[] loadStingerSounds(GameObject weapon)
    {
        AudioSource[] sounds = new AudioSource[soundsStinger.Length];

        for (int i = 0; i < soundsStinger.Length; i++)
        {
            sounds[i] = weapon.AddComponent<AudioSource>();
            sounds[i].clip = soundsStinger[i];
        }

        return sounds;
    }

    AudioSource[] loadSwordfishSounds(GameObject weapon)
    {
        AudioSource[] sounds = new AudioSource[soundsSwordFish.Length];

        for (int i = 0; i < soundsSwordFish.Length; i++)
        {
            sounds[i] = weapon.AddComponent<AudioSource>();
            sounds[i].clip = soundsSwordFish[i];
        }

        return sounds;
    }

    AudioSource[] loadEelSounds(GameObject weapon)
    {
        AudioSource[] sounds = new AudioSource[soundsEel.Length];

        for (int i = 0; i < soundsEel.Length; i++)
        {
            sounds[i] = weapon.AddComponent<AudioSource>();
            sounds[i].clip = soundsEel[i];
        }

        return sounds;
    }

    AudioSource[] loadWunderWaffenSounds(GameObject weapon)
    {
        AudioSource[] sounds = new AudioSource[soundsWunderWaffen.Length];

        for (int i = 0; i < soundsWunderWaffen.Length; i++)
        {
            sounds[i] = weapon.AddComponent<AudioSource>();
            sounds[i].clip = soundsWunderWaffen[i];
        }

        return sounds;
    }

    public void loadGunSounds(GameObject player)
    {
        BaseBat = loadBatSounds(player);
        Pistol = loadPistolSounds(player);
        Ram = loadRamSounds(player);
        Stinger = loadStingerSounds(player);
        Swordfish = loadSwordfishSounds(player);
        Eel = loadEelSounds(player);
        Wunder = loadWunderWaffenSounds(player);

        Sounds[0] = Pistol;
        Sounds[1] = null; 
        Sounds[2] = Stinger;
        Sounds[3] = Eel;
        Sounds[4] = Wunder;
        Sounds[5] = Ram;
        Sounds[6] = Swordfish;
        Sounds[7] = BaseBat;
    }

    public void playWeaponSound(int weapon)
    {
        AudioSource[] sounds = Sounds[weapon];

        if (playing == false)
        {
            soundNr = Random.Range(0, sounds.Length);
            WeaponFire = sounds[soundNr];
            playing = true;
            StartCoroutine("WeaponShot");
        }
    }

    IEnumerator WeaponShot()
    {
        WeaponFire.Play();
        yield return new WaitForSeconds(0.5f);
        WeaponFire.Stop();
        playing = false;
        yield return null;
    }
}
