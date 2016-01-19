using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sfxslider : MonoBehaviour
{

    public AudioSource zoomaudio;

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
        var zoom = zoomaudio.GetComponent<AudioSource>();



        var slider = gameObject.GetComponent<Slider>();


        float temp = slider.value;
        zoom.volume = temp;
       buttonaudio.volume = temp;


        PlayerPrefs.SetFloat("sfx option", temp);
        PlayerPrefs.Save();
        // Debug.Log (temp);

    }
}
