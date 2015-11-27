using UnityEngine;
using System.Collections;

public class CatapultFire : MonoBehaviour {

    private float angle;
    private bool top;
    private bool fire;
    private GameObject Vijand;

    public GameObject Object;
    public GameObject TurretBase;

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
                StartCoroutine("FireCatapult");
            }
        }
    }

    void OnTriggerEnter(Collider Other)
    {
        if (!Vijand)
        {
            if (Other.tag == "Enemy")
            {
                Vijand = Other.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider Other)
    {
        if (Other.tag == "Enemy")
        {
            Vijand = null;
        }
    }

    void FireObject()
    {
        Vector3 aimPoint = Vijand.transform.position;
        Vector3 firePoint = transform.position;
        firePoint.y = 4.3f;
        Vector3 aim = aimPoint - firePoint;
        GameObject Cat = (GameObject)Instantiate(Object,firePoint,Quaternion.identity);
        Cat.GetComponent<Rigidbody>().AddForce(aim*100);
        Cat.GetComponent<Rigidbody>().AddForce(0, -10, 0);
    }

    IEnumerator FireCatapult()
    {
            while (!top)
            {
                angle++;
                if (angle == 10)
                {
                    top = true;
                    FireObject();
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
                    yield return new WaitForSeconds(1);
                    fire = false;
                    break;
                }
                transform.Rotate(new Vector3(0, 1, 0), -15);
                yield return null;
            }
        

        yield return null;
    }
}
