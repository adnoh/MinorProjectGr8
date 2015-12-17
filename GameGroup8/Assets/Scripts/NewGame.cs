using UnityEngine;
using System.Collections;

public class NewGame : MonoBehaviour {

    public void setNGame(bool newgame_)
    {
        GameStateController.setNewgame(newgame_);
    }
}
