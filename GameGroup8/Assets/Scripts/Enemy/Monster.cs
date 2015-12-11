using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Monster {

	[XmlAttribute("level")]
	public int level;

	[XmlAttribute("health")]
	public int health;

	[XmlAttribute("maxHealth")]
	public int maxHealth;

	[XmlAttribute("attackPower")]
	public int attackPower;

    [XmlAttribute("walkingSpeed")]
    public float walkingSpeed;

	[XmlAttribute("type")]	
    public string type { get; private set; }

    [XmlAttribute("location")]
	public Vector3 location;

    [XmlAttribute("rotation")]
    public Quaternion rotation;

    [XmlAttribute("isPosioned?")]
    public bool isPoisoned;

    [XmlAttribute("isStunned?")]
    public bool isStunned;

	public Monster(){

	}

	public Monster(EnemyController enemy){
		level = enemy.getLevel ();
		health = enemy.getHealth ();
		maxHealth = enemy.getMaxHealth ();
		attackPower = enemy.getAttackPower ();
		walkingSpeed = enemy.getWalkingSpeed ();
		type = enemy.getType().toString();
		location = enemy.getPosition ();
        rotation = enemy.getRotation();
        isPoisoned = enemy.getPoisoned();
        isStunned = enemy.getStunned();

	}

}
