using UnityEngine;
using System.Collections;

public class EnemyFactory {

	public EnemyFactory() {
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

	private Enemy getNormalEnemy() {
		return new Enemy (1, 100, 10, 2f);
	}

	private Enemy getBetterEnemy() {
		return new Enemy (2, 200, 15, 1f);
	}

}
