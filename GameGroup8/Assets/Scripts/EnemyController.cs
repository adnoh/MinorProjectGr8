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
		enemy = enemyFactory.getEnemy ("normal");
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


	
}
