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

	public static EnemyController lastAttackedEnemy;

	public float fireRate = 0.5f;
	private float nextFire = 0.0f;
		
	void Start () {
		showEnemyDescription = false;
		enemyDescription.SetActive (false);
	}

	void Update () {
		enemyDescription.SetActive (showEnemyDescription);

		if (lastAttackedEnemy != null) {
			setEnemyDescription (lastAttackedEnemy);
		} else {
			showEnemyDescription = false;
		}

		if(Input.GetMouseButtonDown(0) && Time.time > nextFire){
			nextFire = Time.time + fireRate;
			GameObject bulletClone = GameObject.Instantiate(bullet, transform.position + (transform.forward), transform.rotation) as GameObject;
			bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
		}
	}

	public void setEnemyDescription(EnemyController enemyController){
		enemyDescriptionText.text = "Level = " + enemyController.getLevel ();
		enemyHealthBar.text = "Health = " + enemyController.getHealth ();
		enemyWeaponDamageText.text = "Weapon Damage = " + enemyController.getAttackPower ();
		showEnemyDescription = true;
	}
}
