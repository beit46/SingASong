using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {
	public KoreopgrapherController koreographerController;
	private LineSelector lineSelector;
	public float targetSpeed = 15.0f;
	public List<Target> liveTargets;
	public GameObject TargetPrefab;
	bool isSpawning = false;

	void Awake() {
		lineSelector = GetComponent<LineSelector>();
		this.koreographerController.OnBeat += SpawnTarget;
	}

//	void Update () {
//		if (liveTargets.Count <= 1 && !isSpawning) {
//			this.isSpawning = true;
//			Invoke("SpawnTarget", Random.Range(0.3f, 1.5f));
//		}
//	}

	void SpawnTarget() {
		int moveTo = Random.Range(0, 3);
		if(moveTo == 0)
			lineSelector.MoveLeft();
		else if(moveTo == 2)
			lineSelector.MoveRight();
		
		Target target = Instantiate(TargetPrefab, this.transform.position, Quaternion.identity).GetComponent<Target>();
		target.Shot(Vector2.down, targetSpeed, (TARGET_TYPE)Random.Range(1, 4));
		target.OnTargetDestroyed += TargetDestroyed;
		this.liveTargets.Add(target);

//		this.isSpawning = false;
	}

	void TargetDestroyed(Target target) {
		this.liveTargets.Remove(target);
	}
}
