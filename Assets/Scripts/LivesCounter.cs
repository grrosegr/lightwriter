using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LivesCounter : MyMonoBehaviour {

	IList<char> mistakes;
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
				lives = 6;
			else if (len > 300)
				lives = 5;
			else if (len > 200)
				lives = 4;
			else if (len > 100)
				lives = 3;
			else
				lives = 2;
		} else {
			lives = 4;
		}

//		Mathf.Max(

//		myText.text = 
	}

	void OnMistake(char incorrect) {
//		mistakes
	}

	void OnNewLevel() {

	}
}
