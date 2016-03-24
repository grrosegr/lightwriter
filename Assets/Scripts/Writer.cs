using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Writer : Singleton<Writer> {

	// Constants
	const int MaxIncorrectKeysToGuess = 20;
	const int MinIncorrectKeysToGuess = 10;
	const float InitialHintsRatio = .6f;
	const float NextLevelChange = 1.0f;

	public bool GlobalReplace;
	public bool AutoReveal = true;
	public float RevealRate = 2.0f;

	public GameObject FallingLetter;

	// Variables
	string desiredWord;

	string wordMask = "";

	int index = 0;

	bool finishTimerStarted;
	float finishTime;

	float nextRevealTime;
	bool stopped = false;

	int correctLetters;
	int totalLetters;

	public float GetAccuracy() {
		if (totalLetters == 0)
			return 0;

		return correctLetters / (float)totalLetters;
	}

//	GameObject Canvas;

	// inclusive bounds
//	int incorrectKeysRemaining = MaxIncorrectKeysToGuess;

	void ResetIncorrectKeys() {
		// +1 for inclusiveness
//		incorrectKeysRemaining = Random.Range(MinIncorrectKeysToGuess, MaxIncorrectKeysToGuess + 1);
	}

	void ResetGame() {
		desiredWord = PhraseSelector.Instance.GetNextPhrase();
		finishTimerStarted = false;
		index = 0;
		RegenMask();
		Redraw();
		ResetIncorrectKeys();
		RevealLetters(Mathf.FloorToInt(wordMask.Count(x => x == '_') * InitialHintsRatio));
		nextRevealTime = Time.time + RevealRate;
	}

	public void Stop() {
		stopped = true;
		Redraw();
	}

	void Awake() {
//		Canvas = GameObject.Find("Canvas");
	}
		
	void Start () {
		ResetGame();
	}

	void RegenMask() {
		wordMask = new string(desiredWord.Select<char,char>(nonspaceToUnderscore).ToArray());
	}

	void RevealMatchingLetters(char c) {
		char[] mask = wordMask.ToCharArray();

		// REQUIRES: mask[index] == '_'
		if (mask[index] != '_')
			Debug.LogWarning("Mask at " + index + " is not _, but " + mask[index]);

		bool found = false;

		for (int i = index; i < wordMask.Length; i++) {
			if (mask[i] == '_' && char.ToLower(desiredWord[i]) == char.ToLower(c)) {
				mask[i] = desiredWord[i];
				Score.Instance.Increment(1);
				found = true;
				if (!GlobalReplace)
					break;
			}
		}

		if (found)
			correctLetters += 1;
		totalLetters += 1;

//		if (found)
			myAudio.PlayOneShot(AssetHolder.Instance.Keypress);

		wordMask = new string(mask);

		SkipIndexToNextBlank();
		Redraw();
	}

	void RevealLetters(int num) {
		char[] mask = wordMask.ToCharArray();
		int numUnrevealed = mask.Skip(index).Count(x => x == '_');
		num = Mathf.Min(numUnrevealed, num);

		if (index >= mask.Length) {
			Debug.LogWarning("Cannot reveal letters, at end");
			return;
		}

		// REQUIRES: mask[index] == '_'
		if (mask[index] != '_')
			Debug.LogWarning("Mask at " + index + " is not _, but " + mask[index]);

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
//				Debug.LogWarning("Hit end of letters! Looking for " + indexToReveal + ", made it to " + unrevealedIndex);	
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
		if (stopped) {
			myText.text = "Game Over!";
			return;
		}

		string prefix = desiredWord.Substring(0, index);

		string newMask = wordMask;

		string suffix = "_";//"<color=green>_</color>";
		if (index >= newMask.Length)
			suffix = newMask.Substring(index);
		else
			suffix += newMask.Substring(index + 1);

		string result = prefix + suffix;

		if (index == desiredWord.Length) {
			result = "<color=green>" + result + "</color>";
		}

		myText.text = result;
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
		if (stopped)
			return;

		if (index >= desiredWord.Length || finishTimerStarted) {
			
			if (finishTimerStarted) {
				if (Time.time > finishTime) {
					ResetGame();
				}
			} else {
				finishTime = Time.time + NextLevelChange;
				finishTimerStarted = true;
				myAudio.PlayOneShot(AssetHolder.Instance.Win);

				Score.Instance.Increment(500);
				Countdown.Instance.AddBonusTime(Settings.Instance.BonusTime);
			}
		}			

		if (Input.GetKeyDown(KeyCode.LeftShift)) 
			RevealLetters(10);

		if (Input.GetKeyDown(KeyCode.RightShift))
			Debug.Log(desiredWord);

		if (AutoReveal && Time.time > nextRevealTime) {
			RevealLetters(1);
			nextRevealTime = Time.time + RevealRate;
		}


		if (Input.inputString != "" && Input.anyKeyDown) {
			foreach (char c in Input.inputString) {
				if (!char.IsLetter(c))
					continue;
				
				if (index >= desiredWord.Length) {
					index = 0;
					return;
				}

				RevealMatchingLetters(c);
				
//				if (char.ToLower(c) == char.ToLower(desiredWord[index])) {
//					IncrementLetter();
//				} else {
//					incorrectKeysRemaining -= 1;
//					if (incorrectKeysRemaining <= 0) {
//						IncrementLetter();
//						ResetIncorrectKeys();
//					}
//				}
//
//				Redraw();
			}



		}
			
	
	}
}
