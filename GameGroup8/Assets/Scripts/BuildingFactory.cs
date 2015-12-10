using UnityEngine;
using System.Collections;

public class BuildingFactory {

	public BuildingFactory(){
	}

	public Building getBuilding(string name){
		if (name.Equals ("Rock-paper-scissor turret(Clone)")) {
			return new Building (true, false, false, new Type (0), "Rock-paper-scissor turret", 5);
		}
		if (name.Equals ("Cat-a-pult(Clone)")) {
			return new Building (true, false, false, new Type (2), "Cat-a-pult", 10);
		}
		if (name.Equals ("Snailgun(Clone)")) {
			return new Building (true, false, false, new Type (3), "Snailgun", 10);
		}
		if (name.Equals ("Harpgoon(Clone)")) {
			return new Building (true, false, false, new Type (1), "Harpgoon", 10);
		}
		if (name.Equals ("Bed(Clone)")) {
			return new Building (false, true, false, new Type (0), "Bed", 10);
		}
		if (name.Equals ("EnergyBed(Clone)")) {
			return new Building (false, true, false, new Type (0), "EnergyBed", 15);
		}
		if (name.Equals ("HealthBed(Clone)")) {
			return new Building (false, true, false, new Type (0), "HealthBed", 15);
		}
		if (name.Equals ("GearShack(Clone)")) {
			return new Building (false, false, true, new Type (0), "GearShack", 10);
		}
		if (name.Equals ("Generator(Clone)")) {
			return new Building (false, false, true, new Type (0), "Generator", 25);
		}
		if (name.Equals ("GunSmith(Clone)")) {
			return new Building (false, false, true, new Type (0), "GunSmith", 25);
		}
		if (name.Equals ("TechSmith(Clone)")) {
			return new Building (false, false, true, new Type (0), "TechSmith", 25);
		} else {
			return new Building (true, false, false, new Type (0), "Rock-Paper-Scissor turret", 5);
		}
	}
}

