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

    IEnumerator MyMethod1()
    {
        yield return new WaitForSeconds(2);
        GameStateController.newgame = true;
        // Application.LoadLevel (1);
        SceneManager.LoadScene(1);
    }

    IEnumerator MyMethod2()
    {
        yield return new WaitForSeconds(2);
        GameStateController.newgame = false;
        SceneManager.LoadScene(1);
    }


    public void StartGame(){

        StartCoroutine(MyMethod1());
        //GameStateController.newgame = true;
		// Application.LoadLevel (1);
		//SceneManager.LoadScene(1);
	}

	public void LoadGame(){
        StartCoroutine(MyMethod2());
        //GameStateController.newgame = false;
        //SceneManager.LoadScene(1);
    }
	

}
