using UnityEngine;
using System.Collections;

public class CatapultFire : MonoBehaviour {

    private float angle;
    private bool top;
    private bool fire;
    private GameObject Enemy;
    private GameObject TempEnemy;

    public GameObject Object;
    public GameObject TurretBase;

	void Start () {
        angle = 0;
        top = false;
        fire = false;
	}
	
	void Update () {
        if (Enemy)
        {
            if (!fire)
            {
                fire = true;
                StartCoroutine("FireCatapult");
            }
        }
        if (!Enemy)
        {
            Enemy = null;
        }
    }

    void OnTriggerEnter(Collider Other)
    {
        if (!Enemy)
        {
            if (Other.tag == "Enemy")
            {
                Enemy = Other.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider Other)
    {
        if (Other.CompareTag("Enemy"))
        {
            Enemy = null;
        }
    }

    void FireObject()
    {
        Vector3 aimPoint = TempEnemy.transform.position;
        Vector3 firePoint = transform.position;
        firePoint.y = 6; //4.3f;
        Vector3 aim = aimPoint - firePoint;
        GameObject Cat = (GameObject)Instantiate(Object,firePoint,Quaternion.identity);
        Cat.GetComponent<Rigidbody>().AddForce(aim*100);
        Cat.GetComponent<Rigidbody>().AddForce(0, -10, 0);
        TempEnemy = null;
    }

    IEnumerator FireCatapult()
    {
        TempEnemy = Enemy;
            while (!top)
            {
                angle++;
                if (angle == 10)
                {
                    top = true;
                    if (Enemy)
                    {
                        FireObject();
                    }
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

        TempEnemy = null;
        yield return null;
    }
}
