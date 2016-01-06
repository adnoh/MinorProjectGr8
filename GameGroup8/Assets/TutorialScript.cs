using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialScript : MonoBehaviour {

    public Text Tutorialtext;
    int count = 0;
    float starttime;

    void Start () {
        float starttime = Time.time;
        int count = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time - starttime > 50.0f) ;
            Tutorialtext.text = "press spacebar to continue";
        if (Input.GetKeyDown(KeyCode.Space)){
            count = count + 1;
        
        if (count == 1)
            {
                Tutorialtext.text = "hallo";
            }
        }

    }
}
