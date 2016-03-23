using UnityEngine;
using System.Collections;

public class Countdown : MyMonoBehaviour {

	public int MaxTime = 60;
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
		int timeLeft = Mathf.Max(0, (int)(MaxTime - (Time.time - startTime)));
		myText.text = timeLeft.ToString();

		if (!hasStopped && timeLeft == 0) {
			hasStopped = true;
			writer.Stop();
		}

	}
}
