using UnityEngine;
using System.Collections;

public class SearchLight : MonoBehaviour {

    public GameObject Lamp;
    public GameObject Light;

    private bool rotationStan;
    private bool rotationLight;
    private Vector3 offset = new Vector3(0, 40, 0);
    private Vector3 offsetLight = new Vector3(5, 0, 0);
    private Vector3 initialRotation;
    private Vector3 maxRotation;
    private Vector3 minRotation;
    private Vector3 initialRotationLight;
    private Vector3 maxRotationLight;
    private Vector3 minRotationLight;

    private GameObject Enemy;

    void Start() {
        rotationStan = false;
        rotationLight = false;
        Enemy = null;

        initialRotation = transform.rotation.eulerAngles;
        maxRotation = initialRotation + offset;
        minRotation = initialRotation - offset;
        initialRotationLight = Lamp.transform.rotation.eulerAngles;
        maxRotationLight = initialRotationLight + offsetLight * 4;
        minRotationLight = initialRotationLight - offsetLight;
    }
	
	void Update () {
        float height = GameObject.Find("SUn").GetComponent<Daynight>().getHeigth();
        if (height < 150)
        {
            Light.SetActive(true);
            if (Enemy == null)
            {
                
                if (!rotationStan)
                {
                    gameObject.transform.Rotate(Vector3.up, 1f);
                    if (transform.rotation.eulerAngles.y >= maxRotation.y)
                        rotationStan = true;
                }
                else if (rotationStan)
                {
                    gameObject.transform.Rotate(Vector3.up, -1f);
                    if (transform.rotation.eulerAngles.y <= minRotation.y)
                        rotationStan = false;
                }
                
                if (!rotationLight)
                {
                    Lamp.transform.Rotate(Vector3.up, 0.1f);
                    if (Lamp.transform.rotation.eulerAngles.x <= minRotationLight.x)
                        rotationLight = true;
                }
                else if (rotationLight)
                {
                    Lamp.transform.Rotate(Vector3.up, -.1f);
                    if (Lamp.transform.rotation.eulerAngles.x >= maxRotationLight.x)
                        rotationLight = false;
                }
            }
            else if (Enemy != null)
            {
                Vector3 placeY = new Vector3(Enemy.transform.position.x, transform.position.y, Enemy.transform.position.z);
                transform.LookAt(placeY);
                transform.Rotate(new Vector3(0, 90, 0));
                Lamp.transform.LookAt(Enemy.transform.position);
            }
        } else
        {
            Light.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (Enemy == null)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy = col.gameObject;
            }
        }
    }

    void onTriggerExit(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            Enemy = null;
        }
    }
}
