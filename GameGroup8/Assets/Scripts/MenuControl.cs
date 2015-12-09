using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuControl : MonoBehaviour {

	public Canvas Main;
	public Canvas Options;



	void Start () {

		Options = Options.GetComponent<Canvas> ();
	
		Options.enabled = false;

	}


	

}
