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

	public Type currentType;
	public Text playerWeaponText;

	public GameObject bullet;
	public float bulletSpeed = 100f;

	public float attackRate = 0.5f;
	private float nextAttack = 0.0f;

	public static EnemyController lastAttackedEnemy;
		
	void Start () {
		showEnemyDescription = false;
		enemyDescription.SetActive (false);
		currentType = new Type (1);
		playerWeaponText.text = "Weapon: " + currentType.toString () + "-type";
	}

	void Update () {
		enemyDescription.SetActive (showEnemyDescription);
        bool Base = PlayerController.getPause();
		if (lastAttackedEnemy != null) {
			setEnemyDescription (lastAttackedEnemy);
		} else {
			showEnemyDescription = false;
		}

        if (!Base){
			if (Input.GetMouseButtonDown(0) && Time.time > nextAttack ){
				nextAttack = Time.time + attackRate;
				GameObject bulletClone = GameObject.Instantiate(bullet, transform.position + (transform.forward), transform.rotation) as GameObject;
				bulletClone.tag = currentType.toString ();
                bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            }
        }
		if(Input.GetMouseButtonDown (1)){
			currentType.nextType();
			playerWeaponText.text = "Weapon: " + currentType.toString () + "-type";
		}
	}

	public void setEnemyDescription(EnemyController enemyController){
		enemyDescriptionText.text = "Enemy Type = " + enemyController.getType().toString();
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
