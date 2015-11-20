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

	private Rigidbody rb;

	void Start () {
		if (Random.Range (0, 2) == 0) {
			enemy = enemyFactory.getEnemy ("normal");
		} else {
			enemy = enemyFactory.getEnemy ("harder");
		}
		level = enemy.getLevel ();
		maxHealth = enemy.getMaxHealth();
		health = enemy.getMaxHealth();
		attackPower = enemy.getAttackPower();
		speed = enemy.getWalkingSpeed();
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()	{
		position = Controls.getposition ();
		transform.position = Vector3.MoveTowards(transform.position, position,   speed*Time.deltaTime);
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

	public Enemy getEnemy() {
		return enemy;
	}


	
}
