using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAttacker : MonoBehaviour {

	private int enemiesKilled;
	public Text enemiesDead;

	// Use this for initialization
	void Start () {
		enemiesDead = GetComponent<Text> ();
		updateEnemiesKilled ();
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.CompareTag ("Enemy")) {
			Destroy (col.gameObject);
			enemiesKilled ++;
			updateEnemiesKilled ();
		}
	}

	public void updateEnemiesKilled(){
		enemiesDead.text = "Enemies Killed: 0";
	}
	
}
