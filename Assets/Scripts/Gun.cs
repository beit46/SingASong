using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	public AudioProcessor audioProcessor;
	public GameObject projectileBluePrefab;
	public GameObject projectileOrangePrefab;

	private LineSelector lineSelector;
	private bool isReloading = false;
	public float reloadTime = 0.3f;

	float elapsedTime;
	bool shieldOn;
	int shields;

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
		shields = 5;
		toggleShield += UpdateShield;
	}

	void VolumeInputSingle(AudioProcessor.VolumeInput value) {
		//Debug.Log("Volume Input Single " + value);
		if (!shieldOn) {
			switch (value) {
			case AudioProcessor.VolumeInput.LOW:
				MainReferences.AudioPlayer.PlayEffect(AudioPlayer.EFFECT_TYPE.BLUE_EXPLOSION);
				Shot (PROJECTILE_TYPE.BLUE);
				break;
			case AudioProcessor.VolumeInput.HIGH:
				MainReferences.AudioPlayer.PlayEffect(AudioPlayer.EFFECT_TYPE.ORANGE_EXPLOSION);
				Shot (PROJECTILE_TYPE.ORANGE);
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
		//if (shields > 0) {
			toggleShield (true);
		//}
	}

	void MoveLeft() {
		this.lineSelector.MoveLeft();
	}

	void MoveRight() {
		this.lineSelector.MoveRight();
	}

	void Shot(PROJECTILE_TYPE type) {
		if (!this.isReloading) {
			Projectile projectile;
			if (type == PROJECTILE_TYPE.BLUE) 
				projectile = Instantiate(projectileBluePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
			else 
				projectile = Instantiate(projectileOrangePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();

			projectile.OnProjectileMissedTarget += OnProjectileMissedTarget;
			projectile.Shot(Vector2.up, type);
			this.isReloading = true;
			Invoke("Reload", reloadTime);
		}
	}

	void OnProjectileMissedTarget(Projectile projectile) {
		projectile.OnProjectileMissedTarget -= OnProjectileMissedTarget;
		if (scoreController != null) {
			this.scoreController.Miss ();
		}
	}

	void Reload() {
		this.isReloading = false;
	}
		
//	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			this.Shot(PROJECTILE_TYPE.BLUE);
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			this.Shot(PROJECTILE_TYPE.ORANGE);
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
			if (scoreController != null) {
				scoreController.Miss ();
			}
			shields--;
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
