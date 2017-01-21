using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TARGET_TYPE {
	NONE,
	LIGHT,
	MEDIUM,
	STRONG
}

public class Target : MonoBehaviour {
	public float speed = 10.0f;
	public TARGET_TYPE type = TARGET_TYPE.NONE;
	Vector2 direction = Vector2.down;

	public delegate void TargetDestroyed(Target target);
	public TargetDestroyed OnTargetDestroyed;

	public delegate void TargetEscaped(Target target);
	public TargetEscaped OnTargetEscaped;

	SpriteRenderer spriteRenderer;

	void Awake() {
		this.spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Shot(Vector2 direction, float speed, TARGET_TYPE type) {
		this.direction = direction;
		this.speed = speed;
		this.type = type;
		this.spriteRenderer.color = MainReferences.ColorGenerator.colorForType((int)type);
	}

	public void Hit() {
		NotifyTargetEscaped();
		Destroy(this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		this.transform.position += (Vector3)direction.normalized * Time.deltaTime * speed;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Projectile") {
			Projectile projectile = collider.gameObject.GetComponent<Projectile>();

			if ((int)projectile.type == (int)this.type) {
				MainReferences.AudioPlayer.PlayEffect();
				Destroy(projectile.gameObject);
				Hit();
			}
		} 
	}

	void NotifyTargetDestroyed() {
		if(this.OnTargetDestroyed != null)
			this.OnTargetDestroyed(this);
	}	
	
	void OnBecameInvisible() {
		NotifyTargetEscaped();
		Destroy(gameObject);
	}

	void NotifyTargetEscaped() {
		if(this.OnTargetEscaped != null)
			this.OnTargetEscaped(this);
	}
}
