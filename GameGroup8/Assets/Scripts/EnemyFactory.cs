using UnityEngine;
using System.Collections;

public class EnemyFactory {

	public EnemyFactory() {
	}


	public Enemy getEnemy(string type, string difficulty){
		if (type.Equals ("water")) {
			return getWaterEnemy (difficulty);
		} else if (type.Equals ("wind")) {
			return getWindEnemy (difficulty);
		} else {
			return getEarthEnemy (difficulty);
		}
	}

	private Enemy getWaterEnemy(string difficulty) {
		if(difficulty.Equals ("normal")){
			return new Enemy (1, 100, 10, 2f, new Type(3));
		}
		else if(difficulty.Equals ("harder")){
			return new Enemy (2, 200, 15, 1f, new Type(3));
		}
		else{
			return new Enemy (1, 100, 10, 2f, new Type(3));
		}
	}

	private Enemy getWindEnemy(string difficulty) {
		if(difficulty.Equals ("normal")){
			return new Enemy (1, 100, 10, 2f, new Type(1));
		}
		else if(difficulty.Equals ("harder")){
			return new Enemy (2, 200, 15, 1f, new Type(1));
		}
		else{
			return new Enemy (1, 100, 10, 2f, new Type(1));
		}
	}

	private Enemy getEarthEnemy(string difficulty) {
		if(difficulty.Equals ("normal")){
			return new Enemy (1, 100, 10, 2f, new Type(2));
		}
		else if(difficulty.Equals ("harder")){
			return new Enemy (2, 200, 15, 1f, new Type(2));
		}
		else{
			return new Enemy (1, 100, 10, 2f, new Type(2));
		}
	}
}
