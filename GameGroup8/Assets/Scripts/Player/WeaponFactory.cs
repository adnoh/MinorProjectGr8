using UnityEngine;
using System.Collections;

public class WeaponFactory {

	public WeaponFactory(){
	}

	public Weapon getPistol(){
		return new Weapon (10, 0.5f, false, new Type (0), false, false, false, false, false, 0f);
	}

	public Weapon getShrimpPistol(){
		return new Weapon (40, 2f, false, new Type (3), false, false, false, true, false, 0f);
	}

	public Weapon getStingerGun(){
		return new Weapon (5, 0.125f, true, new Type (2), true, false, false, false, false, 0f);
	}

	public Weapon getWeaponizedEel(){
		return new Weapon(5, 0.1f, true, new Type(1), false, false, true, false, false, 0f);
    }

	public Weapon getWunderwuffen(){
		return new Weapon(35, 1f, true, new Type(0), false, false, false, false, true, 0f);
	}

	public Weapon getBatteringRam(){
		return new Weapon (40, 2.0f, false, new Type (2), false, true, false, false, false, 4f);
	}

	public Weapon getSwordfish(){
		return new Weapon (25, 0.5f, false, new Type (3), false, true, false, false, false, 2f);
	}

	public Weapon getBaseballBat(){
		return new Weapon (20, 0.75f, false, new Type (1), false, true, false, false, false, 6f);
	}

}
