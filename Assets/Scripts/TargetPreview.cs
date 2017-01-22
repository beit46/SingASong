using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class TargetPreview : MonoBehaviour {
	public RuntimeAnimatorController blueEnemyAnimator;
	public RuntimeAnimatorController organgeEnemyAnimator;

	public TargetSpawner spawner;
	private SpriteRenderer spriteRenderer;
	private Animator animator;

	void Start() {
		this.spriteRenderer = GetComponent<SpriteRenderer>();
		this.animator = GetComponent<Animator>();;
		this.spawner.OnTargetSpawned += TargetSpawned;

		TargetSpawned();

		Koreographer.Instance.RegisterForEvents("NewKoreographyTrack", KoreographyEventCallback);
	}

	void TargetSpawned() {
		Color c = this.spriteRenderer.color;
		c.a = 0.0f;
		spriteRenderer.color = c;
	}

	public void KoreographyEventCallback(KoreographyEvent koreEvent) {
//		Debug.Log("I got payload " + koreEvent.GetIntValue());
		TargetSpawner.KOREO_EVENT_TYPE koreoEvent = (TargetSpawner.KOREO_EVENT_TYPE)koreEvent.GetIntValue();
		switch(koreoEvent) {
		case TargetSpawner.KOREO_EVENT_TYPE.PRE_TARGET:
			AnimateAlpha();
			PreviewNextEnemy();
			break;
		default:
			break;
		}
	}

	void PreviewNextEnemy() {
		if (spawner.nextSpawn == TARGET_TYPE.BLUE)
			this.animator.runtimeAnimatorController = blueEnemyAnimator;
		else
			this.animator.runtimeAnimatorController = organgeEnemyAnimator;

		AnimateAlpha();
	}

	void AnimateAlpha() {
		Color c = this.spriteRenderer.color;
		c.a = 1.0f;
		this.spriteRenderer.color = c;
	}
}
