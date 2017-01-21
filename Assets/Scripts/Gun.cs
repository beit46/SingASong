using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	public AudioProcessor audioProcessor;
	public GameObject projectilePrefab;

	private LineSelector lineSelector;

	void Awake() {
		this.lineSelector = GetComponent<LineSelector>();

		audioProcessor.volumeInputSingle += VolumeInputSingle;
		audioProcessor.volumeInputContinued += VolumeInputContinued;
	}

	void VolumeInputSingle(AudioProcessor.VolumeInput value) {
		Debug.Log("Volume Input Single " + value);
		Shot();
	}

	void VolumeInputContinued(AudioProcessor.VolumeInput value) {
		Debug.Log("Volume Input Continued " + value);
	}

	void MoveLeft() {
		this.lineSelector.MoveLeft();
	}

	void MoveRight() {
		this.lineSelector.MoveRight();
	}

	void Shot() {
		Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
		projectile.Shot(Vector2.up, PROJECTILE_TYPE.LIGHT);
	}
		
//	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
			projectile.Shot(Vector2.up, PROJECTILE_TYPE.LIGHT);
		}
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
}
