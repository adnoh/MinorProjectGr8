using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BuildingController : MonoBehaviour {

    public static List<GameObject> enemys = new List<GameObject>();
    private Vector3 enemyPosition;

	private Building building;
	private BuildingFactory buildingFactory = new BuildingFactory();

	public float timeInterval = 10.0f;
	private float time = 10.0f;

	public float timeToNextAttack = 2.0f;
	private float attackTime = 1.0f;

	public GameObject bullet;

	public bool weaponUnlocker = false;

	Animator anim;

	/// <summary>
	/// Implements the effects of a building.
	/// </summary>
	void Start(){
		building = buildingFactory.getBuilding (this.gameObject.name);
		if (building.getName().Equals("Harpgoon") || building.getName().Equals("Cat-a-pult")) {
			anim = GetComponent<Animator> ();
		}else if (building.getName ().Equals ("Bed")) {
			PlayerController.amountOfBeds ++;
			PlayerAttributes.maxFatique += 5000;
		}else if (building.getName ().Equals ("HealthBed")) {
			PlayerController.amountOfBeds ++;
			PlayerAttributes.amountOfHealthBeds++;
		}else if (building.getName ().Equals ("EnergyBed")) {
			PlayerAttributes.setMaxEnergy(PlayerAttributes.getMaxEnergy() * 2);
		}else if (building.getName ().Equals ("GearShack")) {
			PlayerAttacker.unlock(2);
		}else if (building.getName().Equals("Snailgun")){
            this.gameObject.transform.Rotate(0, -90, 0);
        }
    }
    
	/// <summary>
	/// Is called every frame to attacks the first enemy in the list of enemies; or it generates a unit.
	/// </summary>
	void Update(){
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
			if (building.getName().Equals("Harpgoon") || building.getName().Equals("Cat-a-pult")) {
				StartCoroutine (animation ("attack"));
			}
			attackTime = Time.time + timeToNextAttack;
			Type bulletType = building.getType();
			if(building.getType ().getType() == 0){
				bulletType = new Type(Random.Range (1, 4));
			}
			GameObject bulletClone = GameObject.Instantiate(bullet, transform.position, transform.rotation) as GameObject;
			bulletClone.tag = bulletType.toString ();
			if(this.gameObject.name.Equals ("Rock-paper-scissor turret(Clone)")){
				bulletClone.GetComponent<Bullet>().dmg = 10;
			}else{
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
            }else{
                bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f);
            }
		}
		if (building != null && building.getName ().Equals ("Generator") && Time.time > time) {
			PlayerController.setCount(-1);
			time = Time.time + timeInterval;
		}
    }

	/// <summary>
	/// When an enemy enters the sphere collider it's added to the enemy list.
	/// </summary>
	/// <param name="other">Other.</param>
    void OnTriggerEnter(Collider other){
		if (other.CompareTag("Enemy") && building.returnIfTurret() && this.gameObject.CompareTag("Turret")){
            enemys.Add (other.gameObject);
        }
    }

	/// <summary>
	/// When an enemy exits the sphere collider it's removed from the enemy list.
	/// </summary>
	/// <param name="other">Other.</param>
    void OnTriggerExit(Collider other){
		if (other.CompareTag ("Enemy") && building.returnIfTurret() && this.gameObject.CompareTag("Turret")){
            Vector3 LastPosition = other.transform.position;
            enemyPosition = LastPosition;
			enemys.Remove(other.gameObject);
        }     
    }

	/// <summary>
	/// When a building is deleted from the base the effects will be lifted by this method.
	/// </summary>
	public void Delete(){
		if (building.getName ().Equals ("Bed")) {
			PlayerController.amountOfBeds --;
			PlayerAttributes.maxFatique -= 5000;
		}else if (building.getName ().Equals ("HealthBed")) {
			PlayerController.amountOfBeds -= 2;
			PlayerAttributes.amountOfHealthBeds -= 1;
			PlayerAttributes.maxFatique -= 5000;
		}else if (building.getName ().Equals ("EnergyBed")) {
			PlayerAttributes.setMaxEnergy((int)(PlayerAttributes.getMaxEnergy() / 2));
			PlayerAttributes.maxFatique -= 5000;
		}
	}

	/// <summary>
	/// Gets the building.
	/// </summary>
	/// <returns>The building.</returns>
	public Building getBuilding(){
		return building;
	}

	/// <summary>
	/// Plays the attack animation.
	/// </summary>
	/// <param name="an">An.</param>
	IEnumerator animation(string an){
		anim.SetBool (an, true);
		yield return new WaitForSeconds (0.5f);
		anim.SetBool (an, false);
	}
}
