using UnityEngine;
using System.Collections;
[RequireComponent(typeof (Light))]

public class Daynight : MonoBehaviour {

	public static float Height;

    public float intensity;
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (Vector3.zero, Vector3.right, 5f * Time.deltaTime);
		transform.LookAt (Vector3.zero);
		var myPosition = transform.position;
		Height = myPosition.y;
        GetComponent<Light>().intensity = (Height < 0f) ? 0f * intensity : 1f * intensity *(0.2f+(Height/500));
	}

	
	public static float getCount()
	{
		return Height;
	}
}
