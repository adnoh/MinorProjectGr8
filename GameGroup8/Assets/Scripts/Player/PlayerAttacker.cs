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
	
	public GameObject bullet;
	public float bulletSpeed = 1000f;
	public static Weapon currentWeapon;

	private WeaponFactory weaponFactory = new WeaponFactory();

	private float nextAttack = 0.0f;
	
	public static EnemyController lastAttackedEnemy;
	
	public GameObject weaponPanel;
	
	public Image pistolImage;
	public Image shrimpImage;
	public Image stingerImage;
	public Text eelText;
	public Text wunderwuffenText;
	public Image batteringRamImage;
	public Text swordfishText;
	public Image baseBallBatImage;

	public Text typeOfWunderWaffenText;
	
	void Start () {
		currentWeapon = weaponFactory.getPistol ();
		setActive (pistolImage);
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
		
		if (!Base){
			if(currentWeapon.getIfElectric() && Input.GetMouseButton(0) && lastAttackedEnemy != null){
				lastAttackedEnemy.health -= (int)(2 * currentWeapon.getType ().damageMultiplierToType(lastAttackedEnemy.getType()) * PlayerAttributes.getAttackMultiplier());
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
			if(!currentWeapon.getIfElectric() && currentWeapon.getIfAutomatic() && Input.GetMouseButton(0) && Time.time > nextAttack && !currentWeapon.getIfMelee()){
				nextAttack = Time.time + currentWeapon.getAttackSpeed();
				GameObject bulletClone = GameObject.Instantiate(bullet, transform.position + (transform.forward), transform.rotation) as GameObject;
				bulletClone.tag = currentWeapon.getType().toString ();
				bulletClone.GetComponent<Bullet>().dmg = currentWeapon.getWeaponDamage();
				bulletClone.GetComponent<Bullet>().poisonous = currentWeapon.getIfPoisonous();
				bulletClone.GetComponent<Bullet>().stun = currentWeapon.getIfStuns();
				bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
			}
			if(!currentWeapon.getIfAutomatic() && Input.GetMouseButtonDown(0) && Time.time > nextAttack && !currentWeapon.getIfMelee()){
				nextAttack = Time.time + currentWeapon.getAttackSpeed();
				GameObject bulletClone = GameObject.Instantiate(bullet, transform.position + (transform.forward), transform.rotation) as GameObject;
				bulletClone.tag = currentWeapon.getType().toString ();
				bulletClone.GetComponent<Bullet>().dmg = currentWeapon.getWeaponDamage();
				bulletClone.GetComponent<Bullet>().poisonous = currentWeapon.getIfPoisonous();
				bulletClone.GetComponent<Bullet>().stun = currentWeapon.getIfStuns();
				bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
			}
			if (Input.GetMouseButtonDown(0) && Time.time > nextAttack && currentWeapon.getIfMelee() && lastAttackedEnemy != null){
				nextAttack = Time.time + currentWeapon.getAttackSpeed();
				int damage = (int)(Random.Range (currentWeapon.getWeaponDamage(), currentWeapon.getWeaponDamage() + 10) * currentWeapon.getType ().damageMultiplierToType(lastAttackedEnemy.getType()) * PlayerAttributes.getAttackMultiplier());
				lastAttackedEnemy.setHealth(lastAttackedEnemy.getHealth () - damage);
				lastAttackedEnemy.gameObject.transform.Translate (new Vector3(lastAttackedEnemy.gameObject.transform.position.x - this.gameObject.transform.position.x, 0, lastAttackedEnemy.gameObject.transform.position.z - this.gameObject.transform.position.z) * currentWeapon.getKnockBack());
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
				currentWeapon = weaponFactory.getPistol();
				setUnActive();
				setActive (pistolImage);
			}
			if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)){
				currentWeapon = weaponFactory.getShrimpPistol();
				setUnActive();
				setActive (shrimpImage);
			}
			if (Input.GetKeyDown(KeyCode.Keypad3)  || Input.GetKeyDown(KeyCode.Alpha3)){
				currentWeapon = weaponFactory.getStingerGun();
				setUnActive();
				setActive (stingerImage);
			}
			if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)){
				currentWeapon = weaponFactory.getWeaponizedEel();
				setUnActive();
				setActive (eelText);
			}
			if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)){
				currentWeapon = weaponFactory.getWunderwuffen();
				currentWeapon.setType(new Type(1));
				typeOfWunderWaffenText.text = currentWeapon.getType().toString();
				setUnActive();
				setActive (wunderwuffenText);
			}
			if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6)){
				currentWeapon = weaponFactory.getBatteringRam();
				setUnActive();
				setActive (batteringRamImage);
			}
			if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7)){
				currentWeapon = weaponFactory.getSwordfish();
				setUnActive();
				setActive (swordfishText);
			}
			if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8)){
				currentWeapon = weaponFactory.getBaseballBat();
				setUnActive();
				setActive (baseBallBatImage);
			}
			if(currentWeapon.getIfChangeable() && Input.GetMouseButtonDown(1)){
				if(currentWeapon.getType().getType() < 3){
					currentWeapon.setType(new Type(currentWeapon.getType().getType() + 1));
					typeOfWunderWaffenText.text = currentWeapon.getType().toString();
				}
				else{
					currentWeapon.setType(new Type(1));
					typeOfWunderWaffenText.text = currentWeapon.getType().toString();
				}
			}

		}
		
	}
	
	public void setEnemyDescription(EnemyController enemyController){
		if (enemyController.getType ().getType () == 1) {
			enemyDescriptionText.text = "Desert Eagle";
		}
		if (enemyController.getType ().getType () == 2) {
			enemyDescriptionText.text = "Fire Fox";
		} else {
			enemyDescriptionText.text = "Hammerhead Shark";
		}
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
		if (col.gameObject.CompareTag ("Enemy")) {
			lastAttackedEnemy = col.gameObject.GetComponent<EnemyController>();
		}
	}

	private void setActive(Image img){
		img.color = Color.red;
	}
	
	private void setUnActive(){
		pistolImage.color = Color.black;
		shrimpImage.color = Color.black;
		stingerImage.color = Color.black;
		eelText.color = Color.black;
		wunderwuffenText.color = Color.black;
		batteringRamImage.color = Color.black;
		swordfishText.color = Color.black;
		baseBallBatImage.color = Color.black;
	}

	private void setActive(Text txt){
		txt.color = Color.red;
	}
}