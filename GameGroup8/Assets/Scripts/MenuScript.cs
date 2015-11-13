using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Canvas OptionsScreen;
	public Button StartGame;
	public Button ExitGame;
	public Button OpenOptions;


	void start() {
		StartGame = StartGame.GetComponent<Button> ();
		ExitGame = ExitGame.GetComponent<Button> ();
		OptionsScreen = OptionsScreen.GetComponent<Canvas> ();
		OptionsScreen.enabled = false;
	}

	public void StartPress() {
		Application.LoadLevel ("Game");
	}

	public void ExitPress() {
		Application.Quit ();
	}

	public void OptionsPress() {
		OptionsScreen.enabled = true;
		StartGame.enabled = false;
		ExitGame.enabled = false;
	}


}
