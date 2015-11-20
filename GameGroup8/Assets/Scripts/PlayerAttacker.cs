using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAttacker : MonoBehaviour {

	public GameObject enemyDescription;
	public bool showEnemyDescription;

	public Text enemyDescriptionText;
	public Text enemyHealthBar;
	public Text enemyWeaponDamageText;

	private GameObject enemy;

	private EnemyController enemyController;

	private int attackStrength;
	
	void Start () {
		enemyController = null;
		showEnemyDescription = false;
		enemyDescription.SetActive (false);
		enemy = null;
		attackStrength = 25;
	}

	void Update () {
		enemyDescription.SetActive (showEnemyDescription);
		if (enemyController != null) {
			enemyDescriptionText.text = "Enemy Level: " + enemyController.getLevel ();
			enemyHealthBar.text = "Health: " + enemyController.getHealth ();
			enemyWeaponDamageText.text = "Weapon Damage: " + enemyController.getAttackPower();
		}
		if(Input.GetKeyDown (KeyCode.T)){
             
			RaycastHit hit;
			Ray bulletray = new Ray(transform.position, Vector3.forward);
			if(Physics.Raycast(bulletray, out hit)){
				if(hit.collider.tag == "Enemy"){
			enemyController.setHealth(enemyController.getHealth () - attackStrength);
			enemyHealthBar.text = "Health = " + enemyController.getHealth ();
			if(enemyController.getHealth () <= 0){
				EnemySpawner.enemiesDefeaten++;
				Destroy(enemy);
				showEnemyDescription = false;
			}
				}
			}
		}
	}


	void OnTriggerEnter(Collider col) {
		if (col.gameObject.CompareTag ("Enemy")) {
			enemyController = col.GetComponent<EnemyController> ();
			enemy = col.gameObject;
			showEnemyDescription = true;
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.CompareTag ("Enemy")) {
			showEnemyDescription = false;
			enemy = null;
		}
	}






	
}
