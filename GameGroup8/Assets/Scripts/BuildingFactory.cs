using UnityEngine;
using System.Collections;

public class BuildingFactory {

	public BuildingFactory(){
	}

	public Building getBuilding(string name){
		if (name.Equals ("BasicTurret(Clone)")) {
			return new Building (true, false, false, new Type (0), "Basic turret");
		}
		if (name.Equals ("Catapult(Clone)")) {
			return new Building (true, false, false, new Type (1), "Catapult");
		}
		if (name.Equals ("Snailgun(Clone)")) {
			return new Building (true, false, false, new Type (2), "Snailgun");
		}
		if (name.Equals ("Harpgoon(Clone)")) {
			return new Building (true, false, false, new Type (3), "Harpgoon");
		}
		if (name.Equals ("Bed(Clone)")) {
			return new Building (false, true, false, new Type (0), "Bed");
		}
		if (name.Equals ("FatiqueBed(Clone)")) {
			return new Building (false, true, false, new Type (0), "FatiqueBed");
		}
		if (name.Equals ("HealthBed(Clone)")) {
			return new Building (false, true, false, new Type (0), "EnergyBed");
		}
		if (name.Equals ("GearShed(Clone)")) {
			return new Building (false, false, true, new Type (0), "GearShed");
		}
		if (name.Equals ("Generator(Clone)")) {
			return new Building (false, false, true, new Type (0), "Generator");
		}
		if (name.Equals ("WeaponSmith(Clone)")) {
			return new Building (false, false, true, new Type (0), "WeaponSmith");
		}
		if (name.Equals ("GadgetSmith(Clone)")) {
			return new Building (false, false, true, new Type (0), "GadgetSmith");
		} else {
			return new Building (false, false, true, new Type (0), "GadgetSmith");
		}
	}
}

