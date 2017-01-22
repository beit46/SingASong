using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	public AudioProcessor audioProcessor;
	public GameObject projectilePrefab;

	private LineSelector lineSelector;
	private bool isReloading = false;
	public float reloadTime = 0.3f;

	float elapsedTime;
	bool shieldOn;

	public delegate void ToggleShield (bool active);
	public ToggleShield toggleShield;

	public SpriteRenderer spriteShield;
	public ScoreController scoreController;

	void Awake() {
		this.lineSelector = GetComponent<LineSelector>();

		audioProcessor.volumeInputSingle += VolumeInputSingle;
		audioProcessor.volumeInputContinued += VolumeInputContinued;

		elapsedTime = 0f;
		shieldOn = false;
		toggleShield += UpdateShield;
	}

	void VolumeInputSingle(AudioProcessor.VolumeInput value) {
		//Debug.Log("Volume Input Single " + value);
		if (!shieldOn) {
			switch (value) {
			case AudioProcessor.VolumeInput.LOW:
				Shot (PROJECTILE_TYPE.LIGHT);
				break;
			case AudioProcessor.VolumeInput.HIGH:
				Shot (PROJECTILE_TYPE.MEDIUM);
				break;
			default:
				break;
			}
			toggleShield (false);
		}
	}

	void VolumeInputContinued(AudioProcessor.VolumeInput value) {
		//Debug.Log("Volume Input Continued " + value);
		elapsedTime = 0f;
		toggleShield (true);
	}

	void MoveLeft() {
		this.lineSelector.MoveLeft();
	}

	void MoveRight() {
		this.lineSelector.MoveRight();
	}

	void Shot(PROJECTILE_TYPE type) {
		if (!this.isReloading) {
			Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
			projectile.Shot(Vector2.up, type);
			this.isReloading = true;
			Invoke("Reload", reloadTime);
		}
	}

	void Reload() {
		this.isReloading = false;
	}
		
//	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			this.Shot(PROJECTILE_TYPE.LIGHT);
		}
		elapsedTime += Time.deltaTime;
		if (shieldOn && elapsedTime > 0.05f) {
			toggleShield (false);
		}
//		if (Input.GetKeyDown(KeyCode.D)) {
//			toggleShield (true);
//		}
//		if (Input.GetKeyDown(KeyCode.S)) {
//			toggleShield (false);
//		}
//		elapsedTime = 0;
//		if (Input.GetKeyDown(KeyCode.S)) {
//			Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
//			projectile.Shot(Vector2.up, PROJECTILE_TYPE.MEDIUM);
//		}
//		if (Input.GetKeyDown(KeyCode.D)) {
//			Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
//			projectile.Shot(Vector2.up, PROJECTILE_TYPE.STRONG);
//		}
//
//
//		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
//			this.lineSelector.MoveLeft();
//		}
//
//		if (Input.GetKeyDown(KeyCode.RightArrow)) {
//			this.lineSelector.MoveRight();
//		}
	}

	void UpdateShield(bool active) {
		if (active && !shieldOn) {
			shieldOn = true;
			spriteShield.gameObject.SetActive (true);
			scoreController.Miss();
		}
		else if (!active && shieldOn) {
			shieldOn = false;
			spriteShield.gameObject.SetActive (false);
		}
	}

	public bool ShieldOn() {
		return shieldOn;
	}
}
