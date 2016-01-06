using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Animator backdrop;
    public Animator buttons;



    public void animationplay()
    {
        backdrop = GetComponent<Animator>();
        buttons = GetComponent<Animator>();
        backdrop.SetBool("animatorconstant", true);
        buttons.SetBool("start game", true);

    }
}
