using UnityEngine;
using System.Collections;

public class Countdown : Singleton<Countdown> {

	public int MaxTime = 60;
	public bool CountUp = true;

	float startTime;
	bool hasStopped = false;

	Writer writer;

	void Awake() {
		writer = FindObjectOfType<Writer>();
	}

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (CountUp) {
			int timeTaken = (int)(Time.time - startTime);
			int seconds = timeTaken % 60;
			int minutes = timeTaken / 60;

			myText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);

		} else {
			int timeLeft = Mathf.Max(0, (int)(MaxTime - (Time.time - startTime)));
			myText.text = timeLeft.ToString();

			if (!hasStopped && timeLeft == 0) {
				hasStopped = true;
				writer.Stop();
			}
		}

	}

	public void AddBonusTime(int t) {
		MaxTime += t;

	}
}
