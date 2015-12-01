using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttacker : MonoBehaviour {

	public GameObject enemyDescription;
	public bool showEnemyDescription;

	public Text enemyDescriptionText;
	public Slider enemyHealthBar;
	public Text enemyWeaponDamageText;
	public Text enemyLevelText;

	public static Type currentType;
	public Text playerWeaponText;

	public GameObject bullet;
	public float bulletSpeed = 1000f;
	public int meleeAttackPower = 30;

	public float attackRate = 0.5f;
	private float nextAttack = 0.0f;

	public bool meleeAttack = true;
	public bool rangedAttack = false;
	public Text playerWeaponStyleText;

	public static EnemyController lastAttackedEnemy;
		
	void Start () {
		showEnemyDescription = false;
		enemyDescription.SetActive (false);
		currentType = new Type (1);

		playerWeaponText.text = "Weapon: " + currentType.toString () + "-type";
		if (meleeAttack) {
			playerWeaponStyleText.text = "Melee";
		} else {
			playerWeaponStyleText.text = "Ranged";
		}
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
			if (Input.GetMouseButtonDown(0) && Time.time > nextAttack && rangedAttack){
				nextAttack = Time.time + attackRate;
				GameObject bulletClone = GameObject.Instantiate(bullet, transform.position + (transform.forward), transform.rotation) as GameObject;
				bulletClone.tag = currentType.toString ();
                bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            }
			if (Input.GetMouseButtonDown(0) && Time.time > nextAttack && meleeAttack && lastAttackedEnemy != null){
				nextAttack = Time.time + attackRate;
				int damage = (int)(Random.Range (meleeAttackPower, 40) * currentType.damageMultiplierToType(lastAttackedEnemy.getType()) * PlayerAttributes.getAttackMultiplier());
				lastAttackedEnemy.setHealth(lastAttackedEnemy.getHealth () - damage);
				if(lastAttackedEnemy.getHealth () <= 0){
					EnemySpawner.enemiesDefeaten++;
					Destroy(lastAttackedEnemy.gameObject);
					MiniMapScript.enemies.Remove(lastAttackedEnemy);
					PlayerAttributes.getExperience(lastAttackedEnemy.getLevel());
					PlayerAttacker.lastAttackedEnemy = null;
				}
			}
        }
		if(Input.GetMouseButtonDown (1)){
			currentType.nextType();
			playerWeaponText.text = "Weapon: " + currentType.toString () + "-type";
		}
		if(Input.GetMouseButtonDown (2)){
			meleeAttack = !meleeAttack;
			rangedAttack = !rangedAttack;
			if (meleeAttack) {
				playerWeaponStyleText.text = "Melee";
			} else {
				playerWeaponStyleText.text = "Ranged";
			}
		}
	}

	public void setEnemyDescription(EnemyController enemyController){
		enemyDescriptionText.text = "Enemy Type: " + enemyController.getType().toString();
		enemyHealthBar.value = (float)enemyController.getHealth () / 100f;
		enemyWeaponDamageText.text = "Weapon Damage:" + enemyController.getAttackPower ();
		enemyLevelText.text = "Level: " + enemyController.getLevel ();
		showEnemyDescription = true;
	}

	public void setEnemyDescriptionOff(){
		showEnemyDescription = false;
	}

	public void setEnemyDescriptionOn(){
		showEnemyDescription = true;
	}

	public void OnTriggerEnter(Collider col){
		if (meleeAttack && col.gameObject.CompareTag ("Enemy")) {
			lastAttackedEnemy = col.gameObject.GetComponent<EnemyController>();
		}
	}


}
