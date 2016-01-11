using UnityEngine;
using System.Collections;

public class SearchLight : MonoBehaviour {

    public GameObject Light;

    private bool rotationStan;
    private int rotationY;
    private bool rotationLight;
    private int rotationX;

    void Start() {
        rotationStan = false;
        rotationLight = false;
        rotationY = 100;
        rotationX = 10;
    }
	
	void Update () {
        /*
        if (!rotationStan)
        {
            gameObject.transform.Rotate(Vector3.up, 1);
            rotationY--;
            if (rotationY <= 0)
                rotationStan = true;
        }
        else if (rotationStan)
        {
            gameObject.transform.Rotate(Vector3.up, -1);
            rotationY++;
            if (rotationY >= 200)
                rotationStan = false;
        }
        /**/
        if (!rotationLight)
        {
            Light.transform.Rotate(Vector3.up, 0.25f);
            rotationX--;
            if (rotationX <= 0)
                rotationLight = true;
        }
        else if (rotationLight)
        {
            Light.transform.Rotate(Vector3.up, -.25f);
            rotationX++;
            if (rotationX >= 80)
                rotationLight = false;
            //dingen
        }
    }
}
