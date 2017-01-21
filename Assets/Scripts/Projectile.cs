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
		this.spriteRenderer.color = MainReferences.ColorGenerator.colorForType((int)type);
		ChangeParticleStartingColorOverTime();

//		ParticleSystem.MinMaxGradient gradient = this.particleSystem.main.startColor;
//		gradient.color = MainReferences.ColorGenerator.colorForType((int)type);
		this.particleSystem.startColor = MainReferences.ColorGenerator.colorForType((int)type);

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
//		this.trailRenderer.widthMultiplier = Mathf.Lerp(0.5f, 4.0f, 0.3f);
//		this.trailRenderer.time = Mathf.Lerp(0.03f, 0.2f, 0.05f);
	}

	void OnBecameInvisible() {
		Destroy(gameObject);
	}
}
