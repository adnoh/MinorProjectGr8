using UnityEngine;
using System.Collections;

public class EnemyFactory {

	public EnemyFactory() {
	}
	
	public Enemy getEnemy(string name, int level){
		if (name.Equals ("Hammerhead")) {
			return getHammerhead(level);
		} else if (name.Equals ("DesertEagle")) {
			return getDesertEagle(level);
		} else if(name.Equals("FireFox")){
			return getFireFox(level);
		}
        else
        {
            return getHammerhead(level);
        }
	}

	private Enemy getHammerhead(int level) {
		if (level == 1) {
			return new Enemy (1, 100, 10, 2f, new Type (3));
		} else if (level == 2) {
			return new Enemy (2, 200, 15, 2f, new Type (3));
		} else if (level == 3) {
			return new Enemy (3, 250, 20, 2f, new Type (3));
		} else if (level == 4) {
			return new Enemy (4, 300, 22, 2f, new Type (3));
		} else if (level == 5) {
			return new Enemy (5, 400, 25, 2f, new Type (3));
		} else {
			return new Enemy (1, 100, 10, 2f, new Type (3));
		}
	}

	private Enemy getDesertEagle(int level) {
		if (level == 1) {
			return new Enemy (1, 75, 7, 4f, new Type (1));
		} else if (level == 2) {
			return new Enemy (2, 125, 9, 4f, new Type (1));
		} else if (level == 3) {
			return new Enemy (3, 175, 14, 4f, new Type (1));
		} else if (level == 4) {
			return new Enemy (4, 250, 17, 4f, new Type (1));
		} else if (level == 5) {
			return new Enemy (5, 300, 20, 4f, new Type (1));
		} else {
			return new Enemy (1, 75, 7, 4f, new Type (1));
		}
	}

	private Enemy getFireFox(int level) {
		if (level == 1) {
			return new Enemy (1, 25, 30, 6f, new Type (2));
		} else if (level == 2) {
			return new Enemy (2, 45, 45, 6f, new Type (2));
		} else if (level == 3) {
			return new Enemy (3, 50, 60, 6f, new Type (2));
		} else if (level == 4) {
			return new Enemy (4, 65, 75, 6f, new Type (2));
		} else if (level == 5) {
			return new Enemy (5, 80, 85, 6f, new Type (2));
		} else {
			return new Enemy (1, 25, 30, 6f, new Type (2));
		}
	}
}
