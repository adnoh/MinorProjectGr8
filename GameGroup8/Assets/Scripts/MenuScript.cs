using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {
	
	public Button StartGame;
	public Button ExitGame;
	public Button OpenOptions;


	void start() {
		StartGame = StartGame.GetComponent<Button> ();
		ExitGame = ExitGame.GetComponent<Button> ();
	}

	public void StartPress() {
		Application.LoadLevel ("Outside");
	}

	public void ExitPress() {
		Application.Quit ();
	}

}
