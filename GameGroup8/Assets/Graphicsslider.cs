using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Graphicsslider : MonoBehaviour {

	void Start() {
		var slider = gameObject.GetComponent <Slider>();
		var temp = PlayerPrefs.GetInt ("grahpics option");
		var temp1 = (float)temp;
		slider.value = temp1;
		QualitySettings.SetQualityLevel(temp, true);

	}
	// Update is called once per frame
	void Update () {

		 var slider = gameObject.GetComponent <Slider>();
		var text = GetComponentInChildren<Text> ();

	

		int caseSwitch = Mathf.CeilToInt(slider.value);
		switch (caseSwitch)
		{
		case 0:
			QualitySettings.SetQualityLevel(0, true);
			text.text=  "Fastest";
			break;
		case 1:
			QualitySettings.SetQualityLevel(1, true);
			text.text=  "Fast";
			break;
		case 2:
			QualitySettings.SetQualityLevel(2, true);
			text.text=  "Simple";
			break;
		case 3:
			QualitySettings.SetQualityLevel (3, true);
			text.text=  "Good";
			break;
		case 4:
			QualitySettings.SetQualityLevel(4, true);
			text.text=  "Beautiful";
			break;
		case 5:
			QualitySettings.SetQualityLevel(5, true);
			text.text=  "Fantastic";
			break;
		default:
			QualitySettings.SetQualityLevel(0, true);
			text.text=  "you broke the system......";
			break;


		}
		PlayerPrefs.SetInt("grahpics option", Mathf.CeilToInt(slider.value));
		PlayerPrefs.Save();
	}
}
