using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollision : MonoBehaviour {

	enum Type {
		NONE,
		BLUE,
		ORANGE
	}

	Type type;
	SpriteRenderer spriteRenderer;
	Animator animator;
	public Sprite enemyOrangeA;
	public RuntimeAnimatorController enemyOrangeB;
	public Sprite enemyBlueA;
	public RuntimeAnimatorController enemyBlueB;

	// Use this for initialization
	void Start () {
		type = Type.BLUE;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider) {

		if (collider.gameObject.tag == "Projectile") {
			Projectile projectile = collider.gameObject.GetComponent<Projectile>();

			if ((int)projectile.type == (int)this.type) {
				if (this.type == Type.BLUE) {
					spriteRenderer.sprite = enemyOrangeA;
					animator.runtimeAnimatorController = enemyOrangeB;
					type = Type.ORANGE;
				}
				else if (this.type == Type.ORANGE) {
					spriteRenderer.sprite = enemyBlueA;
					animator.runtimeAnimatorController = enemyBlueB;
					type = Type.BLUE;
				}
			}
			Destroy(projectile.gameObject);
		} 
	}
}
