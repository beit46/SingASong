using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInput : MonoBehaviour {

	AudioSource _audio;
	public float lastInput;

	public class Volume {
		public const float ZERO   = 0.0f;
		public const float TEST   = 0.005f;
		public const float LOW    = 0.02f;
		public const float MEDIUM = 0.05f;
		public const float HIGH   = 0.08f;
	}

	// Use this for initialization
	void Start () {
		_audio = GetComponent<AudioSource>();
		_audio.clip = Microphone.Start(null, true, 5, 44100);
		_audio.loop = true;
		//_audio.mute = true;
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
