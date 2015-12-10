using UnityEngine;
using System.Collections;

public class Pausemenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape))
			loadpause ();
	
	}

	void loadpause(){
		Application.LoadLevel(2);
	}

	public void loadoutside(){
		Application.LoadLevel(1);
	}

}
