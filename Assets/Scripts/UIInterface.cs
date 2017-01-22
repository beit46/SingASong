using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInterface : MonoBehaviour {
	public Text score;
	public Text multiplier;
	public Text lives;

	public void SetScore(float score) {
		//this.score.text = "SCORE: " + (int)score;
		this.score.text = "S\nC\nO\nR\nE\n" + VerticalizeString(score.ToString());
	}

	public void SetMultiplier(float multiplier) {
		this.multiplier.text = "MULTIPLIER: " + multiplier;
		this.multiplier.enabled = multiplier > 1;
	}

	public void SetLives(int lives) {
		//this.lives.text = "LIVES: " + lives;
		this.lives.text = "L\nI\nV\nE\nS\n" + VerticalizeString(lives.ToString());
	}

	string VerticalizeString(string s) {
		string vs = "";
		foreach (char c in s) {
			vs += "\n" + c;
		}
		return vs;
	}
}
