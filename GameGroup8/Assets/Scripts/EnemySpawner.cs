using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject normalEnemy;
	public int wave;
	private bool waveOver;

	public static int enemiesDefeaten;

	void Start () {
		wave = 1;
		waveOver = true;
		enemiesDefeaten = 0;
	}

	void Update(){
		if(Input.GetKeyDown (KeyCode.P) && waveOver){
			enemiesDefeaten = 0;
			waveOver = false;
			for(int i = 0; i < wave; i++){
				Instantiate (normalEnemy, getRandomPosition(), Quaternion.identity);
			}
			wave++;
		}
		if (enemiesDefeaten == wave - 1) {
			waveOver = true;
		}
	}

	Vector3 getRandomPosition(){
		return new Vector3 ((float)Random.Range (20, 40), 1f, (float)Random.Range (-10, 10));
	}
			       



}
