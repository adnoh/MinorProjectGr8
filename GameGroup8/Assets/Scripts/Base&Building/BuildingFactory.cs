using UnityEngine;
using System.Collections;

public class BuildingFactory {

	/// <summary>
	/// Constructor for the BuildingFactory.
	/// </summary>
	public BuildingFactory(){
	}

	/// <summary>
	/// Gets the building according to the name.
	/// </summary>
	/// <returns>The building.</returns>
	/// <param name="name">Name.</param>
	public Building getBuilding(string name){
		if (name.Equals ("Rock-paper-scissor turret(Clone)")) {
			return new Building (true, false, false, new Type (0), "Rock-paper-scissor turret", 5);
		}else if (name.Equals ("Cat-a-pult(Clone)")) {
			return new Building (true, false, false, new Type (2), "Cat-a-pult", 15);
		}else if (name.Equals ("Snailgun(Clone)")) {
			return new Building (true, false, false, new Type (3), "Snailgun", 15);
		}else if (name.Equals ("Harpgoon(Clone)")) {
			return new Building (true, false, false, new Type (1), "Harpgoon", 15);
		}else if (name.Equals ("Bed(Clone)")) {
			return new Building (false, true, false, new Type (0), "Bed", 10);
		}else if (name.Equals ("EnergyBed(Clone)")) {
			return new Building (false, true, false, new Type (0), "EnergyBed", 15);
		}else if (name.Equals ("HealthBed(Clone)")) {
			return new Building (false, true, false, new Type (0), "HealthBed", 15);
		}else if (name.Equals ("GearShack(Clone)")) {
			return new Building (false, false, true, new Type (0), "GearShack", 10);
		}else if (name.Equals ("Generator(Clone)")) {
			return new Building (false, false, true, new Type (0), "Generator", 25);
		}else if (name.Equals ("GunSmith(Clone)")) {
			return new Building (false, false, true, new Type (0), "GunSmith", 25);
		}else if (name.Equals ("TechSmith(Clone)")) {
			return new Building (false, false, true, new Type (0), "TechSmith", 25);
		}else {
			return new Building (true, false, false, new Type (0), "Rock-Paper-Scissor turret", 5);
		}
	}
}