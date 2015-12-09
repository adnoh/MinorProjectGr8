using UnityEngine;
using System.Collections;

public class Building {
	
	private bool turret;
	private bool bed;
	private bool gearShed;
	private Type type;
	private string name;

	public Building(bool turret, bool bed, bool gearShed, Type type, string name){
		this.turret = turret;
		this.bed = bed;
		this.gearShed = gearShed;
		this.type = type;
		this.name = name;
	}

	public bool returnIfTurret(){
		return turret;
	}

	public bool returnIfBed(){
		return bed;
	}

	public bool returnIfGearShed(){
		return gearShed;
	}

	public Type getType(){
		return type;
	}

	public string getName(){
		return name;
	}

}

