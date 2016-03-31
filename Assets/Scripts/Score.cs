using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : Singleton<Score> {

	int score;
	Text text;
	int lastCount = -1;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		Redraw();
	}

	void Redraw() {
		if (PhraseSelector.Instance.PhraseNumber == lastCount)
			return;
		
		lastCount = PhraseSelector.Instance.PhraseNumber;
		text.text = string.Format("Phrase {1} of {2}", score, PhraseSelector.Instance.PhraseNumber, PhraseSelector.Instance.PhraseCount);
	}

	public void Increment(int amount) {
		score += amount;
		Redraw();
	}
	
	// Update is called once per frame
	void Update () {
		Redraw();
	
	}
}
