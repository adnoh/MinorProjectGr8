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

	public void StartGame(){
		GameStateController.newgame = true;
		// Application.LoadLevel (1);
		SceneManager.LoadScene(1);
	}

	public void LoadGame(){
		GameStateController.newgame = false;
		SceneManager.LoadScene(1);
	}
	

}
