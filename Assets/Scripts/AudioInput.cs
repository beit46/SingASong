using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInput : MonoBehaviour {

	AudioSource _audio;
	public float lastInput;

	// Use this for initialization
	void Start () {
		_audio = GetComponent<AudioSource>();
		_audio.clip = Microphone.Start(null, true, 5, 44100);
		_audio.loop = true;
		//_audio.mute = true; Doesn't work, is bugged;
		//_audio.volume = 0.0000001f;
		while (!(Microphone.GetPosition(null) > 0)){}
		_audio.Play();
		lastInput = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		lastInput = GetAveragedVolume ();
//		if (lastInput > 0)
//			Debug.Log (lastInput);
	}

	void GetData() {
		float[] data = new float[256];
		_audio.GetOutputData (data, 0);
		foreach (float s in data) {
			Debug.Log (s);
		}
	}

	float GetAveragedVolume() { 
		float[] data = new float[256];
		float average = 0;
		_audio.GetOutputData(data, 0);
		foreach(float s in data) {
			average += Mathf.Abs(s);
		}
		return average / 256;
	}
}
