using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PROJECTILE_TYPE {
	NONE,
	LIGHT,
	MEDIUM,
	STRONG
}

public class Projectile : MonoBehaviour {
	public float speed = 40.0f;
	Vector2 direction = Vector2.up;
	public PROJECTILE_TYPE type = PROJECTILE_TYPE.NONE;

	SpriteRenderer spriteRenderer;

	void Awake() {
		this.spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Shot(Vector2 direction, PROJECTILE_TYPE type) {
		this.direction = direction;
		this.type = type;
//		this.spriteRenderer.color = MainReferences.ColorGenerator.colorForType((int)type);
	}

	void Update () {
		this.transform.position += (Vector3)direction.normalized * Time.deltaTime * speed;
	}

	void OnBecameInvisible() {
		Destroy(gameObject);
	}
}
