using UnityEngine;
using System.Collections;

public class Pausemenu : MonoBehaviour {

    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)){
            // Save player position
            MonsterCollection.playerSave("Assets/saves/Player.xml");

            // Save enemies
            MonsterCollection.MonsterSave("Assets/saves/monsters.xml");

            // Save outside variables + base
            MonsterCollection.outsideSave("Assets/saves/outside.xml");
            MonsterCollection.turretSave("Assets/saves/turrets.xml");
            MonsterCollection.BaseSave("Assets/saves/base.xml");

            // Save World (if you want to save the world, go ahead, be a hero)
            MonsterCollection.MapSave("Assets/saves/world.xml");

            MiniMapScript.clearEnemies();
            
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
