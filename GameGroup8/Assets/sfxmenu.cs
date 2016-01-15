using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sfxmenu : MonoBehaviour
{

    public AudioSource zoomaudio;
    public AudioSource fox;
    public AudioSource shark;
    public AudioSource bear;
    public AudioSource peem;
    public AudioSource phant;
    public AudioSource eagle;


    void Start()
    {
        var slider = gameObject.GetComponent<Slider>();
        var temp = PlayerPrefs.GetFloat("sfx option");
        slider.value = temp;


    }

    // Update is called once per frame
    void Update()
    {

        var buttonaudio = Camera.main.GetComponent<AudioSource>();
        var fox1 = fox.GetComponent<AudioSource>();
        var shark1 = shark.GetComponent<AudioSource>();
        var bear1 = bear.GetComponent<AudioSource>();
        var peem1 = peem.GetComponent<AudioSource>();
        var phant1 = phant.GetComponent<AudioSource>();
        var eagle1 = eagle.GetComponent<AudioSource>();
        var zoom = zoomaudio.GetComponent<AudioSource>();



        var slider = gameObject.GetComponent<Slider>();


        float temp = slider.value;
        fox1.volume = temp;
        shark1.volume = temp;
        bear1.volume = temp;
        peem1.volume = temp;
        phant1.volume = temp;
        eagle1.volume = temp;
        zoom.volume = temp;

        buttonaudio.volume = temp;


        PlayerPrefs.SetFloat("sfx option", temp);
        PlayerPrefs.Save();
        
    }
}
