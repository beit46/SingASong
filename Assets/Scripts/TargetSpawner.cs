using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
public class TargetSpawner : MonoBehaviour {
	public KoreopgrapherController koreographerController;
	private LineSelector lineSelector;
	public float targetSpeed = 15.0f;
	public List<Target> liveTargets;
	public GameObject TargetPrefab;

	public TARGET_TYPE nextSpawn = TARGET_TYPE.NONE;
	 
	public delegate void TargetSpawned();
	public TargetSpawned OnTargetSpawned;

	int enemyCounter = 0;
	int speedStep = 30;

	public enum KOREO_EVENT_TYPE {
		NONE,
		PRE_TARGET,
		TARGET,
		SONG_END
	}

	void Awake() {
		lineSelector = GetComponent<LineSelector>();
//		this.koreographerController.OnBeat += SpawnTarget;
		Koreographer.Instance.RegisterForEvents("NewKoreographyTrack", KoreographyEventCallback);

		this.nextSpawn = (TARGET_TYPE)Random.Range(1, 3);
	}

	public void KoreographyEventCallback(KoreographyEvent koreographyEvent) {
//		Debug.Log("I got payload " + koreographyEvent.GetIntValue());
		KOREO_EVENT_TYPE koreoEvent = (KOREO_EVENT_TYPE)koreographyEvent.GetIntValue();
		switch(koreoEvent) {
		case KOREO_EVENT_TYPE.TARGET:
			this.SpawnTarget();
			break;
		default:
			break;
		}
	}

	void SpawnTarget() {
		koreographerController.DecreaseVolumeForDuration(0.4f);

		CreateTargetAndShot();
		this.enemyCounter++;
		if (this.enemyCounter % speedStep == 0) {
			Debug.Log("time to increase speed");
			this.targetSpeed += 0.5f;
		}
			
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
		target.Shot(Vector2.down, targetSpeed, nextSpawn);
		target.OnTargetDestroyed += TargetDestroyed;
		target.OnTargetEscaped += TargetEscaped;
		this.liveTargets.Add(target);

		nextSpawn = (TARGET_TYPE)Random.Range(1, 3);
		NotifyTargetSpawned();
	}

	void NotifyTargetSpawned() {
		if(this.OnTargetSpawned != null)
			this.OnTargetSpawned();
	}

	void TargetDestroyed(Target target) {
//		Debug.Log("Target Destroyed");
		ClearTarget(target);

		MainReferences.ScoreController.Hit();
	}

	void TargetEscaped(Target target) {
//		Debug.Log("Target escpaed");
		ClearTarget(target);

		MainReferences.ScoreController.Miss();
	}

	void ClearTarget(Target target) {
		target.OnTargetDestroyed -= TargetDestroyed;
		target.OnTargetEscaped -= TargetEscaped;
		this.liveTargets.Remove(target);
	}

}
