using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesLayout : MonoBehaviour {
	List<Transform> lines = new List<Transform>();

	// Use this for initialization
	void Start () {
		float height = Camera.main.orthographicSize * 2.0f;
		float width = height * Screen.width / Screen.height;

		float center = width / 2;
		Debug.Log("Screen width is " + width + ", scree center " + center);

		float delta = center - width + width / 6;

		foreach (Transform t in transform) {
			lines.Add(t);
			t.transform.position = new Vector3(delta, t.position.y, 0);
			delta += width / 3;
		}



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
