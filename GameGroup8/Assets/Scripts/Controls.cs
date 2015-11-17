using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controls : MonoBehaviour {
	
	public float speed;

	public float RotateSpeed = 30f;
	
	public Text winText;

	public Text countText;

	private int count;

	private int needed;
	
	void Start ()
	{
	


		needed = Spawner.amount;
		Debug.Log(needed);
		count = 0;
		countText.text = "Count: " + count.ToString ();
		winText.text = "";
		//rb = GetComponent<Rigidbody>();
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


	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick-Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			countText.text = "Count: " + count.ToString ();
			if (count >= needed)
			{
				winText.text = "You Win!";
			}
		}
	}


}