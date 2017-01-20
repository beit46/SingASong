using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
	public float speed = 10.0f;
	Vector2 direction = Vector2.down;

	public delegate void TargetDestroyed(Target target);
	public TargetDestroyed OnTargetDestroyed;

	public void Shot(Vector2 direction, float speed) {
		this.direction = direction;
		this.speed = speed;
	}

	// Update is called once per frame
	void Update () {
		this.transform.position += (Vector3)direction.normalized * Time.deltaTime * speed;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Projectile") {
			MainReferences.AudioPlayer.PlayEffect();

			NotifyTargetDestroyed();

			Projectile projectile = collider.gameObject.GetComponent<Projectile>();
			Destroy(projectile.gameObject);

			Destroy(this.gameObject);
		}
	}

	void NotifyTargetDestroyed() {
		if(this.OnTargetDestroyed != null)
			this.OnTargetDestroyed(this);
	}

	void OnBecameInvisible() {
		NotifyTargetDestroyed();
		Destroy(gameObject);
	}
}
