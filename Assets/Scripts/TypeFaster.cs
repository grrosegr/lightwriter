using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypeFaster : Singleton<TypeFaster> {

	public float OnTime = 0.4f;
	public float OffTime = 0.1f;

	float switchTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float remainingFraction = Writer.Instance.LettersRemaining / (float)Writer.Instance.CurrentPhrase.Quote.Length;

		bool activated = Writer.Instance.IsFastMode && Typometer.Instance.CharsPerSecond <= 10 && remainingFraction > 0.2f;

		if (!activated) {
			myText.enabled = false;
			return;
		}

		if (Time.time > switchTime) {
			if (myText.enabled) {
				myText.enabled = false;
				switchTime = Time.time + OffTime;
			} else {
				myText.enabled = true;
				switchTime = Time.time + OnTime;
			}
		}
	
	}
}
