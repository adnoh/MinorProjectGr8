using UnityEngine;
using System.Collections;

public class Building {
	
	private bool turret;
	private bool bed;
	private bool gearShack;
	private Type type;
	private string name;
	private int cost;

	/// <summary>
	/// Constructor for the building object.
	/// </summary>
	/// <param name="turret">If set to <c>true</c> the building is a turret</param>
	/// <param name="bed">If set to <c>true</c> the building is a bed</param>
	/// <param name="gearShack">If set to <c>true</c> the building is a gearshack</param>
	/// <param name="type">the Type of the building</param>
	/// <param name="name">the name of the building</param>
	/// <param name="cost">the cost of the building</param>
	public Building(bool turret, bool bed, bool gearShack, Type type, string name, int cost){
		this.turret = turret;
		this.bed = bed;
		this.gearShack = gearShack;
		this.type = type;
		this.name = name;
		this.cost = cost;
	}

	/// <summary>
	/// Returns if turret.
	/// </summary>
	/// <returns><c>true</c>, if if turret was returned, <c>false</c> otherwise.</returns>
	public bool returnIfTurret(){
		return turret;
	}

	/// <summary>
	/// Returns if bed.
	/// </summary>
	/// <returns><c>true</c>, if if bed was returned, <c>false</c> otherwise.</returns>
	public bool returnIfBed(){
		return bed;
	}

	/// <summary>
	/// Returns if gear shack.
	/// </summary>
	/// <returns><c>true</c>, if if gear shack was returned, <c>false</c> otherwise.</returns>
	public bool returnIfGearShack(){
		return gearShack;
	}

	/// <summary>
	/// Gets the type.
	/// </summary>
	/// <returns>The type.</returns>
	public Type getType(){
		return type;
	}

	/// <summary>
	/// Gets the name.
	/// </summary>
	/// <returns>The name.</returns>
	public string getName(){
		return name;
	}

	/// <summary>
	/// Gets the cost.
	/// </summary>
	/// <returns>The cost.</returns>
	public int getCost(){
		return cost;
	}

}

