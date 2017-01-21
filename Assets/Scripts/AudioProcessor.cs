using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioProcessor : MonoBehaviour {

	class Volume {
		public const float NONE = 0.0f;
		public const float LOW = 0.02f;
		public const float MIDDLE = 0.05f;
		public const float HIGH = 0.08f;
		public const float FLOW = -1f;
	}

	const int THRESHOLD_SINGLE = 7;

	AudioInput _audioInput;

	float _currentVolume; // = Volume.NONE;
	int _sequenceCount; // = 0;
	float _toTrigger; // = Volume.NONE;

	void Start() {
		_audioInput = GetComponent<AudioInput> ();
		_currentVolume = Volume.NONE;
		_sequenceCount = 0;
		_toTrigger = Volume.NONE;
	}

	void Update() {
		UpdateVolumeState (_audioInput.lastInput);
	}

	float GetVolumeCardinal(float volume) {
		if (volume >= Volume.HIGH) {
			return Volume.HIGH;
		} 
		else if (volume >= Volume.MIDDLE) {
			return Volume.MIDDLE;
		}
		else if (volume >= Volume.LOW) {
			return Volume.LOW;
		}
		return Volume.NONE;
	}

	void UpdateVolumeState(float volume) {
		float newVolume = GetVolumeCardinal(volume);
		if (newVolume == Volume.NONE) {
			if (_toTrigger != Volume.NONE && _toTrigger != Volume.FLOW) {
				TriggerSingle (_toTrigger);
			}
			if (_toTrigger != Volume.NONE) {
				_toTrigger = Volume.NONE;
			}
		}
		else if (newVolume < _currentVolume) {
			if (_toTrigger == Volume.NONE) {
				_toTrigger = _currentVolume;
			}
			else if (_toTrigger == Volume.FLOW) {
				_sequenceCount = 0;
			}
		}
		else if (newVolume == _currentVolume) {
			if (_sequenceCount > THRESHOLD_SINGLE) {
				_toTrigger = Volume.FLOW;
				TriggerFlow (_currentVolume);
			}
			else {
				_sequenceCount++;
			}
		}
		else if (newVolume > _currentVolume) {
			_sequenceCount = 0;
		}
		_currentVolume = volume;
	}

	void TriggerSingle(float volume) {
		Debug.Log ("Single => " + GetVolumeText(volume));
	}

	void TriggerFlow(float volume) {
		Debug.Log ("Flow => " + GetVolumeText (volume));
	}

	string GetVolumeText(float volume) {
		if (volume >= Volume.HIGH) {
			return "HIGH";
		}
		else if (volume >= Volume.MIDDLE) {
			return "MIDDLE";
		}
		else if (volume >= Volume.LOW) {
			return "LOW";
		}
		return "NONE";
	}
}
