using UnityEngine;
using System.Collections;

public class WeaponFactory {

	/// <summary>
	/// Constructor for the Weapon Factory.
	/// </summary>
	public WeaponFactory(){
	}

	/// <summary>
	/// Gets the pistol.
	/// </summary>
	/// <returns>The pistol.</returns>
	public Weapon getPistol(){
		return new Weapon (10, 0.5f, false, new Type (0), false, false, false, false, false, 0f);
	}

	/// <summary>
	/// Gets the shrimp pistol.
	/// </summary>
	/// <returns>The shrimp pistol.</returns>
	public Weapon getShrimpPistol(){
		return new Weapon (40, 2f, false, new Type (3), false, false, false, true, false, 0f);
	}

	/// <summary>
	/// Gets the stinger gun.
	/// </summary>
	/// <returns>The stinger gun.</returns>
	public Weapon getStingerGun(){
		return new Weapon (5, 0.125f, true, new Type (2), true, false, false, false, false, 0f);
	}

	/// <summary>
	/// Gets the weaponized eel.
	/// </summary>
	/// <returns>The weaponized eel.</returns>
	public Weapon getWeaponizedEel(){
		return new Weapon(5, 0.1f, true, new Type(1), false, false, true, false, false, 0f);
    }

	/// <summary>
	/// Gets the wunderwuffen.
	/// </summary>
	/// <returns>The wunderwuffen.</returns>
	public Weapon getWunderwuffen(){
		return new Weapon(35, 1f, true, new Type(0), false, false, false, false, true, 0f);
	}

	/// <summary>
	/// Gets the battering ram.
	/// </summary>
	/// <returns>The battering ram.</returns>
	public Weapon getBatteringRam(){
		return new Weapon (40, 2.0f, false, new Type (2), false, true, false, false, false, 4f);
	}

	/// <summary>
	/// Gets the swordfish.
	/// </summary>
	/// <returns>The swordfish.</returns>
	public Weapon getSwordfish(){
		return new Weapon (25, 0.5f, false, new Type (3), false, true, false, false, false, 2f);
	}

	/// <summary>
	/// Gets the baseball bat.
	/// </summary>
	/// <returns>The baseball bat.</returns>
	public Weapon getBaseballBat(){
		return new Weapon (20, 0.75f, false, new Type (1), false, true, false, false, false, 6f);
	}

}
