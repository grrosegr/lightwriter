using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : Singleton<Score> {

	int score;
	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		Redraw();
	}

	void Redraw() {
		text.text = "Score: " + score.ToString();
	}

	public void Increment(int amount) {
		score += amount;
		Redraw();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
