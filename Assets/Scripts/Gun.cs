using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	public GameObject projectilePrefab;

	private LineSelector lineSelector;

	void Awake() {
		this.lineSelector = GetComponent<LineSelector>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
			projectile.Shot(Vector2.up, PROJECTILE_TYPE.LIGHT);
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
			projectile.Shot(Vector2.up, PROJECTILE_TYPE.MEDIUM);
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
			projectile.Shot(Vector2.up, PROJECTILE_TYPE.STRONG);
		}


		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			this.lineSelector.MoveLeft();
		}

		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			this.lineSelector.MoveRight();
		}
	}
}
