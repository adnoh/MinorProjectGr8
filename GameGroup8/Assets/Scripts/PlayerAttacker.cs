using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAttacker : MonoBehaviour {

	public GameObject enemyDescription;
	public bool showEnemyDescription;

	public Text enemyDescriptionText;
	public Text enemyHealthBar;

	private GameObject enemy;
	
	void Start () {
		showEnemyDescription = false;
		enemyDescription.SetActive (false);
		enemy = null;
	}

	void Update () {
		enemyDescription.SetActive (showEnemyDescription);
		enemyDescriptionText.text = "Enemy: " + Controls.EnemyDescription;
		enemyHealthBar.text = "Enemy. Health = " + Controls.EnemyHealth;
		if(Input.GetKeyDown (KeyCode.T) && showEnemyDescription){
			Controls.EnemyHealth -= Controls.WeaponDamage;
			if(Controls.EnemyHealth <= 0){
				Destroy(enemy);
				showEnemyDescription = false;
			}
		}

	}


	void OnTriggerEnter(Collider col) {
		if (col.gameObject.CompareTag ("Enemy") && enemy == null) {
			Controls.EnemyHealth = 100;
			Controls.EnemyDescription = "Normal Enemy";
			showEnemyDescription = true;
			enemy = col.gameObject;
		} else if (col.gameObject.CompareTag ("Enemy1") && enemy == null) {
			Controls.EnemyHealth = 200;
			Controls.EnemyDescription = "Better Enemy";
			showEnemyDescription = true;
			enemy = col.gameObject;
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.CompareTag ("Enemy") || col.gameObject.CompareTag ("Enemy1")) {
			showEnemyDescription = false;
			enemy = null;
		}
	}






	
}
