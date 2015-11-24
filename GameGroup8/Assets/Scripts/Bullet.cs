using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	

	void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag ("Enemy")){
			EnemyController enemyController = col.gameObject.GetComponent<EnemyController>();
			enemyController.setHealth(enemyController.getHealth () - Random.Range (20, 30));
			if(enemyController.getHealth () <= 0){
				EnemySpawner.enemiesDefeaten++;
				Destroy(col.gameObject);
			}
		}
		GameObject.Destroy (gameObject);
	}
}
