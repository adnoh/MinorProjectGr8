using UnityEngine;
using System.Collections;

public class Turret{

	public string name;
	public float x;
	public float y;
	public float z;
	public float wRot;
	public float xRot;
	public float yRot;
	public float zRot;
	public float timeTillNextAttack;
	public float timeTillNext;

	/// <summary>
	/// Constructor for the turret object. Used for saving buildings.
	/// </summary>
	public Turret(){
	}

	/// <summary>
	/// Constructor for the turret object. Saves only the important fields of the BuildingConstructor script attached to the building.
	/// </summary>
	/// <param name="bc">Bc.</param>
	public Turret(BuildingController bc){
		name = bc.getBuilding().getName();
		x = bc.gameObject.transform.position.x;
		y = bc.gameObject.transform.position.y;
		z = bc.gameObject.transform.position.z;
		wRot = bc.gameObject.transform.rotation.w;
		xRot = bc.gameObject.transform.rotation.x;
		yRot = bc.gameObject.transform.rotation.y;
		zRot = bc.gameObject.transform.rotation.z;
		timeTillNextAttack = bc.timeToNextAttack;
		timeTillNext = bc.timeInterval;
	}
}
