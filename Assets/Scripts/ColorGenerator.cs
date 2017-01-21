using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator : MonoBehaviour {

	public Color colorLight;
	public Color colorMedium;
	public Color colorStrong;
	public Color blueTron;

	public Color colorForType(int type) {
//		Debug.Log("Color for type " + type);
		Color color;
		switch(type) {
		case 1:
			color = colorLight;
			break;
		case 2:
			color = colorMedium;
			break;
		case 3:
			color = colorStrong;
			break;
		default:
			color = colorLight;
			break;
		}
		return color;
	}
}
