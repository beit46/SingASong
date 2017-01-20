using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PROJECTILE_FORCE {
	LIGHT,
	MEDIUM,
	STRONG
}

public class Projectile : MonoBehaviour {
	public float speed = 40.0f;
	Vector2 direction = Vector2.up;

	public void Shot(Vector2 direction) {
		this.direction = direction;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += (Vector3)direction.normalized * Time.deltaTime * speed;
	}

	void OnBecameInvisible() {
		Destroy(gameObject);
	}
}
