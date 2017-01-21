using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class KoreopgrapherController : MonoBehaviour {
	public delegate void Beat();
	public Beat OnBeat;

	private AudioSource audioPlayer;

	// Use this for initialization
	void Start () {
		this.audioPlayer = GetComponent<AudioSource>();
		Koreographer.Instance.RegisterForEvents( "NewKoreographyTrack" , FireEventDebugLog);
	}

	void  FireEventDebugLog( KoreographyEvent  koreoEvent) {
//		Debug .Log( "Koreography Event Fired." ); 
		NotifyReceivedBeat();
	}

	void NotifyReceivedBeat() {
		if(this.OnBeat != null)
			this.OnBeat();
	}

	public void DecreaseVolumeForDuration(float duration) {
		this.audioPlayer.volume = 0.6f;
		Invoke("RestoreVolume", duration);
	}

	void RestoreVolume() {
		this.audioPlayer.volume = 1.0f;
	}
}
