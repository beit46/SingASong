using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {
	public float targetSpeed = 15.0f;
	public List<Target> liveTargets;
	public GameObject TargetPrefab;
	bool isSpawning = false;

	void Update () {
		if (liveTargets.Count <= 1 && !isSpawning) {
			this.isSpawning = true;
			Invoke("SpawnTarget", Random.Range(0.3f, 1.5f));
		}
	}

	void SpawnTarget() {
		Target target = Instantiate(TargetPrefab, this.transform.position, Quaternion.identity).GetComponent<Target>();
		target.Shot(Vector2.down, targetSpeed);
		target.OnTargetDestroyed += TargetDestroyed;
		this.liveTargets.Add(target);

		this.isSpawning = false;
	}

	void TargetDestroyed(Target target) {
		this.liveTargets.Remove(target);
	}
}
