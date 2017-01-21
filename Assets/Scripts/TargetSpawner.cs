using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {
	private float score = 0;
	private float scoreStep = 10f;
	private float multiplier = 1f;
	private int consecutiveHitSuccesfull = 0;
	private bool lastShotWasAHit = false;

	private float numberOfHitToIncreaseMultiplier = 2;
	private int successfullConsecutiveHits = 0;


	public KoreopgrapherController koreographerController;
	private LineSelector lineSelector;
	public float targetSpeed = 15.0f;
	public List<Target> liveTargets;
	public GameObject TargetPrefab;
	bool isSpawning = false;

	void Awake() {
		lineSelector = GetComponent<LineSelector>();
		this.koreographerController.OnBeat += SpawnTarget;

		MainReferences.UIInterface.SetScore(score);
		MainReferences.UIInterface.SetMultiplier(multiplier);
	}

//	void Update () {
//		if (liveTargets.Count <= 1 && !isSpawning) {
//			this.isSpawning = true;
//			Invoke("SpawnTarget", Random.Range(0.3f, 1.5f));
//		}
//	}

	void SpawnTarget() {
		koreographerController.DecreaseVolumeForDuration(0.4f);

//		MoveTargetToLeftOrRight();
		
		CreateTargetAndShot();

//		this.isSpawning = false;
	}

	void MoveTargetToLeftOrRight() {
		int moveTo = Random.Range(0, 3);
		if(moveTo == 0)
			lineSelector.MoveLeft();
		else if(moveTo == 2)
			lineSelector.MoveRight();
	}

	void CreateTargetAndShot() {
		Target target = Instantiate(TargetPrefab, this.transform.position, Quaternion.identity).GetComponent<Target>();
		target.Shot(Vector2.down, targetSpeed, (TARGET_TYPE)Random.Range(1, 3));
		target.OnTargetDestroyed += TargetDestroyed;
		target.OnTargetEscaped += TargetEscaped;
		this.liveTargets.Add(target);
	}

	void TargetDestroyed(Target target) {
		Debug.Log("Target Destroyed");
		ClearTarget(target);
		this.lastShotWasAHit = true;
		if (this.lastShotWasAHit) {
			this.consecutiveHitSuccesfull++;
			if (this.consecutiveHitSuccesfull >= this.numberOfHitToIncreaseMultiplier) {
				//Increase multipilier
				this.multiplier += 0.1f;
				this.numberOfHitToIncreaseMultiplier *= 1.5f;

				this.consecutiveHitSuccesfull = 0;
			}
		} else {
			this.multiplier = 1.0f;
			this.consecutiveHitSuccesfull = 0;
		}


		this.score += scoreStep * multiplier;

		MainReferences.UIInterface.SetScore(score);
		MainReferences.UIInterface.SetMultiplier(multiplier);
	}

	void TargetEscaped(Target target) {
		Debug.Log("Target escpaed");
		ClearTarget(target);

		this.multiplier = 1.0f;
		this.consecutiveHitSuccesfull = 0;
		MainReferences.UIInterface.SetMultiplier(multiplier);

		this.lastShotWasAHit = false;
	}

	void ClearTarget(Target target) {
		target.OnTargetDestroyed -= TargetDestroyed;
		target.OnTargetEscaped -= TargetEscaped;
		this.liveTargets.Remove(target);
	}

}
