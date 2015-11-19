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

	public Enemy getNormalEnemy() {
		return new Enemy (1, 100, 10, 5f);
	}

	public Enemy getBetterEnemy() {
		return new Enemy (2, 200, 15, 0.25f);
	}

}
