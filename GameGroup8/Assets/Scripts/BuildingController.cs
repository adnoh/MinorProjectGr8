using UnityEngine;
using System.Collections;

public class BuildingController : MonoBehaviour {

    private GameObject enemy;
    private Vector3 enemyPosition;

	private Building building;
	private BuildingFactory buildingFactory = new BuildingFactory();

	public float timeInterval = 30.0f;
	private float time = 30.0f;

	void Start(){
		building = buildingFactory.getBuilding (this.gameObject.name);
		if (building.getName ().Equals ("FatiqueBed")) {
			PlayerAttributes.fatique += 5000;
		}

	}
    
    void Update(){
        if (enemy && building.returnIfTurret()){
            enemyPosition = enemy.transform.position;
            enemyPosition.y = 0;
            transform.LookAt(enemyPosition);
            transform.Rotate(new Vector3 (0, 1, 0), 90);
        }
		if (Time.time > time) {
			time = Time.time + timeInterval;
			if(building.returnIfBed()){
				if(building.getName ().Equals ("Bed")){
					PlayerAttributes.replenish();
				}
				if(building.getName().Equals("HealthBed")){
					PlayerAttributes.regenerate();
				}
			}
			if(building.getName().Equals ("Generator")){
				PlayerController.setCount(-1);
			}
		}
    }

    void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy" && building.returnIfTurret()){
            enemy = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other){
		if (other.tag == "Enemy" && building.returnIfTurret()){
            Vector3 LastPostition = other.transform.position;
            enemyPosition = LastPostition;
            enemy = null;
        }     
    }
}
