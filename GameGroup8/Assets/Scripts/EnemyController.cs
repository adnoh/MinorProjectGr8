using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed;

	public bool isDead;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		isDead = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (getDirection ());
	}

	Vector3 getDirection() {

		int i = Random.Range (0, 5);
		Vector3 direction = new Vector3 (0, 0, 0);

		if(i <= 1){
			direction = new Vector3(speed, 0f, 0f);
		}
		if(i > 1 && i <= 2){
			direction = new Vector3((-1.0f * speed), 0f, 0f);
		}
		if(i > 2 && i <= 3){
			direction = new Vector3(0f, 0f, speed);
		}
		if(i > 3 && i <= 4){
			direction = new Vector3(0f, 0f, (-1.0f * speed));
		}
		return direction;
	}
	
}
