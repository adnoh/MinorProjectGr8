using UnityEngine;
using System.Collections;

public class CatapultFire : MonoBehaviour {

    private float angle;
    private bool top;
    private bool fire;

	void Start () {
        angle = 0;
        top = false;
        fire = false;
	}
	
	void Update () {
        if (Input.GetKeyDown("g"))
        {
            if (!fire)
            {
                fire = true;
                StartCoroutine("FireCatapult");
            }
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
