using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LivesCounter : Singleton<LivesCounter> {

	IList<char> mistakes = new List<char>();
	int lives;

	// Use this for initialization
	void Start () {
		Writer.Instance.Mistake += OnMistake;
		Writer.Instance.NewLevel += OnNewLevel;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Draw() {
		if (Writer.Instance.CurrentPhrase != null) {
			int len = Writer.Instance.CurrentPhrase.Quote.Length;

			if (len > 500)
				lives = 8;
			else if (len > 400)
				lives = 7;
			else if (len > 300)
				lives = 6;
			else if (len > 200)
				lives = 5;
			else
				lives = 4;
		} else {
			lives = 4;
		}
		lives += 2;

		var underscores = string.Format("<color={0}>{1}</color>", Settings.Instance.UnfillableColor, new string('_', Mathf.Max(0, lives - mistakes.Count)));

		myText.text = string.Join("", mistakes.Select(x => x.ToString()).ToArray()) + underscores;
	}

	void OnMistake(char incorrect) {
		if (!myText.enabled)
			return;
		
		char canonical = char.ToUpperInvariant(incorrect);

		if (mistakes.Contains(canonical)) {
			myAudio.PlayOneShot(AssetHolder.Instance.SoftIncorrect);
			return;
		}

		myAudio.PlayOneShot(AssetHolder.Instance.Incorrect, 0.2f);
		mistakes.Add(canonical);
		Draw();

		if (mistakes.Count >= lives) {
			Writer.Instance.FailQuote();
			return;
		}
		

	}

	void OnNewLevel() {
		mistakes.Clear();
		Draw();
	}

	public void Deactivate() {
		myText.enabled = false;
	}

	public void Activate() {
		myText.enabled = true;
		OnNewLevel();
	}
}
