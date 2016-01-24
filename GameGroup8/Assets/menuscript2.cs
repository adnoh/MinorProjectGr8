using UnityEngine;
using System.Collections;

public class menuscript2 : MonoBehaviour {

    public Animator backdrop;

    /// <summary>
    ///  plays the menu animations when new game game is pressed.
    /// </summary>
    public void animationplay()
    {
        backdrop = GetComponent<Animator>();
        
        backdrop.SetBool("animatorconstant", true);

    }


}
