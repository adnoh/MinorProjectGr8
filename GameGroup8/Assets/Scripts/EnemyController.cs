using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Enemy enemy;
	private EnemyFactory enemyFactory;
	private Vector3 position;
	public float speed;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		//enemy = enemyFactory.getEnemy ("normal");
		//speed = enemy.getWalkingSpeed ();

		Debug.Log (position);
	}
	void FixedUpdate ()
		
		
	{
		position = Controls.getposition ();
		transform.position = Vector3.MoveTowards(transform.position, position,   speed*Time.deltaTime);

	}


	
}
