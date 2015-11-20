using UnityEngine;
using System.Collections;
[RequireComponent(typeof (Light))]
public class Daynight : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (Vector3.zero, Vector3.right, 10f * Time.deltaTime);
		transform.LookAt (Vector3.zero);
		var myPosition = transform.position;
		var Height = myPosition.y;
		GetComponent<Light>().intensity = (Height < 0) ? 0f : 1f;

	}
}
