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

	public GameObject unit;

	private int type = 1;
	public static Type currentType;

	public GameObject bullet;
	public float bulletSpeed = 1000f;
	public int meleeAttackPower = 30;

	public float attackRate = 0.5f;
	private float nextAttack = 0.0f;

	public bool meleeAttack = true;
	public bool rangedAttack = false;

	public static EnemyController lastAttackedEnemy;

	public GameObject weaponPanel;

	public Text windText;
	public Text earthText;
	public Text waterText;
	public Text meleeText;
	public Text rangedText;
		
	void Start () {
		type = 1;
		currentType = new Type (1);
		meleeAttack = true;
		showEnemyDescription = false;
		enemyDescription.SetActive (false);
		setAttackTypeToWind ();
		setAttackStyleToMelee ();
	}

	void Update () {
		enemyDescription.SetActive (showEnemyDescription);
        bool Base = PlayerController.getPause();
		currentType.setType (type);
		if (meleeAttack) {
			setActive (meleeText);
			setUnActive (rangedText);
		} else {
			setUnActive (meleeText);
			setActive (rangedText);
		}
		if (currentType.getType () == 1) {
			setActive (windText);
			setUnActive (earthText);
			setUnActive (waterText);
		}
		if (currentType.getType () == 2) {
			setUnActive (windText);
			setActive (earthText);
			setUnActive (waterText);
		}
		if (currentType.getType () == 3) {
			setUnActive (windText);
			setUnActive (earthText);
			setActive (waterText);
		}

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
					PSpawner spawner = Camera.main.GetComponent<PSpawner>();
					spawner.placeUnit(lastAttackedEnemy.gameObject.transform.position);
					EnemySpawner.enemiesDefeaten++;
					Destroy(lastAttackedEnemy.gameObject);
					MiniMapScript.enemies.Remove(lastAttackedEnemy);
					PlayerAttributes.getExperience(lastAttackedEnemy.getLevel());
					PlayerAttacker.lastAttackedEnemy = null;
				}
			}
			if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)){
				setAttackTypeToWind();
			}
			if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)){
				setAttackTypeToEarth();
			}
			if (Input.GetKeyDown(KeyCode.Keypad3)  || Input.GetKeyDown(KeyCode.Alpha3)){
				setAttackTypeToWater();
			}
			if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)){
				setAttackStyleToMelee();
			}
			if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)){
				setAttackStyleToRanged();
			}
        }

	}

	public void setEnemyDescription(EnemyController enemyController){
		enemyDescriptionText.text = "Enemy Type: " + enemyController.getType().toString();
		enemyHealthBar.maxValue = (float)enemyController.getMaxHealth ();
		enemyHealthBar.value = (float)enemyController.getHealth ();
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

	public void setAttackStyleToMelee(){
		meleeAttack = true;
		rangedAttack = false;
	}

	public void setAttackStyleToRanged() {
		meleeAttack = false;
		rangedAttack = true;
	}

	public void setAttackTypeToWind() {
		type = 1;
	}

	public void setAttackTypeToEarth() {
		type = 2;
	}

	public void setAttackTypeToWater() {
		type = 3;
	}

	private void setActive(Text text){
		text.color = Color.red;
	}

	private void setUnActive(Text text){
		text.color = Color.black;
	}
}
