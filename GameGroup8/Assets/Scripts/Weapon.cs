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

	public int getWeaponDamage(){
		return weaponDamage;
	}

	public float getAttackSpeed(){
		return attackSpeed;
	}

	public bool getIfAutomatric(){
		return automatic;
	}

	public Type getType(){
		return type;
	}

	public bool getIfPoisonous() {
		return poisonous;
	}

	public bool getIfMelee(){
		return melee;
	}

	public bool getIfElectric(){
		return electric;
	}

	public bool getIfStuns(){
		return stuns;
	}

	public bool getIfChangeable(){
		return changeable;
	}

	public void changeType(Type type){
		if(changeable){
			this.type = type;
		}
	}

	public float getKnockBack(){
		return knockBack;
	}

}
