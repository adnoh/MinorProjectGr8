using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAttacker : MonoBehaviour {

	public GameObject enemyDescription;
	public bool showEnemyDescription;

	public Text enemyDescriptionText;
	public Text enemyHealthBar;

	private GameObject enemy;

	private EnemyController enemyController;
	
	void Start () {
		enemyController = null;
		showEnemyDescription = false;
		enemyDescription.SetActive (false);
		enemy = null;
	}

	void Update () {
		enemyDescription.SetActive (showEnemyDescription);
		if (enemyController != null) {
			enemyDescriptionText.text = "Enemy Level: " + enemyController.getLevel ();
			enemyHealthBar.text = "Health = " + enemyController.getHealth ();
		}
		if(Input.GetKeyDown (KeyCode.T) && enemy != null){

			enemyController.setHealth(enemyController.getHealth () - 25);
			print(enemyController.getHealth());
			enemyHealthBar.text = "Health = " + enemyController.getHealth ();
			if(enemyController.getHealth () <= 0){
				Destroy(enemy);
				showEnemyDescription = false;
			}
		}
	}


	void OnTriggerEnter(Collider col) {
		enemyController = col.GetComponent<EnemyController> ();
		enemy = col.gameObject;
		showEnemyDescription = true;
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.CompareTag ("Enemy")) {
			showEnemyDescription = false;
			enemy = null;
		}
	}






	
}
