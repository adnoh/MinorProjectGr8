using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Canvas AreYouSureMenu;

	public void newGame(){

	}

	public void ExitPress(){
		AreYouSureMenu.enabled = true;
	}

	public void ExitGame(){
		Debug.Log (true);
		Application.Quit ();
	}

	public void NoPress(){
		AreYouSureMenu.enabled = false;
	}

}
