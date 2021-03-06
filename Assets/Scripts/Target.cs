﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TARGET_TYPE {
	NONE,
	BLUE,
	ORANGE,
	STRONG
}

public class Target : MonoBehaviour {
	public RuntimeAnimatorController blueEnemyAnimator;
	public RuntimeAnimatorController organgeEnemyAnimator;

	public GameObject blueExplosion;
	public GameObject orangeExplosion;

	public float speed = 10.0f;
	public TARGET_TYPE type = TARGET_TYPE.NONE;
	Vector2 direction = Vector2.down;

	public delegate void TargetDestroyed(Target target);
	public TargetDestroyed OnTargetDestroyed;

	public delegate void TargetEscaped(Target target);
	public TargetEscaped OnTargetEscaped;

	SpriteRenderer spriteRenderer;
	Animator animator;

	void Awake() {
		this.spriteRenderer = GetComponent<SpriteRenderer>();
		this.animator = GetComponent<Animator>();
	}

	public void Shot(Vector2 direction, float speed, TARGET_TYPE type) {
		this.direction = direction;
		this.speed = speed;
		this.type = type;

		if (type == TARGET_TYPE.BLUE)
			this.animator.runtimeAnimatorController = blueEnemyAnimator;
		else
			this.animator.runtimeAnimatorController = organgeEnemyAnimator;
		
	}

	public void Hit() {
		NotifyTargetEscaped();
		this.Explode();
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
				Explode();

				projectile.didHitTarget = true;

				Destroy(projectile.gameObject);
				NotifyTargetDestroyed();
				Destroy(this.gameObject);
			} else {
				projectile.MissedTarget();
//				MainReferences.ScoreController.Miss();
			}
			
		} 
	}

	void Explode() {
		if(this.type == TARGET_TYPE.BLUE) {
			MainReferences.AudioPlayer.PlayEffect(AudioPlayer.EFFECT_TYPE.BLUE_EXPLOSION);
			Instantiate(blueExplosion, this.transform.position, Quaternion.identity);
		} else if(this.type == TARGET_TYPE.ORANGE) {
			MainReferences.AudioPlayer.PlayEffect(AudioPlayer.EFFECT_TYPE.ORANGE_EXPLOSION);
			Instantiate(orangeExplosion, this.transform.position, Quaternion.identity);
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
