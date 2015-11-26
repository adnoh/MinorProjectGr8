using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttacker : MonoBehaviour {

	public GameObject enemyDescription;
	public bool showEnemyDescription;

	public Text enemyDescriptionText;
	public Text enemyHealthBar;
	public Text enemyWeaponDamageText;

	public GameObject bullet;
	public float bulletSpeed = 100f;

	public float attackRate = 0.5f;
	private float nextAttack = 0.0f;

	public static EnemyController lastAttackedEnemy;
		
	void Start () {
		showEnemyDescription = false;
		enemyDescription.SetActive (false);

	}

	void Update () {
		enemyDescription.SetActive (showEnemyDescription);
        bool Base = PlayerController.getPause();
		if (lastAttackedEnemy != null) {
			setEnemyDescription (lastAttackedEnemy);
		} else {
			showEnemyDescription = false;
		}

        if (!Base)
        {
			if (Input.GetMouseButtonDown(0) && Time.time > nextAttack ){
				nextAttack = Time.time + attackRate;
				GameObject shot = GameObject.Instantiate(bullet, transform.position + (transform.forward), transform.rotation) as GameObject;
                shot.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            }
        }
		
	}

	public void setEnemyDescription(EnemyController enemyController){
		enemyDescriptionText.text = "Level = " + enemyController.getLevel ();
		enemyHealthBar.text = "Health = " + enemyController.getHealth ();
		enemyWeaponDamageText.text = "Weapon Damage = " + enemyController.getAttackPower ();
		showEnemyDescription = true;
	}

	public void setEnemyDescriptionOff(){
		showEnemyDescription = false;
	}

	public void setEnemyDescriptionOn(){
		showEnemyDescription = true;
	}


}
