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

	public Turret(){
	}

	public Turret(BuildingController bc){
		name = bc.name;
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
