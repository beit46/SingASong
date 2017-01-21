using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioProcessor : MonoBehaviour {

	public delegate void VolumeInputSingle(VolumeInput value);
	public VolumeInputSingle volumeInputSingle;

	public delegate void VolumeInputContinued(VolumeInput value);
	public VolumeInputContinued volumeInputContinued;

	const float VOLUME_STEP = 0.05f;

	class Volume {
		public const float NONE = 0.0f;
		public const float LOW  = 0.02f;
		public const float HIGH = LOW + VOLUME_STEP;
	}

	public enum VolumeInput {
		NONE,
		LOW,
		HIGH
	}

	const int THRESHOLD_SINGLE = 10;
	const int THRESHOLD_ZERO = 3;

	AudioInput _audioInput;

	float _currentVolume;
	int _sequenceCount;
	bool _canSingle;
	bool _block;

	void Start() {
		_audioInput = GetComponent<AudioInput> ();
		_currentVolume = Volume.NONE;
		_sequenceCount = 0;
		_canSingle = true;
		_block = false;
		volumeInputSingle += TriggerSingle;
		volumeInputContinued += TriggerContinued;
	}

	void Update() {
		UpdateVolumeState (_audioInput.lastInput);
	}

	float GetVolumeCardinal(float volume) {
		if (volume >= Volume.HIGH) {
			return Volume.HIGH;
		}
		else if (volume >= Volume.LOW) {
			return Volume.LOW;
		}
		return Volume.NONE;
	}

	void UpdateVolumeState(float volume) {
		float newVolume = GetVolumeCardinal (volume);
		if (_block) {
			_sequenceCount++;
			if (_sequenceCount > THRESHOLD_ZERO) {
				_sequenceCount = 0;
				_block = false;
			}
		}
		if (!_block) {
			if (newVolume < _currentVolume) {
				if (_canSingle) {
					_canSingle = false;
					volumeInputSingle (GetVolumeInput (_currentVolume));
				}
				if (newVolume == Volume.NONE) {
					_block = true;
					_canSingle = true;
				}
				_sequenceCount = 0;
			}
			else if (newVolume == _currentVolume) {
				if (_sequenceCount > THRESHOLD_SINGLE && newVolume != Volume.NONE) {
					volumeInputContinued (GetVolumeInput (_currentVolume));
					_canSingle = false;
				}
				else {
					_sequenceCount++;
				}
			}
			else if (newVolume > _currentVolume) {
				_sequenceCount = 0;
			}
			_currentVolume = newVolume;
		}
	}

	void TriggerSingle(VolumeInput volume) {
		Debug.Log ("Single => " + GetVolumeText(volume));
	}

	void TriggerContinued(VolumeInput volume) {
		Debug.Log ("Flow => " + GetVolumeText(volume));
	}

	string GetVolumeText(VolumeInput volume) {
		if (volume == VolumeInput.HIGH) {
			return "HIGH";
		}
		else if (volume == VolumeInput.LOW) {
			return "LOW";
		}
		return "NONE";
	}

	VolumeInput GetVolumeInput(float volume) {
		if (volume >= Volume.HIGH) {
			return VolumeInput.HIGH;
		}
		else if (volume >= Volume.LOW) {
			return VolumeInput.LOW;
		}
		return VolumeInput.NONE;
	}

	static float CalibrateVolume(float volume) {
		return volume - (VOLUME_STEP / 2f);
	}
}
