using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Text bestScore;
	public Text lastScore;
	public AudioProcessor audioProcessor;

	// Use this for initialization
	void Start () {
		audioProcessor.volumeInputSingle += StartGame ();
		audioProcessor.volumeInputContinued += StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void StartGame() {
		// Start Game
	}

	void setBestScore(int score) {
		bestScore.name = "Best Score: " + score;
	}

	void setLastScore(int score) {
		lastScore.name = "Last Score: " + score;
	}
}
