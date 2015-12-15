using UnityEngine;
using System.Collections;

public class Pausemenu : MonoBehaviour {
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)){
            MonsterCollection.MonsterSave("Assets/saves/monsters.xml");
            MiniMapScript.clearEnemies();
            loadpause();
        }
	}

	void loadpause(){
		Application.LoadLevel(2);
	}

	public void loadoutside(){
		Application.LoadLevel(1);
        MonsterCollection.MonsterLoad("Assets/saves/monsters.xml");
	}

}
