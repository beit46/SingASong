using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class KoreopgrapherController : MonoBehaviour {
	public delegate void Beat();
	public Beat OnBeat;

	// Use this for initialization
	void Start () {
		Koreographer.Instance.RegisterForEvents( "NewKoreographyTrack" , FireEventDebugLog);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void  FireEventDebugLog( KoreographyEvent  koreoEvent) {
		Debug .Log( "Koreography Event Fired." ); 
		NotifyReceivedBeat();
	}

	void NotifyReceivedBeat() {
		if(this.OnBeat != null)
			this.OnBeat();
	}
}
