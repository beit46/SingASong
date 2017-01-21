using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioProcessor : MonoBehaviour {

	public delegate void VolumeInputSingle(VolumeInput value);
	public VolumeInputSingle volumeInputSingle;

	public delegate void VolumeInputContinued(VolumeInput value);
	public VolumeInputContinued volumeInputContinued;

	const float VOLUME_STEP = 0.04f;

	class Volume {
		public const float NONE   = 0.0f;
		public const float LOW    = 0.03f;
		public const float HIGH   = LOW + VOLUME_STEP;
	}

	public enum VolumeInput {
		NONE,
		LOW,
		HIGH
	}

	const int THRESHOLD_SINGLE = 5;
	const int THRESHOLD_ZERO = 3;

	AudioInput _audioInput;

	float _currentVolume;
	int _sequenceCount;
	int _zeroCount;
	bool _canSingle;

	float _timeElapsed;

	void Start() {
		_audioInput = GetComponent<AudioInput> ();
		_currentVolume = Volume.NONE;
		_sequenceCount = 0;
		_timeElapsed = 0f;
		_canSingle = true;
		_zeroCount = 0;
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
		_timeElapsed += Time.deltaTime;
		if (_timeElapsed > 0f) {
			float newVolume = GetVolumeCardinal (volume);
			if (newVolume < _currentVolume) {
				if (newVolume != Volume.NONE || _zeroCount > THRESHOLD_ZERO) {
					if (_canSingle) {
						_canSingle = false;
						volumeInputSingle (GetVolumeInput(_currentVolume));
					}
					if (newVolume == Volume.NONE) {
						_canSingle = true;
					}
					_zeroCount = 0;
				}
				else {
					_zeroCount++;
				}
				_sequenceCount = 0;
			}
			else if (newVolume == _currentVolume) {
				if (_sequenceCount > THRESHOLD_SINGLE && newVolume != Volume.NONE) {
					_canSingle = false;
					volumeInputContinued (GetVolumeInput(_currentVolume));
					_zeroCount = 0;
				}
				else if (newVolume == Volume.NONE && _zeroCount <= THRESHOLD_ZERO) {
					_zeroCount++;
				}
				else if (newVolume == Volume.NONE) {
					_canSingle = true;
					_zeroCount = 0;
				}
				else {
					_sequenceCount++;
					_zeroCount = 0;
				}
			}
			else if (newVolume > _currentVolume) {
				_sequenceCount = 0;
				_zeroCount = 0;
			}
			_currentVolume = newVolume;
			_timeElapsed = 0f;
		}
	}

	void TriggerSingle(VolumeInput volume) {
//		Debug.Log ("Single => " + GetVolumeText(volume));
	}

	void TriggerContinued(VolumeInput volume) {
//		Debug.Log ("Flow => " + GetVolumeText(volume));
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
