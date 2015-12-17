using UnityEngine;
using System.Collections;

public class NewGame : MonoBehaviour {

    // NewGame or not? setter
    public void setNGame(bool newgame_)
    {
        GameStateController.setNewgame(newgame_);
    }

    // NewGamge
    void New_Game()
    {
        setNGame(true);
        loadoutside();

    }

    // LoadGame
    void LoadGame()
    {
        setNGame(false);
        loadoutside();
    }

    public void loadoutside()
    {
        Application.LoadLevel(1);
    }
 
}
