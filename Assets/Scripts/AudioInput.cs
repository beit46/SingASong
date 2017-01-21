using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInput : MonoBehaviour {

	delegate void AudioInputReceived(float volume);
	AudioInputReceived audioInputReceived;

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
		audioInputReceived += TestLogVolume;
		lastInput = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		float volume = GetAveragedVolume ();
		//if (volume >= 0.02f)
			lastInput = volume;
//		else
//			lastInput = 0f;
//		volume = GetVolumeCardinal (volume);
//		if (volume > Volume.ZERO) {
//			audioInputReceived (volume);
//		}
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

	float GetVolumeCardinal(float volume) {
		if (volume >= Volume.HIGH) {
			return Volume.HIGH;
		} else if (volume >= Volume.MEDIUM) {
			return Volume.MEDIUM;
		} else if (volume >= Volume.LOW) {
			return Volume.LOW;
		} else if (volume >= Volume.TEST) {
			return Volume.TEST;
		}
		return Volume.ZERO;
	}

	void TestLogVolume(float volume) {
		Debug.Log (volume);
	}
}
