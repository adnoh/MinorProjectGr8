using UnityEngine;
using System.Collections;

public class ButtonAnimation : MonoBehaviour {



    public void PlayAnimation()
    {
        GetComponent<Animator>().Play("bookert_");
    }


}
