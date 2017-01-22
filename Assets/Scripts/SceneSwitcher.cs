using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

	public void GoToTutorial() {
		SceneManager.LoadScene ("Tutorial");
	}

	public void GoToMainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}

	public void GoToCredits () {
		SceneManager.LoadScene ("Credits");
	}
}
