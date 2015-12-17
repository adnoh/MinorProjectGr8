using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Graphicsslider : MonoBehaviour {


	// Update is called once per frame
	void Update () {

		 var slider = gameObject.GetComponent <Slider>();
		var text = GetComponentInChildren<Text> ();

	

		int caseSwitch = Mathf.CeilToInt(slider.value);
		switch (caseSwitch)
		{
		case 0:
			QualitySettings.currentLevel = QualityLevel.Fastest;
			text.text=  "Fastest";
			break;
		case 1:
			QualitySettings.currentLevel = QualityLevel.Fast;
			text.text=  "Fast";
			break;
		case 2:
			QualitySettings.currentLevel = QualityLevel.Simple;
			text.text=  "Simple";
			break;
		case 3:
			QualitySettings.currentLevel = QualityLevel.Good;
			text.text=  "Good";
			break;
		case 4:
			QualitySettings.currentLevel = QualityLevel.Beautiful;
			text.text=  "Beautiful";
			break;
		case 5:
			QualitySettings.currentLevel = QualityLevel.Fantastic;
			text.text=  "Fantastic";
			break;
		default:
			QualitySettings.currentLevel = QualityLevel.Fast;
			text.text=  "you broke the system......";
			break;
		}
	}
}
