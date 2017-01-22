using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuTutorial : MonoBehaviour {

	public AudioProcessor _audioProcessor;
	Image _imageSingleLow;
	Image _imageSingleHigh;
	Image _imageContinuedLow;
	Image _imageContinuedHigh;

	Text _textSingleLow;
	Text _textSingleHigh;
	Text _textContinuedLow;
	Text _textContinuedHigh;

	int _countSingleLow;
	int _countSingleHigh;
	int _countContinuedLow;
	int _countContinuedHigh;

	Status _status;

	public Sprite _spriteCheck;

	enum Status {
		SINGLE_LOW,
		SINGLE_HIGH,
		CONTINUED_LOW,
		CONTINUED_HIGH,
		DONE
	}

	// Use this for initialization
	void Start () {
		_status = Status.SINGLE_LOW;

		_countSingleLow = 0;
		_countSingleHigh = 0;
		_countContinuedLow = 0;
		_countContinuedHigh = 0;

		_audioProcessor.volumeInputSingle += CheckSingle;
		_audioProcessor.volumeInputContinued += CheckContinued;

		foreach (Transform t in this.transform) {
			if (t.gameObject.name == "SingleLow") {
				_imageSingleLow = t.GetComponent<Image> ();
			}
			else if (t.gameObject.name == "SingleHigh") {
				_imageSingleHigh = t.GetComponent<Image> ();
			}
			else if (t.gameObject.name == "ContinuedLow") {
				_imageContinuedLow = t.GetComponent<Image> ();
			}
			else if (t.gameObject.name == "ContinuedHigh") {
				_imageContinuedHigh = t.GetComponent<Image> ();
			}
			else if (t.gameObject.name == "TextSingleLow") {
				_textSingleLow = t.GetComponent<Text> ();
			}
			else if (t.gameObject.name == "TextSingleHigh") {
				_textSingleHigh = t.GetComponent<Text> ();
			}
			else if (t.gameObject.name == "TextContinuedLow") {
				_textContinuedLow = t.GetComponent<Text> ();
			}
			else if (t.gameObject.name == "TextContinuedHigh") {
				_textContinuedHigh = t.GetComponent<Text> ();
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (_status == Status.DONE) {
			SceneManager.LoadScene ("MainMenu");
		}
	}

	void CheckSingle(AudioProcessor.VolumeInput volume) {
		if (_status == Status.SINGLE_LOW && volume == AudioProcessor.VolumeInput.LOW) {
			_countSingleLow++;
			if (_countSingleLow > 0) {
				_imageSingleLow.sprite = _spriteCheck;
				_imageSingleHigh.gameObject.SetActive(true);
				_textSingleHigh.gameObject.SetActive(true);
				_status = Status.SINGLE_HIGH;
			}
		}
		else if (_status == Status.SINGLE_HIGH && volume == AudioProcessor.VolumeInput.HIGH) {
			_countSingleHigh++;
			if (_countSingleHigh > 0) {
				_imageSingleHigh.sprite = _spriteCheck;
				_imageContinuedLow.gameObject.SetActive(true);
				_textContinuedLow.gameObject.SetActive(true);
				_status = Status.CONTINUED_LOW;
			}
		}
	}

	void CheckContinued(AudioProcessor.VolumeInput volume) {
		if (_status == Status.CONTINUED_LOW && (volume == AudioProcessor.VolumeInput.LOW || volume == AudioProcessor.VolumeInput.HIGH)) {
			_countContinuedLow++;
			if (_countContinuedLow > 4) {
				_imageContinuedLow.sprite = _spriteCheck;
//				_imageContinuedHigh.gameObject.SetActive(true);
//				_textContinuedHigh.gameObject.SetActive(true);
				_status = Status.DONE;
			}
		}
//		else if (_status == Status.CONTINUED_HIGH && volume == AudioProcessor.VolumeInput.HIGH) {
//			_countContinuedHigh++;
//			if (_countContinuedHigh > 4) {
//				_imageContinuedHigh.sprite = _spriteCheck;
//				_status = Status.DONE;
//			}
//		}
	}
}
