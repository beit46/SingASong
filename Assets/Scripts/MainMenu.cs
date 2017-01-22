using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public Text bestScore;
	public Text lastScore;
	public AudioProcessor audioProcessor;

	public Sprite difficultyEasy;
	public Sprite difficultyHard;
	public Image difficultySelector;

	string _difficulty;

	// Use this for initialization
	void Start () {
		//audioProcessor.volumeInputSingle += StartGame;
		audioProcessor.volumeInputContinued += StartGame;

		if (PlayerPrefs.HasKey ("LastScore")) {
			bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
		}

		if (PlayerPrefs.HasKey ("BestScore")) {
			lastScore.text = PlayerPrefs.GetInt("LastScore").ToString();
		}

		_difficulty = "easy";
		if (PlayerPrefs.HasKey ("Track")) {
			_difficulty = PlayerPrefs.GetString ("Track");
		}
		if (_difficulty == "easy") {
			difficultySelector.sprite = difficultyEasy;
		}
		else {
			difficultySelector.sprite = difficultyHard;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void StartGame(AudioProcessor.VolumeInput volume) {
		SceneManager.LoadScene ("Main");
	}

	public void ToggleDifficulty() {
		if (_difficulty == "easy") {
			_difficulty = "hard";
			PlayerPrefs.SetString ("Track", "hard");
			PlayerPrefs.Save ();
			difficultySelector.sprite = difficultyHard;
		}
		else {
			_difficulty = "easy";
			PlayerPrefs.SetString ("Track", "easy");
			PlayerPrefs.Save ();
			difficultySelector.sprite = difficultyEasy;
		}
	}
}
