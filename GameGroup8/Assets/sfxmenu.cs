using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sfxmenu : MonoBehaviour
{
    public Toggle mute;
    public AudioSource zoomaudio;
    public AudioSource fox;
    public AudioSource shark;
    public AudioSource bear;
    public AudioSource peem;
    public AudioSource phant;
    public AudioSource eagle;
    public AudioSource book;
    public AudioSource buttonaudio;

    private Slider slider;


    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        var temp = PlayerPrefs.GetFloat("sfx option");
        slider.value = temp;
        mute.isOn = PlayerPrefs.GetInt("sfx mute") == 1 ? true : false;

    }

    // Update is called once per frame
    void Update()
    {

        if (mute.isOn == true)
        {
            PlayerPrefs.SetInt("sfx mute", 1);
            slider.interactable = false;
            buttonaudio.mute = true;

            fox.mute = true;
            shark.mute = true;
            bear.mute = true;
            peem.mute = true;
            phant.mute = true;
            eagle.mute = true;
            zoomaudio.mute = true;
            book.mute = true;

        }
        else if (mute.isOn == false)
        {
            PlayerPrefs.SetInt("sfx mute", 0);
            slider.interactable = true;
            buttonaudio.mute = false;

            fox.mute = false;
            shark.mute = false;
            bear.mute = false;
            peem.mute = false;
            phant.mute = false;
            eagle.mute = false;
            zoomaudio.mute = false;
            book.mute = false;
        }


        float temp = slider.value;
        fox.volume = temp;
        shark.volume = temp;
        bear.volume = temp;
        peem.volume = temp;
        phant.volume = temp;
        eagle.volume = temp;
        zoomaudio.volume = temp;

        buttonaudio.volume = temp;
        book.volume = temp;


        PlayerPrefs.SetFloat("sfx option", temp);
        PlayerPrefs.Save();
        
    }
}
