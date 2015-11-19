using UnityEngine;
using System.Collections;

public class Enemy {

	public int level;
	public int maxHealth;
	public int attackPower;
	public float walkingSpeed;

	public Enemy(int level, int maxHealth, int attackPower, float walkingSpeed){
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

	public float getWalkingSpeed() {
		return walkingSpeed;
	}




	

}
