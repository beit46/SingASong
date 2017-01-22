using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public Text bestScore;
	public Text lastScore;
	public AudioProcessor audioProcessor;

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void StartGame(AudioProcessor.VolumeInput volume) {
		SceneManager.LoadScene ("Main");
	}
}
