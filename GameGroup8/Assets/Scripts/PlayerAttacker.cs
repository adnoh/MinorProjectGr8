using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAttacker : MonoBehaviour {

	public GameObject enemyDescription;
	public bool showEnemyDescription;

	public Text enemyDescriptionText;
	public Text enemyHealthBar;
	public Text enemyWeaponDamageText;

	private int attackStrength;
	
	void Start () {
		showEnemyDescription = false;
		enemyDescription.SetActive (false);
		attackStrength = 25;
	}

	void Update () {
		enemyDescription.SetActive (showEnemyDescription);
		if(Input.GetKeyDown (KeyCode.T)){
             
			RaycastHit hit;
			Ray bulletray = new Ray(transform.position, Vector3.forward);
			if(Physics.Raycast(bulletray, out hit)){
				if(hit.collider.tag == "Enemy"){
					EnemyController enemyController = hit.collider.gameObject.GetComponent<EnemyController>();
					enemyController.setHealth(enemyController.getHealth () - attackStrength);
					enemyDescriptionText.text = "Level = " + enemyController.getLevel ();
					enemyHealthBar.text = "Health = " + enemyController.getHealth ();
					enemyWeaponDamageText.text = "Weapon Damage = " + enemyController.getAttackPower ();
					showEnemyDescription = true;
					if(enemyController.getHealth () <= 0){
						EnemySpawner.enemiesDefeaten++;
						Destroy(hit.collider.gameObject);
						showEnemyDescription = false;
					}
				}
			}
		}
	}
}
