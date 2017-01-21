using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainReferences : MonoBehaviour {

	private static AudioPlayer audioPlayer;
	private static ColorGenerator colorGenerator;
	private static UIInterface uiInterface;

	public static AudioPlayer AudioPlayer {
		get {
			if (audioPlayer == null)
				return audioPlayer = GameObject.Find("AudioPlayer").GetComponent<AudioPlayer>();
			return audioPlayer;
		}
		private set { audioPlayer = value; }
	}

	public static ColorGenerator ColorGenerator {
		get {
			if (colorGenerator == null)
				return colorGenerator = GameObject.Find("ColorGenerator").GetComponent<ColorGenerator>();
			return colorGenerator;
		}
		private set { colorGenerator = value; }
	}

	public static UIInterface UIInterface {
		get {
			if (uiInterface == null)
				return uiInterface = GameObject.Find("Canvas").GetComponent<UIInterface>();
			return uiInterface;
		}
		private set { uiInterface = value; }
	}
}
