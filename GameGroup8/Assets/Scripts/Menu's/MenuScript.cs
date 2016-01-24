using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {


    public Animator buttons;

	void Start(){
		
	}
    /// <summary>
    ///  plays the menu animations when the game starts.
    /// </summary>
    public void animationplay()
    {

        buttons = GetComponent<Animator>();
        buttons.SetBool("start game", true);

    }
}
