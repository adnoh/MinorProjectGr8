using UnityEngine;

public class SoundsEnemies : MonoBehaviour {

    public AudioClip[] Sharky = new AudioClip[1];
    public AudioClip[] Firefox = new AudioClip[1];
    public AudioClip[] DesertEagle = new AudioClip[1];
    public AudioClip[] PolarBear = new AudioClip[1];
    public AudioClip[] Oilfant = new AudioClip[1];
    public AudioClip[] Meep = new AudioClip[1];

    private float Volume;
    private bool mute;

    void Start(){
        Volume = PlayerPrefs.GetFloat("sfx option");
        mute = PlayerPrefs.GetInt("sfx mute") == 1 ? true : false;
    }

    public AudioSource[] loadSharkSounds(GameObject enemy){
        AudioSource[] sounds = new AudioSource[Sharky.Length];
        for (int i = 0; i < Sharky.Length; i++){
            sounds[i] = enemy.AddComponent<AudioSource>();
            sounds[i].clip = Sharky[i];
            sounds[i].volume = Volume;
            sounds[i].spatialBlend = 1;
            sounds[i].rolloffMode = AudioRolloffMode.Linear;
            sounds[i].maxDistance = 100;
            sounds[i].mute = mute;
        }
		return sounds;
    }

    public AudioSource[] loadFireFoxSounds(GameObject enemy){
        AudioSource[] sounds = new AudioSource[Firefox.Length];
        for (int i = 0; i < Firefox.Length; i++){
            sounds[i] = enemy.AddComponent<AudioSource>();
            sounds[i].clip = Firefox[i];
            sounds[i].volume = Volume;
            sounds[i].spatialBlend = 1;
            sounds[i].rolloffMode = AudioRolloffMode.Linear;
            sounds[i].maxDistance = 100;
            sounds[i].mute = mute;
        }
        return sounds;
    }

    public AudioSource[] loadEagleSounds(GameObject enemy){
        AudioSource[] sounds = new AudioSource[DesertEagle.Length];
        for (int i = 0; i < DesertEagle.Length; i++){
            sounds[i] = enemy.AddComponent<AudioSource>();
            sounds[i].clip = DesertEagle[i];
            sounds[i].volume = Volume;
            sounds[i].spatialBlend = 1;
            sounds[i].rolloffMode = AudioRolloffMode.Linear;
            sounds[i].maxDistance = 100;
            sounds[i].mute = mute;
        }
        return sounds;
    }

    public AudioSource[] loadPolarBearSounds(GameObject enemy){
        AudioSource[] sounds = new AudioSource[PolarBear.Length];
        for (int i = 0; i < PolarBear.Length; i++){
            sounds[i] = enemy.AddComponent<AudioSource>();
            sounds[i].clip = PolarBear[i];
            sounds[i].volume = Volume;
            sounds[i].spatialBlend = 1;
            sounds[i].rolloffMode = AudioRolloffMode.Linear;
            sounds[i].maxDistance = 100;
            sounds[i].mute = mute;
        }
        return sounds;
    }

    public AudioSource[] loadOilfantSounds(GameObject enemy){
        AudioSource[] sounds = new AudioSource[Oilfant.Length];
        for (int i = 0; i < Oilfant.Length; i++){
            sounds[i] = enemy.AddComponent<AudioSource>();
            sounds[i].clip = Oilfant[i];
            sounds[i].volume = Volume;
            sounds[i].spatialBlend = 1;
            sounds[i].rolloffMode = AudioRolloffMode.Linear;
            sounds[i].maxDistance = 100;
            sounds[i].mute = mute;
        }
        return sounds;
    }

    public AudioSource[] loadMeepSounds(GameObject enemy){
        AudioSource[] sounds = new AudioSource[Meep.Length];
        for (int i = 0; i < Meep.Length; i++){
            sounds[i] = enemy.AddComponent<AudioSource>();
            sounds[i].clip = Meep[i];
            sounds[i].volume = Volume;
            sounds[i].spatialBlend = 1;
            sounds[i].rolloffMode = AudioRolloffMode.Linear;
            sounds[i].maxDistance = 100;
            sounds[i].mute = mute;
        }
        return sounds;
    }
}
