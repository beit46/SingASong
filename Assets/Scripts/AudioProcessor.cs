using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioProcessor : MonoBehaviour {

	public delegate void VolumeInputSingle(VolumeInput value);
	public VolumeInputSingle volumeInputSingle;

	public delegate void VolumeInputContinued(VolumeInput value);
	public VolumeInputContinued volumeInputContinued;

	public Volume volume = new Volume();

	[System.Serializable]
	public class Volume {
//		public float VOLUME_STEP = 0.04f;
//		public float LOW_TO_HIGH_DELTA = 0.02f;
		public float NONE = 0.0f;
		public float LOW  = 0.02f;
		public float MIDDLE = 0.04f;
		public float HIGH = 0.06f;
	}

	public enum VolumeInput {
		NONE,
		LOW,
		HIGH
	}

	public int THRESHOLD_SINGLE = 10;
	public int THRESHOLD_ZERO = 3;

	AudioInput _audioInput;

	float _currentVolume;
	int _sequenceCount;
	bool _canSingle;
	bool _block;

	void Start() {
		_audioInput = GetComponent<AudioInput> ();
		_currentVolume = this.volume.NONE;
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
		if (volume >= this.volume.HIGH) {
			return this.volume.HIGH;
		}
		else if (volume >= this.volume.LOW && volume <= this.volume.HIGH - 0.01f) {
			return this.volume.LOW;
		}
		return this.volume.NONE;
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
				if (newVolume == this.volume.NONE) {
					_block = true;
					_canSingle = true;
				}
				_sequenceCount = 0;
			}
			else if (newVolume == _currentVolume) {
				if (_sequenceCount > THRESHOLD_SINGLE && newVolume != this.volume.NONE) {
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
		if (volume >= this.volume.HIGH) {
			return VolumeInput.HIGH;
		}
		else if (volume >= this.volume.LOW) {
			return VolumeInput.LOW;
		}
		return VolumeInput.NONE;
	}
}
