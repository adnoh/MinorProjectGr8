using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	

	void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag ("Enemy") && this.gameObject.name.Equals("Bullet(Clone)")){
			EnemyController enemyController = col.gameObject.GetComponent<EnemyController>();
			enemyController.setHealth(enemyController.getHealth () - Random.Range (20, 30));
			PlayerAttacker.lastAttackedEnemy = enemyController;
			if(enemyController.getHealth () <= 0){
				EnemySpawner.enemiesDefeaten++;
				Destroy(col.gameObject);
				PlayerAttacker.lastAttackedEnemy = null;
			}
		}
		if(this.gameObject.name.Equals("Bullet(Clone)")){
			GameObject.Destroy (gameObject);
		}
	}
}
