using UnityEngine;
using System.Collections;

public class Enemy {

	public int level;
	public int maxHealth;
	public int attackPower;
	public double walkingSpeed;

	public GameObject gameObject;

	public Enemy(int level, int maxHealth, int attackPower, double walkingSpeed){
		this.level = level;
		this.maxHealth = maxHealth;
		this.attackPower = attackPower;
	}
	
	public Enemy getNormalEnemy() {
		return new Enemy (1, 100, 10, 0.5);
	}

	public Enemy getBetterEnemy() {
		return new Enemy (2, 200, 15, 0.25);
	}

	public Enemy getEnemy(string name){
		if (name.Equals ("normal")) {
			return getNormalEnemy ();
		} else if (name.Equals ("better")) {
			return getBetterEnemy ();
		} else {
			return getNormalEnemy ();
		}
	}

	public int getLevel(){
		return level;
	}

	public int getMaxHealth(){
		return maxHealth;
	}

	public int getAttackPower() {
		return attackPower;
	}

	public double getWalkingSpeed() {
		return walkingSpeed;
	}




	

}
