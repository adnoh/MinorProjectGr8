using UnityEngine;
using System.Collections;

public class CatapultFire : MonoBehaviour {

    private float angle;
    private bool top;
    private bool fire;
    private GameObject Vijand;

	void Start () {
        angle = 0;
        top = false;
        fire = false;
	}
	
	void Update () {
        if (Vijand)
        {
            if (!fire)
            {
                fire = true;
                float tijd = Time.realtimeSinceStartup + 2;
                Debug.Log(tijd);
                if (Time.realtimeSinceStartup > tijd)
                {
                    StartCoroutine("FireCatapult");
                    Debug.Log("Fire!!!");
                }
            }
        }
    }

    void OnTriggerEnter(Collider Other)
    {
        if(Other.tag == "Enemy")
        {
            Vijand = Other.gameObject;
        }
    }

    void OnTriggerExit(Collider Other)
    {
        if (Other.tag == "Enemy")
        {
            Vijand = null;
        }
    }

    IEnumerator FireCatapult()
    {
            while (!top)
            {
                angle++;
                if (angle == 10)
                {
                    top = true;
                    break;
                }
                transform.Rotate(new Vector3(0, 1, 0), 15);
                yield return null;
            }
            while (top)
            {
                angle--;
                if (angle == 0)
                {
                    top = false;
                    fire = false;
                    break;
                }
                transform.Rotate(new Vector3(0, 1, 0), -15);
                yield return null;
            }
        

        yield return null;
    }
}
