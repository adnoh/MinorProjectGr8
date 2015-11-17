﻿using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	
	public float speed;
	public float RotateSpeed = 30f;
    public GameObject Gate;
    public GameObject BuildMenu;
    public GameObject BackButton;
	
	private Rigidbody rb;
    private static bool pause;
    private Vector3 playerPos;

	public static float EnemyHealth = 100;
	public static string EnemyDescription = "Normal Enemy";
	public static float WeaponDamage = 25;

	
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
        pause = false;
        BuildMenu.SetActive(false);
	}
	void Update()
	{
		if(Input.GetKey(KeyCode.Q)){
			transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.E)){
			transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
		}

        if (Input.GetButtonDown("Jump")&&Vector3.Distance(Gate.transform.position,transform.position)<2)
        {
            pause = !pause;
            if (pause)
            {
                playerPos = transform.position;
                transform.position = new Vector3(0, -0.501f, -5.2f);
            } else if (!pause)
            {
                transform.position = playerPos;
                BuildMenu.SetActive(false);
                BackButton.SetActive(false);
            }
        }

        if (pause)
        {
            Time.timeScale = 0;
        } else if (!pause)
        {
            Time.timeScale = 1;
        }
	}


	void FixedUpdate ()


	{
		float moveHorizontal = Input.GetAxis("Horizontal")*Time.deltaTime;
		float moveVertical = Input.GetAxis ("Vertical")*Time.deltaTime;
		
		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		transform.Translate(speed*moveHorizontal,0.0f,speed*moveVertical,Space.World);
	}

    static public bool getPause()
    {
        return pause;
    }

}