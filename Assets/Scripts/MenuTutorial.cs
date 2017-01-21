using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTutorial : MonoBehaviour {

	AudioProcessor _audioProcessor;

	int _checkSingleLow;
	int _checkSingleHigh;
	int _checkContinuedLow;
	int _checkContinuedHigh;

	// Use this for initialization
	void Start () {
		_checkSingleLow = 0;
		_checkSingleHigh = 0;
		_checkContinuedLow = 0;
		_checkContinuedHigh = 0;
		_audioProcessor = GetComponent<AudioProcessor> ();
		_audioProcessor.volumeInputSingle += CheckSingle;
		_audioProcessor.volumeInputContinued += CheckContinued;
	}
	
	// Update is called once per frame
	void Update () {
		if (TutorialDone ()) {
			// Goto MainMenu
		}
	}

	void CheckSingle(AudioProcessor.VolumeInput volume) {
		if (volume == AudioProcessor.VolumeInput.LOW) {
			_checkSingleLow++;
		}
		else if (volume == AudioProcessor.VolumeInput.HIGH) {
			_checkSingleHigh++;
		}
	}

	void CheckContinued(AudioProcessor.VolumeInput volume) {
		if (volume == AudioProcessor.VolumeInput.LOW) {
			_checkContinuedLow++;
		}
		else if (volume == AudioProcessor.VolumeInput.HIGH) {
			_checkContinuedHigh++;
		}
	}

	bool TutorialDone() {
		return _checkSingleLow > 1 && _checkSingleHigh > 1 &&
		_checkContinuedLow > 5 && _checkContinuedHigh > 5;
	}
}
