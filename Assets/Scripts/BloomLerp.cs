using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class BloomLerp : MonoBehaviour {
	public BloomOptimized bloomOptimized;

	bool isIncreasing = false;
	bool isDecreasing = false;
	bool isProcessing = false;

	float low = 0.25f;
	float high = 1f;

	int count = 0;
	void Update () {
		if (count >= 2) {
			if (!this.isProcessing) {
				if(bloomOptimized.intensity >= high) {
					this.isDecreasing = true;
					this.isProcessing = true;
				} else if (bloomOptimized.intensity <= low ) {
					this.isIncreasing = true;
					this.isProcessing = true;
				}
			}

			if (this.isProcessing) {
				if(this.isIncreasing)
					this.bloomOptimized.intensity += 0.05f;
				if (this.isDecreasing)
					this.bloomOptimized.intensity -= 0.05f;

				if(this.isIncreasing && bloomOptimized.intensity >= high) {
					this.isProcessing = false;
					this.isIncreasing = false;
				} else if(this.isDecreasing && bloomOptimized.intensity <= low) {
					this.isDecreasing = false;
					this.isProcessing = false;
				}

			}
			count = 0;
		}
		count++;
	}

}
