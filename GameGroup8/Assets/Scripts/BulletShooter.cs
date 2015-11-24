using UnityEngine;
using System.Collections;

public class BulletShooter : MonoBehaviour {

	public GameObject bullet;
	public float bulletSpeed = 100f;
	
	void Update () {
		if(Input.GetMouseButtonDown(0)){

			GameObject shot = GameObject.Instantiate(bullet, transform.position + (transform.forward), transform.rotation) as GameObject;
			shot.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
		}

	}
	

}
