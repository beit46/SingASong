using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public Text bestScore;
	public Text lastScore;
	int _bestScore;
	public AudioProcessor audioProcessor;

	// Use this for initialization
	void Start () {
<<<<<<< HEAD
		audioProcessor.volumeInputSingle += StartGame;
		audioProcessor.volumeInputContinued += StartGame;
		_bestScore = 0;
=======
//		audioProcessor.volumeInputSingle += StartGame;
//		audioProcessor.volumeInputContinued += StartGame;
>>>>>>> bfc2ddd8c8e2717e49c41f3a7da291f05c998358
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void StartGame() {
		SceneManager.LoadScene ("Main");
	}

	void setBestScore(int score) {
		if (score > _bestScore) {
			bestScore.name = "Best Score: " + score;
			_bestScore = score;
		}
	}

	void setLastScore(int score) {
		lastScore.name = "Last Score: " + score;
	}
}
