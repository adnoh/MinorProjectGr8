using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public GameObject windEnemy;
	public GameObject waterEnemy;
	public GameObject earthEnemy;
	public int wave;
	private bool waveOver;

	public static int enemiesDefeaten;
	
	public Text waveText;
	public Text enemiesDefeatenText;

	void Start () {
		wave = 0;
		waveText.text = "Current wave: 1";
		waveOver = true;
		enemiesDefeaten = 0;
		enemiesDefeatenText.text = "Enemies defeaten this wave: " + enemiesDefeaten;
	}

	void Update () {
		if(Input.GetKeyDown (KeyCode.P) && waveOver){
			wave++;
			waveText.text = "Current wave: " + wave;
			enemiesDefeaten = 0;
			waveOver = false;
			for(int i = 0; i < wave; i++){
				int random = Random.Range (0, 3);
				if(random == 0){
					Instantiate (waterEnemy, getRandomPosition(), Quaternion.identity);
				}
				else if(random == 1){
					Instantiate (windEnemy, getRandomPosition(), Quaternion.identity);
				}
				else{
					Instantiate (earthEnemy, getRandomPosition(), Quaternion.identity);
				}
			}
		}
		if (enemiesDefeaten == wave - 1) {
			waveOver = true;
		}
		enemiesDefeatenText.text = "Enemies defeaten this wave: " + enemiesDefeaten;
	}

	Vector3 getRandomPosition(){
		return new Vector3 ((float)Random.Range (20, 40), 1f, (float)Random.Range (-10, 10));
	}
			       



}
