using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject normalEnemy;

	void Update(){
		if(Input.GetKeyDown (KeyCode.P)){
			//Instantiate (normalEnemy, new Vector3(-13, 1, -1), Quaternion.identity);
		}
	}

}
