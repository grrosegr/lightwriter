using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Writer : MyMonoBehaviour {

	// Constants
	const int MaxIncorrectKeysToGuess = 50;
	const int MinIncorrectKeysToGuess = 30;
	const string desiredWord = //"I like potatoes.";
		"It is a capital mistake to theorize before one has data." ;// Insensibly one begins to twist facts to suit theories, instead of theories to suit facts.";
		//"spaghetti tastes really good";
	const float InitialHintsRatio = .2f;
	const float RevealRate = 2.0f;

	// Variables
	string wordMask = "";

	int index = 0;

	bool finishTimerStarted;
	float finishTime;

	float nextRevealTime;

	// inclusive bounds
	int incorrectKeysRemaining = MaxIncorrectKeysToGuess;

	void ResetIncorrectKeys() {
		// +1 for inclusiveness
		incorrectKeysRemaining = Random.Range(MinIncorrectKeysToGuess, MaxIncorrectKeysToGuess + 1);
	}

	void ResetGame() {
		finishTimerStarted = false;
		index = 0;
		RegenMask();
		Redraw();
		ResetIncorrectKeys();
		RevealLetters(Mathf.FloorToInt(wordMask.Count(x => x == '_') * InitialHintsRatio));
		nextRevealTime = Time.time + RevealRate;
	}
		
	void Start () {
		ResetGame();
	}

	void RegenMask() {
		wordMask = new string(desiredWord.Select<char,char>(nonspaceToUnderscore).ToArray());
	}

	void RevealLetters(int num) {
		char[] mask = wordMask.ToCharArray();
		int numUnrevealed = mask.Skip(index).Count(x => x == '_');
		num = Mathf.Min(numUnrevealed, num);

		// REQUIRES: mask[index] == '_'

		// reveal num letters (or however many are left)
		for (int i = 0; i < num; i++) {

			// reveals indexToReveal (out of those that are unrevealed so far)
			int indexToReveal = Random.Range(0, numUnrevealed);
			int j = index;
			int unrevealedIndex = 0;

			while (j < mask.Length && (unrevealedIndex < indexToReveal || mask[j] != '_')) {

				if (mask[j] == '_')
					unrevealedIndex += 1;

				j += 1;
			}

			if (j == mask.Length) {
				Debug.LogWarning("Hit end of letters! Looking for " + unrevealedIndex);	
			} else if (unrevealedIndex == indexToReveal)
				mask[j] = desiredWord[j];
		}

		wordMask = new string(mask);

		SkipIndexToNextBlank();
		Redraw();
	}

	char nonspaceToUnderscore(char x) {
		if (!char.IsLetter(x))
			return x;
		else
			return '_';
	}

	void Redraw() {
		string prefix = desiredWord.Substring(0, index);

		char[] newMaskArr = wordMask.ToCharArray();
//		newMaskArr[index] = '█';
		string newMask = new string(newMaskArr);

		string suffix = "<color=green>_</color>";
		if (index >= newMask.Length)
			suffix = newMask.Substring(index);
		else
			suffix += newMask.Substring(index + 1);

		myText.text = prefix + suffix;
	}

	void SkipIndexToNextBlank() {
		// skip non-letter characters
		while (index < desiredWord.Length && wordMask[index] != '_') {
			index += 1;
		}
	}

	void IncrementLetter() {
		index += 1;

		SkipIndexToNextBlank();
	}
	
	// Update is called once per frame
	void Update () {

		if (index >= desiredWord.Length) {
			
			if (finishTimerStarted) {
				if (Time.time > finishTime) {
					ResetGame();
				}
			} else {
				finishTime = Time.time + 0.5f;
				finishTimerStarted = true;
			}
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
			RevealLetters(1);

		if (Time.time > nextRevealTime) {
			RevealLetters(1);
			nextRevealTime = Time.time + RevealRate;
		}


		if (Input.inputString != "" && Input.anyKeyDown) {
			foreach (char c in Input.inputString) {
				if (index >= desiredWord.Length) {
					index = 0;
					return;
				}
				
				if (char.ToLower(c) == char.ToLower(desiredWord[index])) {
					IncrementLetter();
				} else {
					incorrectKeysRemaining -= 1;
					if (incorrectKeysRemaining <= 0) {
						IncrementLetter();
						ResetIncorrectKeys();
					}
				}

				Redraw();
			}



		}
			
	
	}
}
