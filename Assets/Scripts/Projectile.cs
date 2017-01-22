using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PROJECTILE_TYPE {
	NONE,
	BLUE,
	ORANGE,
	STRONG
}

public class Projectile : MonoBehaviour {
	public float speed = 40.0f;
	Vector2 direction = Vector2.up;
	public PROJECTILE_TYPE type = PROJECTILE_TYPE.NONE;

	public delegate void ProjectileMissedTarget(Projectile projectile);
	public ProjectileMissedTarget OnProjectileMissedTarget;
	public bool didHitTarget = false;

	SpriteRenderer spriteRenderer;
	TrailRenderer trailRenderer;
	ParticleSystem particleSystem;

	void Awake() {
		this.spriteRenderer = GetComponent<SpriteRenderer>();
		this.trailRenderer = GetComponent<TrailRenderer>();
		this.particleSystem = GetComponentInChildren<ParticleSystem>();
	}

	public void Shot(Vector2 direction, PROJECTILE_TYPE type) {
		this.direction = direction;
		this.type = type;
		ChangeParticleStartingColorOverTime();
	}

	public void MissedTarget() {
		//PLAY WRPNG SOUND
		this.OnBecameInvisible();
	}

	void ChangeParticleStartingColorOverTime( ) {
		var col = this.particleSystem.colorOverLifetime;
		col.enabled = true;

		Gradient grad = new Gradient();
		grad.SetKeys( new GradientColorKey[] { new GradientColorKey(MainReferences.ColorGenerator.colorForType((int)type), 0.0f), new GradientColorKey(MainReferences.ColorGenerator.blueTron, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) } );

		col.color = grad;
	}

	void Update () {
		this.transform.position += (Vector3)direction.normalized * Time.deltaTime * speed;
	}

	void OnBecameInvisible() {
		if (!this.didHitTarget)
			NotifyProjectMissedTarget();
		Destroy(gameObject);
	}

	void NotifyProjectMissedTarget() {
		if(this.OnProjectileMissedTarget!= null)
			this.OnProjectileMissedTarget(this);
	}
}
