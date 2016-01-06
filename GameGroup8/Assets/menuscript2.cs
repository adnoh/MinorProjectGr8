using UnityEngine;
using System.Collections;

public class menuscript2 : MonoBehaviour {

    public Animator backdrop;

    public void animationplay()
    {
        backdrop = GetComponent<Animator>();
        
        backdrop.SetBool("animatorconstant", true);

    }


}
