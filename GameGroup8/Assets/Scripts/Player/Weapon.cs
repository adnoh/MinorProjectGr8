using UnityEngine;
using System.Collections;

public class Weapon {

	private int weaponDamage;
	private float attackSpeed;
	private bool automatic;
	private Type type;
	private bool poisonous;
	private bool melee;
	private bool electric;
	private bool stuns;
	private bool changeable;
	private float knockBack;

	/// <summary>
	/// Constructor for the weapon object.
	/// </summary>
	/// <param name="dmg">the damage the weapon does</param>
	/// <param name="attsp">the attackspeed of the weapon</param>
	/// <param name="aut">If set to <c>true</c> the weapon is automatic</param>
	/// <param name="type">the type of the weapon</param>
	/// <param name="pois">If set to <c>true</c> the weapon is poisonous</param>
	/// <param name="melee">If set to <c>true</c> the weapon is melee</param>
	/// <param name="eel">If set to <c>true</c> the weapon is a weaponized eel</param>
	/// <param name="stuns">If set to <c>true</c> the weapon stuns</param>
	/// <param name="changeable">If set to <c>true</c> the type of the weapon is changeable</param>
	/// <param name="knockBack">The amount of knockback of the weapon (melee only)</param>
	public Weapon(int dmg, float attsp, bool aut, Type type, bool pois, bool melee, bool eel, bool stuns, bool changeable, float knockBack){
		weaponDamage = dmg;
		attackSpeed = attsp;
		automatic = aut;
		this.type = type;
		poisonous = pois;
		this.melee = melee;
		electric = eel;
		this.stuns = stuns;
		this.changeable = changeable;
		this.knockBack = knockBack;
	}

	/// <summary>
	/// Gets the weapon damage.
	/// </summary>
	/// <returns>The weapon damage.</returns>
	public int getWeaponDamage(){
		return weaponDamage;
	}

	/// <summary>
	/// Gets the attack speed.
	/// </summary>
	/// <returns>The attack speed.</returns>
	public float getAttackSpeed(){
		return attackSpeed;
	}

	/// <summary>
	/// Gets if automatic.
	/// </summary>
	/// <returns><c>true</c>, if if automatic was gotten, <c>false</c> otherwise.</returns>
	public bool getIfAutomatic(){
		return automatic;
	}

	/// <summary>
	/// Gets the type.
	/// </summary>
	/// <returns>The type.</returns>
	public Type getType(){
		return type;
	}

	/// <summary>
	/// Gets if poisonous.
	/// </summary>
	/// <returns><c>true</c>, if if poisonous was gotten, <c>false</c> otherwise.</returns>
	public bool getIfPoisonous() {
		return poisonous;
	}

	/// <summary>
	/// Gets if melee.
	/// </summary>
	/// <returns><c>true</c>, if if melee was gotten, <c>false</c> otherwise.</returns>
	public bool getIfMelee(){
		return melee;
	}

	/// <summary>
	/// Gets if weaponized eel.
	/// </summary>
	/// <returns><c>true</c>, if if electric was gotten, <c>false</c> otherwise.</returns>
	public bool getIfElectric(){
		return electric;
	}

	/// <summary>
	/// Gets if stuns.
	/// </summary>
	/// <returns><c>true</c>, if if stuns was gotten, <c>false</c> otherwise.</returns>
	public bool getIfStuns(){
		return stuns;
	}

	/// <summary>
	/// Gets if the type of the weapon is changeable.
	/// </summary>
	/// <returns><c>true</c>, if if changeable was gotten, <c>false</c> otherwise.</returns>
	public bool getIfChangeable(){
		return changeable;
	}

	/// <summary>
	/// Changes the type of the weapon.
	/// </summary>
	/// <param name="type">Type.</param>
	public void changeType(Type type){
		if(changeable){
			this.type = type;
		}
	}

	/// <summary>
	/// Gets the knockback.
	/// </summary>
	/// <returns>The knock back.</returns>
	public float getKnockBack(){
		return knockBack;
	}

	/// <summary>
	/// Sets the type.
	/// </summary>
	/// <param name="type">Type.</param>
	public void setType(Type type){
		this.type = type;
	}
}