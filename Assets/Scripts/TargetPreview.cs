using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPreview : MonoBehaviour {
	public RuntimeAnimatorController blueEnemyAnimator;
	public RuntimeAnimatorController organgeEnemyAnimator;

	public TargetSpawner spawner;
	private SpriteRenderer spriteRenderer;
	private Animator animator;

	void Awake() {
		this.spriteRenderer = GetComponent<SpriteRenderer>();
		this.animator = GetComponent<Animator>();;
		this.spawner.OnTargetSpawned += TargetSpawned;

		TargetSpawned();
	}

	void TargetSpawned() {
		if (spawner.nextSpawn == TARGET_TYPE.LIGHT)
			this.animator.runtimeAnimatorController = blueEnemyAnimator;
		else
			this.animator.runtimeAnimatorController = organgeEnemyAnimator;
	}
}
