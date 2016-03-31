using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class MistakeCounter : MyMonoBehaviour {

	int mistakes = 0;

	string s = "Mistakes: {0}\n<color=#C98910>Gold: </color>\n<color=#A8A8A8>Silver: </color>\n<color=#965A38>Bronze: </color>";

	// Use this for initialization
	void Start () {
		Writer.Instance.NewLevel += OnNewLevel;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnNewLevel() {
		mistakes = 0;
		Redraw();
	}

	void Redraw() {
		myText.text = string.Format(s, mistakes);
	}
}
