using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	
	public float speed;

	public float RotateSpeed = 30f;
	
	private Rigidbody rb;

	
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	void Update()
	{
		if(Input.GetKey(KeyCode.Q)){
			transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.E)){
			transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
		}
	}


	void FixedUpdate ()


	{
		float moveHorizontal = Input.GetAxis("Horizontal")*Time.deltaTime;
		float moveVertical = Input.GetAxis ("Vertical")*Time.deltaTime;
		
		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		transform.Translate(speed*moveHorizontal,0.0f,speed*moveVertical,Space.World);
	}
}