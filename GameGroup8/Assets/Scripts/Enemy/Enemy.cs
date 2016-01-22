using UnityEngine;
using System.Collections;

public class Enemy {

	public int level;
	public int maxHealth;
	public int attackPower;
	public float walkingSpeed;

	public Type type;
    public string name;

	/// <summary>
	/// Constructor for the Enemy object
	/// </summary>
	/// <param name="level">Level.</param>
	/// <param name="maxHealth">Max health.</param>
	/// <param name="attackPower">Attack power.</param>
	/// <param name="walkingSpeed">Walking speed.</param>
	/// <param name="type">Type.</param>
	/// <param name="name">Name.</param>
	public Enemy(int level, int maxHealth, int attackPower, float walkingSpeed, Type type, string name){
		this.level = level;
		this.maxHealth = maxHealth;
		this.attackPower = attackPower;
		this.walkingSpeed = walkingSpeed;
		this.type = type;
        this.name = name;
	}

	/// <summary>
	/// Gets the level.
	/// </summary>
	/// <returns>The level.</returns>
	public int getLevel(){
		return level;
	}

	/// <summary>
	/// Gets the max health.
	/// </summary>
	/// <returns>The max health.</returns>
	public int getMaxHealth(){
		return maxHealth;
	}

	/// <summary>
	/// Gets the attack power.
	/// </summary>
	/// <returns>The attack power.</returns>
	public int getAttackPower() {
		return attackPower;
	}

	/// <summary>
	/// Gets the walking speed.
	/// </summary>
	/// <returns>The walking speed.</returns>
	public float getWalkingSpeed() {
		return walkingSpeed;
	}

	/// <summary>
	/// Gets the type.
	/// </summary>
	/// <returns>The type.</returns>
	public Type getType(){
		return type;
	}

	/// <summary>
	/// Gets the name.
	/// </summary>
	/// <returns>The name.</returns>
    public string getName(){
        return name;
    }
}
