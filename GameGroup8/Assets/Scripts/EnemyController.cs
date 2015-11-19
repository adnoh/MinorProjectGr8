using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Enemy enemy;
	private EnemyFactory enemyFactory;

	public double speed;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		enemy = enemyFactory.getEnemy ("normal");
		speed = enemy.getWalkingSpeed ();
	}




	
}
