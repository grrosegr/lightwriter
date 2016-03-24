using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Popup : Singleton<Popup> {

	// Use this for initialization
	void Start () {
		initialPos = transform.position;
		myText.enabled = false;
	}

	Vector2 initialPos;

	float floatDistance = 15.0f;
	float time = 0.75f;

	IEnumerator FadeOut() {
		Color col = myText.color;
		Vector2 pos = transform.position;
		myText.enabled = true;

		while (col.a > 0) {
			float delta = Time.deltaTime / time;
			pos.y += delta * floatDistance;
			transform.position = pos;
			col.a -= delta;
			myText.color = col;
			yield return null; // wait until next frame
		}
		col.a = 0;
		myText.color = col;
		myText.enabled = false;
	}

	public void Trigger() {
		StopAllCoroutines();

		transform.position = initialPos;
		Color col = myText.color;
		col.a = 1.0f;
		myText.color = col;

		StartCoroutine(FadeOut());
	}

}
