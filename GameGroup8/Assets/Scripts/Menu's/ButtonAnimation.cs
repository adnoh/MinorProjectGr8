using UnityEngine;
using System.Collections;

public class ButtonAnimation : MonoBehaviour {

    public Canvas can1;
    public Canvas can2;
    public Canvas can3;
    public Canvas can4;
    public Canvas can5;
    public float wait;

    IEnumerator MyMethod1()
    {
        Debug.Log("Before Waiting 2 seconds");
        can1.gameObject.SetActive(false);
        yield return new WaitForSeconds(wait);
        Debug.Log("After Waiting 2 Seconds");
        can1.gameObject.SetActive(true);
    }

    IEnumerator MyMethod2()
    {
        Debug.Log("Before Waiting 2 seconds");
         can2.gameObject.SetActive(false);
        yield return new WaitForSeconds(wait);
        Debug.Log("After Waiting 2 Seconds");
        can2.gameObject.SetActive(true);
    }

    IEnumerator MyMethod3()
    {
        Debug.Log("Before Waiting 2 seconds");
        can3.gameObject.SetActive(false);
        yield return new WaitForSeconds(wait);
        Debug.Log("After Waiting 2 Seconds");
        can3.gameObject.SetActive(true);
    }

    IEnumerator MyMethod4()
    {
        Debug.Log("Before Waiting 2 seconds");
        can4.gameObject.SetActive(false);
        yield return new WaitForSeconds(wait);
        Debug.Log("After Waiting 2 Seconds");
        can4.gameObject.SetActive(true);
    }

    IEnumerator MyMethod5()
    {
        Debug.Log("Before Waiting 2 seconds");
        can5.gameObject.SetActive(false);
        yield return new WaitForSeconds(wait);
        Debug.Log("After Waiting 2 Seconds");
        can5.gameObject.SetActive(true);
    }

    public void PlayAnimation()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        Animation anim = GetComponent<Animation>();

        GetComponent<Animator>().Play("bookert_");
        if (can1.gameObject.activeSelf == true)
        {
        
            StartCoroutine(MyMethod1());
            
        }


        if (can2.gameObject.activeSelf == true)
        {
         
            StartCoroutine(MyMethod2());
            
        }

        if (can3.gameObject.activeSelf == true)
        {
            
            StartCoroutine(MyMethod3());
          
        }
        if (can4.gameObject.activeSelf == true)
        {
           
            StartCoroutine(MyMethod4());
           
        }

        if (can5.gameObject.activeSelf == true)
        {
            
            StartCoroutine(MyMethod5());
           
        }
 

    }
  
}
    

