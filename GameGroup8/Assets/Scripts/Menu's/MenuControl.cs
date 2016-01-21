using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

/* This class contains all menu function for both: 
   Main Menu and PauseMenu 
*/
public class MenuControl : MonoBehaviour
{
    public Button LoadGameBtn;

    public void StartGame()
    {
        GameStateController.newgame = true;
        SceneManager.LoadScene(3);
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGame_());
    }

    IEnumerator LoadGame_()
    {
        yield return new WaitForSeconds(2);
        GameStateController.newgame = false;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (File.Exists(Application.dataPath + "/saves/world.xml"))
            {
                LoadGameBtn.GetComponent<Button>().interactable = true;
            }
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        { 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
            // Save player position
            MonsterCollection.playerSave(Application.dataPath + "/saves/Player.xml");

            // Save enemies
            MonsterCollection.MonsterSave(Application.dataPath + "/saves/monsters.xml");

            // Save outside variables + base
            MonsterCollection.outsideSave(Application.dataPath + "/saves/outside.xml");
            MonsterCollection.turretSave(Application.dataPath + "/saves/turrets.xml");
            MonsterCollection.BaseSave(Application.dataPath + "/saves/base.xml");

            // Moon and sun position
            MonsterCollection.SunSave(Application.dataPath + "/saves/sun.xml");
            MonsterCollection.MoonSave(Application.dataPath + "/saves/moon.xml");

            // Save World (if you want to save the world, go ahead, be a hero)
            MonsterCollection.MapSave(Application.dataPath + "/saves/world.xml");

            MiniMapScript.clearEnemies();

            loadpause();
            }
        }
    }

    /// <summary>
    /// Load Pause
    /// </summary>
    public void loadpause()
    {
        setNGame(false);
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Load Main Menu
    /// </summary>
	public void loadmenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Load outside (the real level)
    /// </summary>
	public void loadoutside()
    {
        setNGame(false);
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Change NewGame parameter
    /// </summary>
    /// <param name="newgame_"></param>
    public void setNGame(bool newgame_)
    {
        GameStateController.setNewgame(newgame_);
    }
}

