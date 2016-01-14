using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {

	public Canvas Main;
	public Canvas Options;
	
	void Start () {

		Options = Options.GetComponent<Canvas> ();
		Options.enabled = false;
	}

	void Update(){

		/// 
		if ( Input.GetKeyDown(KeyCode.K))
		{
			
		}
	
	}


    IEnumerator startTutorial()
    {
        yield return new WaitForSeconds(2);
        GameStateController.newgame = true;        
        SceneManager.LoadScene(3);
    }

    IEnumerator LoadGame_()
    {
        yield return new WaitForSeconds(2);
        GameStateController.newgame = false;
        SceneManager.LoadScene(1);
    }


    public void StartGame(){

		StartCoroutine(startTutorial());
        //GameStateController.newgame = true;
		// Application.LoadLevel (1);
		//SceneManager.LoadScene(1);
	}

	public void LoadGame(){
        StartCoroutine(LoadGame_());
    }
}
