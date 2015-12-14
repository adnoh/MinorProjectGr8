using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Type type;
	public int dmg;
	public bool stun;
	public bool poisonous;


	void Start(){
		if (this.gameObject.CompareTag ("No type")) {
			type = new Type (0);
		} else if (this.gameObject.CompareTag ("Wind")) {
			type = new Type (1);
		} else if (this.gameObject.CompareTag ("Earth")) {
			type = new Type (2);
		} else {
			type = new Type (3);
		}
	}

	void Update(){
		if ((this.gameObject.name.Equals("newBullet(Clone)") || this.gameObject.name.Equals ("CatPrefab(Clone)") || this.gameObject.name.Equals("SnailPrefab(Clone)")) && this.gameObject.GetComponent<Rigidbody> ().velocity == new Vector3(0f, 0f, 0f)) {
			GameObject.Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag ("Enemy") && (this.gameObject.name.Equals("newBullet(Clone)") || this.gameObject.name.Equals ("CatPrefab(Clone)") || this.gameObject.name.Equals("SnailPrefab(Clone)"))){
			EnemyController enemyController = col.gameObject.GetComponent<EnemyController>();
			int damage = (int)(Random.Range (dmg, dmg + 10) * type.damageMultiplierToType(enemyController.getType()) * PlayerAttributes.getAttackMultiplier());
			enemyController.setHealth(enemyController.getHealth () - damage);
			if(poisonous){
				enemyController.setPoisoned(true);
			}
			if(stun){
				enemyController.stun (true);
			}
			PlayerAttacker.lastAttackedEnemy = enemyController;
			if(enemyController.getHealth () <= 0){
				PSpawner spawner = Camera.main.GetComponent<PSpawner>();
				spawner.placeUnit(enemyController.gameObject.transform.position);
				EnemySpawner.enemiesDefeaten++;
				col.gameObject.GetComponent<Seeker>().StopAllCoroutines();
				col.gameObject.GetComponent<Seeker>().destroyed = true;
				Destroy(col.gameObject);
				PlayerAttacker.lastAttackedEnemy = null;
				MiniMapScript.enemies.Remove(enemyController);
				PlayerAttributes.getExperience(enemyController.getLevel());
			}
			GameObject.Destroy (gameObject);
		}

	}

	/*void OnCollisionEnter(Collision col){
		if ((col.gameObject.CompareTag ("Wall") || col.gameObject.name.Equals ("House(Clone)") || col.gameObject.name.Equals("Gate")) && this.gameObject.name.Equals ("newBullet(Clone)")) {
			GameObject.Destroy (gameObject);
		}
	}*/
}
