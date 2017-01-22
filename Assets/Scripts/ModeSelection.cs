using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;

public class ModeSelection : MonoBehaviour {
	public SimpleMusicPlayer simpleMusicPlayer;
	public Koreography BPM_120_Koreography;
	public Koreography BPM_150_Koreography;
	public TargetSpawner targetSpawner;

	// Use this for initialization
	void Awake () {
		bool isEasyModeEnabled = true;
		if(isEasyModeEnabled) {
			this.simpleMusicPlayer.LoadSong(BPM_120_Koreography);
			targetSpawner.targetSpeed = 6.0f;
		}
		else {
			this.simpleMusicPlayer.LoadSong(BPM_150_Koreography);
			targetSpawner.targetSpeed = 8.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
