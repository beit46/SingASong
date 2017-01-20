using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainReferences : MonoBehaviour {
	private static AudioPlayer audioPlayer;

	public static AudioPlayer AudioPlayer {
		get {
			if (audioPlayer == null)
				return audioPlayer = GameObject.Find("AudioPlayer").GetComponent<AudioPlayer>();
			return audioPlayer;
		}
		private set { audioPlayer = value; }
	}

}
