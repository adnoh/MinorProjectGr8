using UnityEngine;
using System.Collections;

public class EnemyFactory {

	public EnemyFactory() {
	}


	public Enemy getEnemy(string type, int level){
		if (type.Equals ("water")) {
			return getWaterEnemy (level);
		} else if (type.Equals ("wind")) {
			return getWindEnemy (level);
		} else {
			return getEarthEnemy (level);
		}
	}

	private Enemy getWaterEnemy(int level) {
		if (level == 1) {
			return new Enemy (1, 100, 10, 2f, new Type (3));
		} else if (level == 2) {
			return new Enemy (2, 200, 15, 1f, new Type (3));
		} else if (level == 3) {
			return new Enemy (3, 250, 22, 0.8f, new Type (3));
		} else if (level == 4) {
			return new Enemy (4, 300, 25, 1.2f, new Type (3));
		} else if (level == 5) {
			return new Enemy (5, 400, 15, 3f, new Type (3));
		} else {
			return new Enemy (1, 100, 10, 2f, new Type (3));
		}
	}

	private Enemy getWindEnemy(int level) {
		if (level == 1) {
			return new Enemy (1, 100, 10, 2f, new Type (1));
		} else if (level == 2) {
			return new Enemy (2, 200, 15, 1f, new Type (1));
		} else if (level == 3) {
			return new Enemy (3, 250, 22, 0.8f, new Type (1));
		} else if (level == 4) {
			return new Enemy (4, 300, 25, 1.2f, new Type (1));
		} else if (level == 5) {
			return new Enemy (5, 400, 15, 3f, new Type (1));
		} else {
			return new Enemy (1, 100, 10, 2f, new Type (1));
		}
	}

	private Enemy getEarthEnemy(int level) {
		if (level == 1) {
			return new Enemy (1, 100, 10, 2f, new Type (2));
		} else if (level == 2) {
			return new Enemy (2, 200, 15, 1f, new Type (2));
		} else if (level == 3) {
			return new Enemy (3, 250, 22, 0.8f, new Type (2));
		} else if (level == 4) {
			return new Enemy (4, 300, 25, 1.2f, new Type (2));
		} else if (level == 5) {
			return new Enemy (5, 400, 15, 3f, new Type (2));
		} else {
			return new Enemy (1, 100, 10, 2f, new Type (2));
		}
	}
}
