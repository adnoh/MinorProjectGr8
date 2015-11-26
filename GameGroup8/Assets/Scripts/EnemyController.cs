using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Enemy enemy;
	private EnemyFactory enemyFactory = new EnemyFactory();
	private Vector3 position;
	public float speed;

	public int level;
	public int health;
	public int maxHealth;
	public int attackPower;
	public float walkingSpeed;
	public Type type;

	private bool isWithinRange;

	public float attackRate = 2f;
	private float nextAttack = 0.0f;

	void Start () {
		if (this.gameObject.transform.name.Equals ("WaterEnemy(Clone)")) {
			enemy = enemyFactory.getEnemy ("water", "normal");
		} else if (this.gameObject.transform.name.Equals ("WindEnemy(Clone)")) {
			enemy = enemyFactory.getEnemy ("wind", "normal");
		} else if (this.gameObject.transform.name.Equals ("EarthEnemy(Clone)")) {
			enemy = enemyFactory.getEnemy ("earth", "normal");
		}
				
		level = enemy.getLevel ();
		maxHealth = enemy.getMaxHealth();
		health = enemy.getMaxHealth();
		attackPower = enemy.getAttackPower();
		speed = enemy.getWalkingSpeed();
		type = enemy.getType ();

		isWithinRange = false;
	}

	void Update () {
		if (isWithinRange && Time.time > nextAttack) {
			nextAttack = Time.time + attackRate;
			attack ();
		}
	}

	void FixedUpdate ()	{
		position = PlayerController.getPosition ();
		if (!isWithinRange) {
			transform.position = Vector3.MoveTowards (transform.position, position, speed * Time.deltaTime);
		}
	}

	public int getHealth() {
		return health;
	}

	public void setHealth(int health) {
		this.health = health;
	}

	public int getLevel(){
		return level;
	}

	public int getAttackPower() {
		return attackPower;
	}

	public Type getType() {
		return type;
	}

	public Enemy getEnemy() {
		return enemy;
	}

	public void setWithinRange() {
		isWithinRange = !isWithinRange;
		if (isWithinRange) {
			PlayerAttacker.lastAttackedEnemy = this; 
		} else {
			PlayerAttacker.lastAttackedEnemy = null;
		}
	}

	public void attack() {
		PlayerController.setHealth (attackPower);
	}


	
}
