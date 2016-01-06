using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BuildingController : MonoBehaviour {

    public static List<GameObject> enemys = new List<GameObject>();
    private Vector3 enemyPosition;

	private Building building;
	private BuildingFactory buildingFactory = new BuildingFactory();

	public float timeInterval = 30.0f;
	private float time = 30.0f;

	public float timeToNextAttack = 2.0f;
	private float attackTime = 1.0f;

	public GameObject bullet;

	public bool weaponUnlocker = false;

	void Start(){
		building = buildingFactory.getBuilding (this.gameObject.name);
		if (building.getName ().Equals ("Bed")) {
			PlayerController.amountOfBeds ++;
			PlayerAttributes.maxFatique += 5000;
		}
		if (building.getName ().Equals ("HealthBed")) {
			PlayerController.amountOfBeds ++;
			PlayerAttributes.setMaxHealth((int)(PlayerAttributes.getMaxHealth() * (3/2)));
		}
		if (building.getName ().Equals ("EnergyBed")) {
			PlayerAttributes.setMaxEnergy(PlayerAttributes.getMaxEnergy() * 2);
		}
		if (building.getName ().Equals ("GearShack")) {
			PlayerAttacker.unlock(2);
		}
        if (building.getName().Equals("Snailgun"))
        {
            this.gameObject.transform.Rotate(0, -90, 0);
        }
    }
    
    void Update(){
        bool pause = BaseController.getPause();

		for (int i = 0; i < enemys.Count; i++) {
			if (enemys [i] == null) {
				enemys.Remove (enemys[i]);
			}
		}
		if (enemys.Count > 0 && (enemys[0] == null || enemys [0].GetComponent<EnemyController> ().destroyed)) {
			enemys.Remove(enemys[0]);
		}
        if (enemys.Count > 0 && building.returnIfTurret() && enemys[0] != null && !enemys[0].GetComponent<EnemyController>().destroyed){
            enemyPosition = enemys[0].transform.position;
            enemyPosition.y = 0;
            transform.LookAt(enemyPosition);
			if(building.getName().Equals ("Cat-a-pult") || building.getName ().Equals ("Snailgun")){
            	transform.Rotate(new Vector3 (0, 1, 0), 90);
			}
        }
		if (enemys.Count > 0 && Time.time > attackTime && building.returnIfTurret()) {
			attackTime = Time.time + timeToNextAttack;
			Type bulletType = building.getType();
			if(building.getType ().getType() == 0){
				bulletType = new Type(Random.Range (1, 4));
			}
			GameObject bulletClone = GameObject.Instantiate(bullet, transform.position, transform.rotation) as GameObject;
			bulletClone.tag = bulletType.toString ();
			if(this.gameObject.name.Equals ("Rock-paper-scissor turret(Clone)")){
				bulletClone.GetComponent<Bullet>().dmg = 10;
			}
			else{
				bulletClone.GetComponent<Bullet>().dmg = 15;
			}
			if(bulletClone.name.Equals ("newBullet(Clone)")){
				bulletClone.transform.Rotate(90, 0, 0);
			}
			if(bulletClone.name.Equals ("SnailPrefab(Clone)")){
				bulletClone.transform.Rotate (0, 90, 0);
			}
            if (building.getName().Equals("Snailgun") || building.getName().Equals("Cat-a-pult"))
            {
                bulletClone.GetComponent<Rigidbody>().AddForce(-transform.right * 1000f);
            }
            else
            {
                bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f);
            }
		}
		if (building.getName ().Equals ("Generator") && Time.time > time) {
			PlayerController.setCount(-1);
			time = Time.time + timeInterval;
		}

		if(Input.GetKeyDown(KeyCode.T) && pause){
			if (building.getName ().Equals ("GunSmith") && !weaponUnlocker) {
				GameObject.Find ("player").GetComponent<PlayerAttacker> ().weaponUnlockScreen.SetActive (true);
				GameObject.Find ("player").GetComponent<PlayerAttacker> ().setTextOfLockUnlock();
				weaponUnlocker = true;
			} else if (building.getName ().Equals ("GunSmith") && weaponUnlocker) {
				GameObject.Find ("player").GetComponent<PlayerAttacker> ().weaponUnlockScreen.SetActive (false);
				weaponUnlocker = false;
			} 
		}
        else if (!pause)
        {
            GameObject.Find("player").GetComponent<PlayerAttacker>().weaponUnlockScreen.SetActive(false);
            weaponUnlocker = false;
        }
    }

    void OnTriggerEnter(Collider other){
		if (other.CompareTag("Enemy") && building.returnIfTurret() && this.gameObject.CompareTag("Turret")){
            enemys.Add (other.gameObject);
        }
    }

    void OnTriggerExit(Collider other){
		if (other.CompareTag ("Enemy") && building.returnIfTurret() && this.gameObject.CompareTag("Turret")){
            Vector3 LastPostition = other.transform.position;
            enemyPosition = LastPostition;
			enemys.Remove(other.gameObject);
        }     
    }

	public void Delete(){
		if (building.getName ().Equals ("Bed")) {
			PlayerController.amountOfBeds --;
			PlayerAttributes.maxFatique -= 5000;
		}
		if (building.getName ().Equals ("HealthBed")) {
			PlayerController.amountOfBeds -= 2;
			PlayerAttributes.setMaxHealth((int)(-PlayerAttributes.getMaxHealth() * (3/2)));
			PlayerAttributes.maxFatique -= 5000;
		}
		if (building.getName ().Equals ("EnergyBed")) {
			PlayerAttributes.setMaxEnergy((int)(PlayerAttributes.getMaxEnergy() / 2));
			PlayerAttributes.maxFatique -= 5000;
		}
	}

	public Building getBuilding(){
		return building;
	}
}
