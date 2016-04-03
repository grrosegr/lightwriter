using UnityEngine;
using System.Collections;

public class Countdown : Singleton<Countdown> {

	public int MaxTime = 60;
	public bool CountUp = true;

	float startTime;
	bool hasStopped = false;

	public bool Paused;

	Writer writer;

	void Awake() {
		writer = FindObjectOfType<Writer>();
	}

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}

	public void Deactivate() {
		myText.enabled = false;
	}

	public void Activate() {
		myText.enabled = true;
		Paused = false;
		startTime = Time.time;
	}

	int lastTime;
	
	// Update is called once per frame
	void Update () {
		if (!myText.enabled)
			return;

		if (Paused)
			return;

		if (CountUp) {
			int timeTaken = (int)(Time.time - startTime);
			int seconds = timeTaken % 60;
			int minutes = timeTaken / 60;

			myText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);

		} else {
			int timeLeft = Mathf.Max(0, (int)(MaxTime - (Time.time - startTime)));
			if (Settings.Instance.Tick && timeLeft != lastTime) {
				lastTime = timeLeft;
				myAudio.PlayOneShot(AssetHolder.Instance.Tick, 0.4f);
			}

			myText.text = timeLeft.ToString();

			if (timeLeft <= 0) {
				Writer.Instance.FailQuote();
				Paused = true;
			}

//			if (!hasStopped && timeLeft == 0) {
//				hasStopped = true;
//				writer.Stop();
//			}
		}

	}
}
