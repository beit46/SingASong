using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSelector : MonoBehaviour {
	public List<Transform> lines = new List<Transform>();
	public Transform currentLine = null;

	public void MoveLeft() {
//		Debug.Log("move left");
		int index = lines.IndexOf(currentLine);
		if(index >= 1) {
			currentLine = lines[index - 1];
			MoveToLine(index);
		}
	}

	public void MoveRight() {
//		Debug.Log("move right");
		int index = lines.IndexOf(currentLine);
		if(index <= 1) {
			currentLine = lines[index + 1];
			MoveToLine(index);
		}
	}

	void MoveToLine(int lineIndex) {
		this.transform.position = new Vector3(currentLine.position.x, this.transform.position.y, 0);
	}
}
