using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {
	private Vector3 startPosition = Vector3.zero;
	private Vector3 endPosition = Vector3.zero;

	public List<Transform> backgrounds = new List<Transform>();

	public float scrollSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		this.startPosition = backgrounds[0].transform.position;
		this.endPosition = backgrounds[backgrounds.Count - 1].transform.position;
		Debug.Log("start position " + this.startPosition);
		Debug.Log("end position " + this.endPosition);
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Transform background in backgrounds) {
			if(background.transform.position.y <= this.endPosition.y)
				background.transform.position = this.startPosition;
			background.transform.position += (Vector3)Vector2.down * scrollSpeed * Time.deltaTime;
		}
	}
}
