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

	public static bool[] unlocked = new bool[8];
	public Text[] lockedUnlockedTexts = new Text[8];
	public Text[] unitCostWeaponTexts = new Text[8];
	private int currentWeaponInt;

	public Text typeOfWunderWaffenText;

	public GameObject weaponUnlockScreen;

	private Animator playerAnimator;

    private int weaponCost = 5;
    public Text unitCount;
	
	void Start () {
		currentWeapon = weaponFactory.getPistol ();
		currentWeaponInt = 1;
		showEnemyDescription = false;
		enemyDescription.SetActive (false);
		unlocked [0] = true;
		for (int i = 1; i < unlocked.Length; i++) {
			unlocked [i] = false;
		}
		setUnActive ();
		setActive ();
		setTextOfLockUnlock ();
		for (int i = 0; i < 8; i ++) {
			unitCostWeaponTexts[i].text = weaponCost + " Units";
		}
		playerAnimator = gameObject.GetComponent<Animator> ();
	}
	
	void Update () {
		if(Time.time > nextAttack){
			playerAnimator.SetBool("attack", false);
		}
		enemyDescription.SetActive (showEnemyDescription);
		bool Base = BaseController.pause;
		setUnActive ();
		setActive ();
		
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
					lastAttackedEnemy.GetComponent<Seeker>().StopAllCoroutines();
					lastAttackedEnemy.GetComponent<Seeker>().destroyed = true;
					lastAttackedEnemy.die ();
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
				bulletClone.transform.Rotate(90, 0, 0);
				bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
			}
			if(!currentWeapon.getIfAutomatic() && Input.GetMouseButtonDown(0) && Time.time > nextAttack && !currentWeapon.getIfMelee()){
				nextAttack = Time.time + currentWeapon.getAttackSpeed();
				GameObject bulletClone = GameObject.Instantiate(bullet, transform.position + (transform.forward), transform.rotation) as GameObject;
				bulletClone.tag = currentWeapon.getType().toString ();
				bulletClone.GetComponent<Bullet>().dmg = currentWeapon.getWeaponDamage();
				bulletClone.GetComponent<Bullet>().poisonous = currentWeapon.getIfPoisonous();
				bulletClone.GetComponent<Bullet>().stun = currentWeapon.getIfStuns();
				bulletClone.transform.Rotate(90, 0, 0);
				bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
			}
			if (Input.GetMouseButtonDown(0) && Time.time > nextAttack && currentWeapon.getIfMelee()){
				Debug.Log (true);
				playerAnimator.SetBool ("attack", true);
				nextAttack = Time.time + currentWeapon.getAttackSpeed();
				if(lastAttackedEnemy != null){
					int damage = (int)(Random.Range (currentWeapon.getWeaponDamage(), currentWeapon.getWeaponDamage() + 10) * currentWeapon.getType ().damageMultiplierToType(lastAttackedEnemy.getType()) * PlayerAttributes.getAttackMultiplier());
					lastAttackedEnemy.setHealth(lastAttackedEnemy.getHealth () - damage);
					//lastAttackedEnemy.gameObject.transform.Translate(new Vector3((this.gameObject.transform.position.x - lastAttackedEnemy.gameObject.transform.position.x) * currentWeapon.getKnockBack(), 0, (this.gameObject.transform.position.z - lastAttackedEnemy.gameObject.transform.position.z) * currentWeapon.getKnockBack())); 
					if (lastAttackedEnemy.getHealth () <= 0) {
						PSpawner spawner = Camera.main.GetComponent<PSpawner> ();
						spawner.placeUnit (lastAttackedEnemy.gameObject.transform.position);
						EnemySpawner.enemiesDefeaten++;
						lastAttackedEnemy.GetComponent<Seeker> ().StopAllCoroutines ();
						lastAttackedEnemy.GetComponent<Seeker> ().destroyed = true;
						lastAttackedEnemy.destroyed = true;
						lastAttackedEnemy.die ();
						MiniMapScript.enemies.Remove (lastAttackedEnemy);
						PlayerAttributes.getExperience (lastAttackedEnemy.getLevel ());
						PlayerAttacker.lastAttackedEnemy = null;
					}
				}
			}

			if ((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) && unlocked[0]){
				currentWeapon = weaponFactory.getPistol();
				playerAnimator.SetInteger ("weapon", 1);
				currentWeaponInt = 1;
			}
			if ((Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) && unlocked[1]){
				currentWeapon = weaponFactory.getShrimpPistol();
				currentWeaponInt = 2;
				playerAnimator.SetInteger ("weapon", 1);
			}
			if ((Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) && unlocked[2]){
				currentWeapon = weaponFactory.getStingerGun();
				currentWeaponInt = 3;
				playerAnimator.SetInteger ("weapon", 1);
			}
			if ((Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)) && unlocked[3]){
				currentWeapon = weaponFactory.getWeaponizedEel();
				currentWeaponInt = 4;
				playerAnimator.SetInteger ("weapon", 3);
			}
			if ((Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)) && unlocked[4]){
				currentWeapon = weaponFactory.getWunderwuffen();
				currentWeapon.setType(new Type(1));
				currentWeaponInt = 5;
				playerAnimator.SetInteger ("weapon", 1);
			}
			if ((Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6)) && unlocked[5]){
				currentWeapon = weaponFactory.getBatteringRam();
				currentWeaponInt = 6;
				playerAnimator.SetInteger ("weapon", 3);
			}
			if ((Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7)) && unlocked[6]){
				currentWeapon = weaponFactory.getSwordfish();
				currentWeaponInt = 7;
				playerAnimator.SetInteger ("weapon", 2);
			}
			if ((Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8)) && unlocked[7]){
				currentWeapon = weaponFactory.getBaseballBat();
				currentWeaponInt = 8;
				playerAnimator.SetInteger ("weapon", 2);
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

	private void setActive(){
		if (currentWeaponInt == 1) {
			pistolImage.color = Color.red;
		}
		if (currentWeaponInt == 2) {
			shrimpImage.color = Color.red;
		}
		if (currentWeaponInt == 3) {
			stingerImage.color = Color.red;
		}
		if (currentWeaponInt == 4) {
			eelText.color = Color.red;
		}
		if (currentWeaponInt == 5) {
			wunderwuffenText.color = Color.red;
			typeOfWunderWaffenText.color = Color.red;
		}
		if (currentWeaponInt == 6) {
			batteringRamImage.color = Color.red;
		}
		if (currentWeaponInt == 7) {
			swordfishText.color = Color.red;
		}
		if (currentWeaponInt == 8) {
			baseBallBatImage.color = Color.red;
		}
	}

	private void setUnActive(){
		if (!unlocked [0]) {
			pistolImage.color = Color.gray;
		} else {
			pistolImage.color = Color.black;
		}
		if (!unlocked [1]) {
			shrimpImage.color = Color.gray;
		} else {
			shrimpImage.color = Color.black;
		}
		if (!unlocked [2]) {
			stingerImage.color = Color.gray;
		} else {
			stingerImage.color = Color.black;
		}
		if (!unlocked [3]) {
			eelText.color = Color.gray;
		} else {
			eelText.color = Color.black;
		}
		if (!unlocked [4]) {
			wunderwuffenText.color = Color.gray;
			typeOfWunderWaffenText.color = Color.gray;
		} else {
			wunderwuffenText.color = Color.black;
			typeOfWunderWaffenText.color = Color.black;
		}
		if (!unlocked [5]) {
			batteringRamImage.color = Color.gray;
		} else {
			batteringRamImage.color = Color.black;
		}
		if (!unlocked [6]) {
			swordfishText.color = Color.gray;
		} else {
			swordfishText.color = Color.black;
		}
		if (!unlocked [7]) {
			baseBallBatImage.color = Color.gray;
		} else {
			baseBallBatImage.color = Color.black;
		}

	}

	public static void unlock(int i){
		unlocked [i - 1] = true;
	}

	public void unlockInt(int i){
		if(PlayerController.getCount() >= weaponCost){
		    unlocked [i - 1] = true;
		    setTextOfLockUnlock ();

            PlayerController.setCount(weaponCost);
            unitCount.text = "Amount of units:" + PlayerController.getCount();
		}
	}

	public void setTextOfLockUnlock(){
		for (int i = 0; i < 8; i ++) {
			if (unlocked [i]) {
				lockedUnlockedTexts [i].text = "Unlocked";
			} else {
				lockedUnlockedTexts [i].text = "Locked";
			}
		}
	}
}