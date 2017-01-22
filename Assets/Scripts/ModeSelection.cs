﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;

public class ModeSelection : MonoBehaviour {
	public SimpleMusicPlayer simpleMusicPlayer;
	public Koreography BPM_120_Koreography;
	public Koreography BPM_150_Koreography;
	public TargetSpawner targetSpawner;

	public Transform backgrounds;
	public Sprite backGroundEasyNorm;
	public Sprite backGroundEasyCroce;
	public Sprite backGroundHardNorm;
	public Sprite backGroundHardCroce;

	// Use this for initialization
	void Awake () {
		bool isEasyModeEnabled = false;
		if(isEasyModeEnabled) {
			this.simpleMusicPlayer.LoadSong(BPM_120_Koreography);
			targetSpawner.targetSpeed = 5.0f;
			int i = 0;
			foreach (Transform t in backgrounds) {
				if (i % 2 == 0) {
					t.GetComponent<SpriteRenderer> ().sprite = backGroundEasyNorm;
				} else {
					t.GetComponent<SpriteRenderer> ().sprite = backGroundEasyCroce;
				}
				i++;
			}
		}
		else {
			this.simpleMusicPlayer.LoadSong(BPM_150_Koreography);
			targetSpawner.targetSpeed = 5.0f;
			int i = 0;
			foreach (Transform t in backgrounds) {
				if (i % 2 == 0) {
					t.GetComponent<SpriteRenderer> ().sprite = backGroundHardNorm;
				} else {
					t.GetComponent<SpriteRenderer> ().sprite = backGroundHardCroce;
				}
				i++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
