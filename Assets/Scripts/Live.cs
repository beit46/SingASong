using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Live : MonoBehaviour {
	public int startingLives = 5;
	public int currentLives = 5;

	public delegate void Dead ();
	public Dead OnDead;

	public void Hit() {
		this.currentLives--;
		MainReferences.UIInterface.SetLives(currentLives);

		if(this.currentLives <= 0)
			this.NotifyDead();
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

}
