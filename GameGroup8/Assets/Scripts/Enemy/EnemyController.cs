using UnityEngine;
using System.Collections;
using System;


/* this class actually lives in the scene, Enemy.cs not 
can also access all variables defined in Enemy.cs
*/

public class EnemyController : MonoBehaviour {

	public Enemy enemy;
	private EnemyFactory enemyFactory = new EnemyFactory();
    

    // Properties of the actual instance of the enemy
	public int level;
	public int health;
	public int maxHealth;
	public int attackPower;
	public float walkingSpeed;
	public Type type;

    public Vector3 position;
    public Quaternion rotation;

    private bool isWithinRange;

	public float attackRate = 2f;
	private float nextAttack = 0.0f;

    public bool poisoned;
    public float timeToGetPoisonDamage = 5.0f;
	private float intervalToGetPoisonDamage = 5.0f;

    public bool stunned;

    public float timeToUnStun = 0.0f;
	public float stunTimeInterval = 2.0f;

	void Start () {
		level = this.gameObject.GetComponent<EnemyController> ().getLevel ();
		if (this.gameObject.transform.name.Equals ("HammerHeadPrefab(Clone)")) {
			enemy = enemyFactory.getEnemy ("water", level);
		} else if (this.gameObject.transform.name.Equals ("DesertEaglePrefab(Clone)")) {
			enemy = enemyFactory.getEnemy ("wind", level);
		} else if (this.gameObject.transform.name.Equals ("FireFoxPrefab(Clone)")) {
			enemy = enemyFactory.getEnemy ("earth", level);
		}
				
		level = enemy.getLevel ();
		maxHealth = enemy.getMaxHealth();
		health = enemy.getMaxHealth();
		attackPower = enemy.getAttackPower();
		walkingSpeed = enemy.getWalkingSpeed();
		type = enemy.getType ();

		isWithinRange = false;
		MiniMapScript.enemies.Add (this);
	}

	void Update () {
		this.gameObject.transform.LookAt (GameObject.Find ("player").transform.position);;
		if (isWithinRange && Time.time > nextAttack) {
			nextAttack = Time.time + attackRate;
			attack ();
		}
		if (poisoned && Time.time > timeToGetPoisonDamage) {
			timeToGetPoisonDamage = Time.time + intervalToGetPoisonDamage;
			health -= (int)maxHealth / 20;
		}
		if (stunned && Time.time > timeToUnStun) {
			timeToUnStun = Time.time + stunTimeInterval;
			unStun ();
		}

		Vector3 EnemyPosition = this.gameObject.transform.position;
		setPosition (EnemyPosition);
        Quaternion EnemyRotation = this.gameObject.transform.rotation;
        setRotation(EnemyRotation);



    }


    // basic pathfinding code
	/* void FixedUpdate ()	{
		 position = PlayerController.getPosition ();
		if (!isWithinRange) {
			transform.position = Vector3.MoveTowards (transform.position, position, speed * Time.deltaTime);
		}
	} */


    // Getters and setters

	public int getHealth() {
		return health;
	}

	public int getMaxHealth() {
		return maxHealth;
	}

    public void setMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void setHealth(int health) {
		this.health = health;
	}


	public int getLevel(){
		return level;
	}

	public float getWalkingSpeed(){
		return walkingSpeed;
	}

    public void setWalkingSpeed(float WP)
    {
        this.walkingSpeed = WP;
    }

    public void setLevel(int level){
		this.level = level;
	}

	public int getAttackPower() {
		return attackPower;
	}

    public void setAttackPower(int AP)
    {
        this.attackPower = AP;
    }



    public Type getType() {
		return type;
	}

	public Enemy getEnemy() {
		return enemy;
	}

	public void setWithinRange() {
		isWithinRange = !isWithinRange;
		nextAttack = 1.0f;
		if (isWithinRange) {
			PlayerAttacker.lastAttackedEnemy = this; 
		} else {
			PlayerAttacker.lastAttackedEnemy = null;
		}
	}

	public void attack() {
		PlayerAttributes.takeDamage(attackPower);
	}


    // Poisoned
	public void setPoisoned(bool isPoisoned){
		poisoned = isPoisoned;
	}

    public bool getPoisoned()
    {
        return poisoned;
    }

    // Stun
    public void setStunned(){
		StartCoroutine (stun ());
	}

	public void unStun(){
		walkingSpeed = enemy.getWalkingSpeed();
		attackPower = enemy.getAttackPower();
		stunned = false;
	}

    public bool getStunned()
    {
        return stunned;
    }

    IEnumerator stun(){
		walkingSpeed = 0;
		attackPower = 0;
		yield return new WaitForSeconds (2);
		walkingSpeed = enemy.getWalkingSpeed ();
		attackPower = enemy.getAttackPower ();
	}

    // transform.position
    void setPosition(Vector3 here){
		position = here;
	}
	
	public Vector3 getPosition(){
		return position;
	}

    // transform.Rotation
    public void setRotation(Quaternion _rotation)
    {
        rotation = _rotation;
    }

    public Quaternion getRotation()
    {
        return rotation;
    }
}
