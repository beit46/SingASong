using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuTutorial : MonoBehaviour {

	public AudioProcessor _audioProcessor;

	public Text instruction;
	public Text description;
	public Button continueBtn;

	Status _status;

	public Sprite _spriteCheck;

	enum Status {
		SINGLE_LOW,
		SINGLE_HIGH,
		CONTINUED,
		DONE
	}

	// Use this for initialization
	void Start () {
		_status = Status.SINGLE_LOW;

		_audioProcessor.volumeInputSingle += CheckSingle;
		_audioProcessor.volumeInputContinued += CheckContinued;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void CheckSingle(AudioProcessor.VolumeInput volume) {
		if (_status == Status.SINGLE_LOW && volume == AudioProcessor.VolumeInput.LOW) {
			instruction.text = "SINGLE HIGH SOUND..";
			description.text = "..to emit yellow lightnings\nto kill orange enemies";
			_status = Status.SINGLE_HIGH;
		}
		if (_status == Status.SINGLE_HIGH && volume == AudioProcessor.VolumeInput.HIGH) {
			instruction.text = "PROLONGED SOUND..";
			description.text = "..to generate a protective shield";
			_status = Status.CONTINUED;
		}
	}

	void CheckContinued(AudioProcessor.VolumeInput volume) {
		if (_status == Status.CONTINUED) {
			instruction.gameObject.SetActive (false);
			description.gameObject.SetActive (false);
			continueBtn.gameObject.SetActive(true);
			_status = Status.DONE;
		}
	}
}
