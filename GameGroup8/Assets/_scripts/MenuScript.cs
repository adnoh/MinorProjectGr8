using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {
	
	public Button StartGame;
	public Button ExitGame;

	void start() {
		StartGame = StartGame.GetComponent<Button> ();
		ExitGame = ExitGame.GetComponent<Button> ();
	}

	public void StartPress() {
		Application.LoadLevel ("Game");
	}

	public void ExitPress() {
		Application.Quit ();
	}

}
