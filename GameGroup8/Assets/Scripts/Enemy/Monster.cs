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
	public float location_x;
    public float location_y;
    public float location_z;

    [XmlAttribute("rotation")]
    public float rotation_w;
    public float rotation_x;
    public float rotation_y;
    public float rotation_z;

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

		location_x = enemy.getPosition().x;
        location_y = enemy.getPosition().y;
        location_z = enemy.getPosition().z;

        rotation_w = enemy.getRotation().w;
        rotation_x = enemy.getRotation().x;
        rotation_y = enemy.getRotation().y;
        rotation_z = enemy.getRotation().z;


                
        isPoisoned = enemy.getPoisoned();
        isStunned = enemy.getStunned();

	}

}
