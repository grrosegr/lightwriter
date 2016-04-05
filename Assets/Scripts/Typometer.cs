using UnityEngine;
using System.Collections;

public class Typometer : Singleton<Typometer> {

	// TODO: smooth out movement
	readonly MovingDerivative md = new MovingDerivative(40);
	readonly MovingAverage smooth = new MovingAverage(40);

	const int MaxCPS = 40;

	public double CharsPerSecond {get; private set;}

	public double PeakCPS {get; private set;}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		md.AddSample(Input.inputString.Length, Time.deltaTime);

		CharsPerSecond = smooth.SmoothValue(md.GetDerivative());
		if (CharsPerSecond > PeakCPS) {
			PeakCPS = CharsPerSecond;
		}

		// WolframAlpha: English has average of 5.1 characters/word
//		double wpm = 60 * charsPerSec / 5.1;

//		myText.text = ((int)charsPerSec).ToString();

		float cpsNormalized = Mathf.Clamp((float)CharsPerSecond, 0f, MaxCPS);

		Vector3 rot = transform.eulerAngles;
		rot.z = - 180 * cpsNormalized / MaxCPS;
		transform.eulerAngles = rot;
	}
}
