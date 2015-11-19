using UnityEngine;
using System.Collections;

public class Enemy {

	public int level;
	public int maxHealth;
	public int attackPower;
	public double walkingSpeed;

	public Enemy(int level, int maxHealth, int attackPower, double walkingSpeed){
		this.level = level;
		this.maxHealth = maxHealth;
		this.attackPower = attackPower;
		this.walkingSpeed = walkingSpeed;
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
