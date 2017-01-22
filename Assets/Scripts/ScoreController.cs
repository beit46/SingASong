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

	private float numberOfHitToIncreaseMultiplier = 2f;
	private int successfullConsecutiveHits = 0;

	public Live live;

	void Awake() {
		MainReferences.UIInterface.SetScore(score);
		MainReferences.UIInterface.SetMultiplier(multiplier);
		live.OnDead += GoToMainMenu;
	}

	void GoToMainMenu() {
		PlayerPrefs.SetInt ("LastScore", (int) score);
		if (!PlayerPrefs.HasKey ("BestScore") || (int) score > PlayerPrefs.GetInt("BestScore")) {
			PlayerPrefs.SetInt ("BestScore", (int) score);
		}
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("MainMenu");
	}
	
	void CalculateScore() {
		if (this.lastShotWasAHit) {
			this.consecutiveHitSuccesfull++;
//			Debug.Log("Consecutive hit succesfull " + this.consecutiveHitSuccesfull);
			if (this.consecutiveHitSuccesfull >= this.numberOfHitToIncreaseMultiplier) {
				//Increase multipilier
				this.multiplier += 0.1f;
				this.numberOfHitToIncreaseMultiplier *= 1.5f;
				this.consecutiveHitSuccesfull = 0;
//				Debug.Log("Multiplier " + this.multiplier);
			}
			this.score += scoreStep * multiplier;
		} else {
//			Debug.Log("Reset multiplier");
			this.multiplier = 1.0f;
			this.numberOfHitToIncreaseMultiplier = 2f;
			this.consecutiveHitSuccesfull = 0;
		}
			
		MainReferences.UIInterface.SetScore(score);
		MainReferences.UIInterface.SetMultiplier(multiplier);	
	}

	public void Hit() {
//		Debug.Log("Hit");
		this.lastShotWasAHit = true;
		CalculateScore();
	}

	public void Miss() {
		this.lastShotWasAHit = false;
		CalculateScore();
	}
}
