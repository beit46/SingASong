using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour {
	private float score = 0;
	private float scoreStep = 10f;
	private float multiplier = 1f;
	private int consecutiveHitSuccesfull = 0;
	private bool lastShotWasAHit = false;

	private float numberOfHitToIncreaseMultiplier = 2;
	private int successfullConsecutiveHits = 0;

	public Live live;

	void Awake() {
		MainReferences.UIInterface.SetScore(score);
		MainReferences.UIInterface.SetMultiplier(multiplier);
		live.OnDead += GoToMainMenu;
	}

	void GoToMainMenu() {
		PlayerPrefs.SetInt ("LastScore", (int) score);
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("MainMenu");
	}
	
	void CalculateScore() {

		if (this.lastShotWasAHit) {
			this.consecutiveHitSuccesfull++;
			if (this.consecutiveHitSuccesfull >= this.numberOfHitToIncreaseMultiplier) {
				//Increase multipilier
				this.multiplier += 0.1f;
				this.numberOfHitToIncreaseMultiplier *= 1.5f;
				this.consecutiveHitSuccesfull = 0;
			}
			this.score += scoreStep * multiplier;
		} else {
			this.multiplier = 1.0f;
			this.consecutiveHitSuccesfull = 0;
		}



		MainReferences.UIInterface.SetScore(score);
		MainReferences.UIInterface.SetMultiplier(multiplier);	
	}

	public void Hit() {
		this.lastShotWasAHit = true;
		CalculateScore();
	}

	public void Miss() {
		this.lastShotWasAHit = false;
		CalculateScore();
	}
}
