using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPreview : MonoBehaviour {
	public TargetSpawner spawner;
	private SpriteRenderer spriteRenderer;

	void Awake() {
		this.spriteRenderer = GetComponent<SpriteRenderer>();
		this.spawner.OnTargetSpawned += TargetSpawned;
		this.spriteRenderer.color = MainReferences.ColorGenerator.colorForType((int)spawner.nextSpawn);
	}

	void TargetSpawned() {
		this.spriteRenderer.color = MainReferences.ColorGenerator.colorForType((int)spawner.nextSpawn);
	}
}
