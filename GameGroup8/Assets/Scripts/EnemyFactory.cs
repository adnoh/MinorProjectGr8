using UnityEngine;
using System.Collections;

public class EnemyFactory {

	private EnemyFactory() {
	}


	public Enemy getEnemy(string name){
		if (name.Equals ("normal")) {
			return getNormalEnemy ();
		} else if (name.Equals ("better")) {
			return getBetterEnemy ();
		} else {
			return getNormalEnemy ();
		}
	}

	public Enemy getNormalEnemy() {
		return new Enemy (1, 100, 10, 0.5);
	}

	public Enemy getBetterEnemy() {
		return new Enemy (2, 200, 15, 0.25);
	}

}
