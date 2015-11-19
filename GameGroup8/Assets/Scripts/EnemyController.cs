using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {


	private EnemyFactory enemyFactory = new EnemyFactory();
	public Enemy enemy;

	public int level;
	public int maxHealth;
	public int health;
	public int attackPower;
	public double speed;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		enemy = enemyFactory.getNormalEnemy ();
		level = enemy.getLevel ();
		speed = enemy.getWalkingSpeed ();
		maxHealth = enemy.getMaxHealth ();
		health = enemy.getMaxHealth ();
		attackPower = enemy.getAttackPower ();
	}

	public int getHealth(){
		return health;
	}

	public void setHealth(int health){
		this.health = health;
	}

	public int getLevel() {
		return level;
	}


	
}
