using UnityEngine;
using System.Collections;

public class Enemy {

	public int level;
	public int maxHealth;
	public int attackPower;
	public float walkingSpeed;

	public Type type;
    public string name;

	public Enemy(int level, int maxHealth, int attackPower, float walkingSpeed, Type type, string name){
		this.level = level;
		this.maxHealth = maxHealth;
		this.attackPower = attackPower;
		this.walkingSpeed = walkingSpeed;
		this.type = type;
        this.name = name;
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

	public Type getType(){
		return type;
	}

    public string getName()
    {
        return name;
    }
}
