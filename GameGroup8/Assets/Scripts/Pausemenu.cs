﻿using UnityEngine;
using System.Collections;

public class Pausemenu : MonoBehaviour {
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)){
            MonsterCollection.MonsterSave("Assets/saves/monsters.xml");
            MonsterCollection.turretSave("Assets/saves/turrets.xml");
            MonsterCollection.outsideSave("Assets/saves/outside.xml");
			MonsterCollection.playerSave("Assets/saves/Player.xml");
            MiniMapScript.clearEnemies();
            // GameStateController.newgame = false;
            loadpause();
        }
	}

	void loadpause(){
		Application.LoadLevel(2);
	}

	public void loadoutside(){

        
		Application.LoadLevel(1);

        // kan alleen als gamestate controller newgame static is
        // GameStateController.newgame = false;
    }

}