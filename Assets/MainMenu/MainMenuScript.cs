using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	public Dropdown resolutionDropdown;
	public Dropdown graphicsDropdown;
	public Toggle fullscreenToggle;
	public Slider volumeSlider;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("CurrentLevel") == 0)
			PlayerPrefs.SetInt ("CurrentLevel", 1);


		resolutionDropdown.ClearOptions ();
		var resolutions = Screen.resolutions.Select (i => string.Format ("{0} X {1} @{2}Hz", i.width, i.height, i.refreshRate)).ToList ();
		resolutionDropdown.AddOptions (resolutions);
		resolutionDropdown.value = resolutions.FindIndex (i => i == string.Format ("{0} X {1} @{2}Hz", Screen.currentResolution.width, Screen.currentResolution.height, Screen.currentResolution.refreshRate));

		graphicsDropdown.ClearOptions ();
		graphicsDropdown.AddOptions (QualitySettings.names.ToList());
		graphicsDropdown.value = QualitySettings.GetQualityLevel ();

		fullscreenToggle.isOn = Screen.fullScreen;

		volumeSlider.value = AudioListener.volume;

	}
	
	public void ExitGame () {
		Application.Quit ();
	}
	public void BackToMainMenu () {
		SceneManager.LoadScene ("Main Menu");
	}
	public void Continue () {
		SceneManager.LoadScene ("Loading Scene");
	}
	public void NewGame () {
		PlayerPrefs.SetInt ("CurrentLevel", 1);
		SceneManager.LoadScene ("Loading Scene");
	}


	public void OptionsSave(){
		Screen.SetResolution (Screen.resolutions [resolutionDropdown.value].width, Screen.resolutions [resolutionDropdown.value].height, fullscreenToggle.isOn, Screen.resolutions[resolutionDropdown.value].refreshRate);
		QualitySettings.SetQualityLevel (graphicsDropdown.value);
		AudioListener.volume = volumeSlider.value;
	}
}
