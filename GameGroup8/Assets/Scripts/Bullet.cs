using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Type type;

	void Start(){
		if (this.gameObject.CompareTag ("Wind")) {
			type = new Type (1);
		} else if (this.gameObject.CompareTag ("Earth")) {
			type = new Type (2);
		} else {
			type = new Type (3);
		}
	}

	void Update(){
		if (this.gameObject.name.Equals("Bullet(Clone)") && this.gameObject.GetComponent<Rigidbody> ().velocity == new Vector3(0f, 0f, 0f)) {
			GameObject.Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag ("Enemy") && this.gameObject.name.Equals("Bullet(Clone)")){
			EnemyController enemyController = col.gameObject.GetComponent<EnemyController>();
			int damage = (int)(Random.Range (20, 30) * type.damageMultiplierToType(enemyController.getType()));
			enemyController.setHealth(enemyController.getHealth () - damage);
			PlayerAttacker.lastAttackedEnemy = enemyController;
			if(enemyController.getHealth () <= 0){
				EnemySpawner.enemiesDefeaten++;
				Destroy(col.gameObject);
				PlayerAttacker.lastAttackedEnemy = null;
			}
		}
		if(col.gameObject.CompareTag ("Enemy") && this.gameObject.name.Equals("Bullet(Clone)")){
			GameObject.Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision col){
		if (this.gameObject.name.Equals ("Bullet(Clone)")) {
			GameObject.Destroy (gameObject);
		}
	}
}
