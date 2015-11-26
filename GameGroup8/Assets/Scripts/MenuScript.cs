using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Button startGame;
	public Button exitGame;

	public Slider slider;

	public InputField Name;

	void start() {

		startGame = startGame.GetComponent<Button> ();
		exitGame = exitGame.GetComponent<Button> ();

		slider = slider.GetComponent<Slider> ();

		if (!PlayerPrefs.HasKey ("Volume")) {
			PlayerPrefs.SetFloat ("Volume", 1);
		}

		slider.value = PlayerPrefs.GetFloat ("Volume");

		AudioListener.volume = PlayerPrefs.GetFloat ("Volume");

		Name = Name.GetComponent<InputField> ();

		if (!PlayerPrefs.HasKey ("PlayerName")) {
			PlayerPrefs.SetString ("PlayerName", "");
		}

		Name.text = PlayerPrefs.GetString ("PlayerName");

	}

	public void StartPress() {
		Application.LoadLevel ("Outside");
	}

	public void ExitPress() {
		Application.Quit ();
	}

	public void setSound() {
		AudioListener.volume = slider.normalizedValue;
		PlayerPrefs.SetFloat ("Volume", slider.normalizedValue);
	}

	public void changeName() {
		PlayerPrefs.SetString ("PlayerName", Name.text);
	}


}
