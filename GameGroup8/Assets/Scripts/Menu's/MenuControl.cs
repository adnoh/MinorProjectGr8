using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* This class contains all menu function for both: 
   Main Menu and PauseMenu 
*/
public class MenuControl : MonoBehaviour
{
    public Button Pause_SaveGameToCloud;

    void Start()
    {
        if (SaveBase.loggedIn)
        {
            Pause_SaveGameToCloud.enabled = true;
        }
    }


    public void StartGame()
    {
        StartCoroutine(startTutorial());
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

    IEnumerator startTutorial()
    {
        yield return new WaitForSeconds(2);
        GameStateController.newgame = true;
        SceneManager.LoadScene(3);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        
        if (Application.loadedLevel == 1)
        { 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
            // Save player position
            MonsterCollection.playerSave("Assets/saves/Player.xml");

            // Save enemies
            MonsterCollection.MonsterSave("Assets/saves/monsters.xml");

            // Save outside variables + base
            MonsterCollection.outsideSave("Assets/saves/outside.xml");
            MonsterCollection.turretSave("Assets/saves/turrets.xml");
            MonsterCollection.BaseSave("Assets/saves/base.xml");

            // Moon and sun position
            MonsterCollection.SunSave("Assets/saves/sun.xml");
            MonsterCollection.MoonSave("Assets/saves/moon.xml");

            // Save World (if you want to save the world, go ahead, be a hero)
            MonsterCollection.MapSave("Assets/saves/world.xml");

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
        Time.timeScale = 0;
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Load Main Menu
    /// </summary>
	public void loadmenu()
    {
        Time.timeScale = 0;
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

