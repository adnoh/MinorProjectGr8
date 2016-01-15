using System.Collections;
using UnityEngine;

public class SoundsPlayer : MonoBehaviour {

    public AudioClip walk;
    public AudioClip[] stab = new AudioClip[2];
    public AudioClip dead;

    private AudioSource[] Sounds;
    private bool playwalk = false;

	void Start () {
        Sounds = new AudioSource[4];

        for (int i = 0; i < 4; i++)
        {
            Sounds[i] = gameObject.AddComponent<AudioSource>();
        }

        Sounds[0].clip = walk;
        Sounds[1].clip = stab[0];
        Sounds[2].clip = stab[1];
        Sounds[3].clip = dead;
    }
	
	public void PlayWalk()
    {
        
        if (playwalk == false)
        {
            Sounds[0].Play();
            playwalk = true;
            StartCoroutine("WalkPlay");
        }
    }

    public void StopWalk()
    {
        Sounds[0].Stop();
    }

    public void PlayHit()
    {
        float nr = Random.Range(0f, 1f);

        if (nr <= 0.5)
        {
            Sounds[1].Play();
        }
        else if (nr > 0.5)
        {
            Sounds[2].Play();
        }
    }

    public void PlayDead()
    {
        Sounds[3].Play();
    }

    IEnumerator WalkPlay()
    {
        yield return new WaitForSeconds(1);
        playwalk = false;
        yield return null;
    }
}
