using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {
	public AudioClip orangeProjectileEffect;
	public AudioClip blueProjectileEffect;

	public AudioClip orangeExplosionEffect;
	public AudioClip blueExplosionEffect;

	public enum EFFECT_TYPE {
		ORANGE_EXPLOSION,
		ORANGE_PROJECTILE,
		BLUE_EXPLOSION,
		BLUE_PROJECTILE
	}

	public void PlayEffect(EFFECT_TYPE type) {
		AudioSource audioSource = GetComponent<AudioSource>();

		switch(type) {
		case EFFECT_TYPE.BLUE_EXPLOSION:
			audioSource.clip = this.blueExplosionEffect;
			break;
		case EFFECT_TYPE.BLUE_PROJECTILE:
			audioSource.clip = this.blueProjectileEffect;
			break;
		case EFFECT_TYPE.ORANGE_EXPLOSION:
			audioSource.clip = this.orangeExplosionEffect;
			break;
		case EFFECT_TYPE.ORANGE_PROJECTILE:
			audioSource.clip = this.orangeProjectileEffect;
			break;
		default:
			audioSource.clip = this.blueExplosionEffect;
			break;
		}

		audioSource.Play(); 
	}
}
