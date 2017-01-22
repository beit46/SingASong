using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Live : MonoBehaviour {
	public Transform livesBarPrefabs;
	public List<Transform> livesRepresentation = new List<Transform>();
	public Transform livesBarContainer;

	public int startingLives = 6;
	public int currentLives = 6;

	public delegate void Dead ();
	public Dead OnDead;
	Gun gun;

	void Start() {
		gun = GetComponent<Gun>();
		float step = 360f / (startingLives - 1f);
		for (int index = 0; index < startingLives - 1; index++) {
			Transform t  = Instantiate(livesBarPrefabs, this.transform.position, Quaternion.identity);
			t.parent = this.livesBarContainer;
			t.Rotate(0.0f, 0.0f, step * index);
			this.livesRepresentation.Add(t);
			t.localScale = new Vector3(0.9f, 0.9f, 0.9f);
		}
	}

	public void Hit() {
		if (!gun.ShieldOn ()) {
			this.currentLives--;
			MainReferences.UIInterface.SetLives (currentLives);
			RemoveLivesBar();
			this.LayoutLives();
			if (this.currentLives <= 0)
				this.NotifyDead ();
		}
	}
	
	void LayoutLives() {
		float step = 180f / (float)this.livesRepresentation.Count;
		int counter = 0;
		foreach (Transform t in this.livesRepresentation) {
			t.rotation = Quaternion.identity;
			t.Rotate(0.0f, 0.0f, step * counter++);
		}
	}

	void NotifyDead() {
		if(this.OnDead != null)
			this.OnDead();
	}
		

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Target") {
			this.Hit();
			Target t = collider.gameObject.GetComponent<Target>();
			t.Hit();
		} 
	}


	void RemoveLivesBar() {
		if (livesRepresentation.Count >= 1) {
			Transform t = livesRepresentation[0];
			this.livesRepresentation.Remove(t);
			Destroy(t.gameObject);
		}
	}

}
