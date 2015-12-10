using UnityEngine;
using System.Collections;

public class Building {
	
	private bool turret;
	private bool bed;
	private bool gearShack;
	private Type type;
	private string name;
	private int cost;

	public Building(bool turret, bool bed, bool gearShack, Type type, string name, int cost){
		this.turret = turret;
		this.bed = bed;
		this.gearShack = gearShack;
		this.type = type;
		this.name = name;
		this.cost = cost;
	}

	public bool returnIfTurret(){
		return turret;
	}

	public bool returnIfBed(){
		return bed;
	}

	public bool returnIfGearShack(){
		return gearShack;
	}

	public Type getType(){
		return type;
	}

	public string getName(){
		return name;
	}

	public int getCost(){
		return cost;
	}

}

