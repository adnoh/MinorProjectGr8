using UnityEngine;
using System.Collections;

public class ButtonAnimation : MonoBehaviour {

    // Canvas of the main menu
    public Canvas can1;
    public Canvas can2;
    public Canvas can3;
    public Canvas can4;
    public Canvas can5;
    // The time it takes for the animation to play
    public float wait;


    // the canvases are disabled for the wait time and afterwards activated
    IEnumerator PageFlipWait1()
    {
        can1.gameObject.SetActive(false);
        yield return new WaitForSeconds(wait);
        can1.gameObject.SetActive(true);
    }

    IEnumerator PageFlipWait2()
    {
         can2.gameObject.SetActive(false);
        yield return new WaitForSeconds(wait);
        can2.gameObject.SetActive(true);
    }

    IEnumerator PageFlipWait3()
    {
        can3.gameObject.SetActive(false);
        yield return new WaitForSeconds(wait);
        can3.gameObject.SetActive(true);
    }

    IEnumerator PageFlipWait4()
    {
        can4.gameObject.SetActive(false);
        yield return new WaitForSeconds(wait);
        can4.gameObject.SetActive(true);
    }

    IEnumerator PageFlipWait5()
    {
        can5.gameObject.SetActive(false);
        yield return new WaitForSeconds(wait);
        can5.gameObject.SetActive(true);
    }

    // the animation and sound is played and the Coroutine started
    public void PlayAnimation()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
  

        GetComponent<Animator>().Play("bookert_");
        if (can1.gameObject.activeSelf == true)
        {
        
            StartCoroutine(PageFlipWait1());
            
        }


        if (can2.gameObject.activeSelf == true)
        {
         
            StartCoroutine(PageFlipWait2());
            
        }

        if (can3.gameObject.activeSelf == true)
        {
            
            StartCoroutine(PageFlipWait3());
          
        }
        if (can4.gameObject.activeSelf == true)
        {
           
            StartCoroutine(PageFlipWait4());
           
        }

        if (can5.gameObject.activeSelf == true)
        {
            
            StartCoroutine(PageFlipWait5());
           
        }
 

    }
  
}
    

