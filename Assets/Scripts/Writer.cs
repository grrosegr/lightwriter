using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Writer : MyMonoBehaviour {

	// Constants
	const int MaxIncorrectKeysToGuess = 20;
	const int MinIncorrectKeysToGuess = 10;
	const float InitialHintsRatio = .2f;
	const float RevealRate = 2.0f;
	const float NextLevelChange = 1.0f;

	readonly string[] Phrases = {
		"It is a capital mistake to theorize before one has data.", 
		// Insensibly one begins to twist facts to suit theories, instead of theories to suit facts.";
		"Make America Great Again!",
		"You see, but you do not observe.",
		"Sometimes it's the very people who no one imagines anything of who do the things no one can imagine.",
		"You can torture us, and bomb us, or burn our districts to the ground. But do you see that? Fire is catching... If we burn... you burn with us!",
		"It is the things we love most that destroy us.",
		"Greed, for lack of a better word, is good.",
		"I am not a number, I am a free man.",
		"In the face of overwhelming odds, I'm left with only one option, I'm gonna have to science the shit out of this.",
		"You're waiting for a train, a train that will take you far away. You know where you hope this train will take you, but you don't know for sure. But it doesn't matter. How can it not matter to you where that train will take you?",
		"Gentleman, you had my curiosity, now you have my attention.",
		"A million dollars isn't cool. You know what's cool? A billion dollars.",
		"Congratulations San Francisco, you've ruined pizza! First the Hawaiians, and now YOU!",
		"May the odds be ever in your favor.",
		"Carpe diem.",
		"One should use common words to say uncommon things.",
		"Perfection is achieved, not when there is nothing more to add, but when there is nothing left to take away.",
		"Be the change that you wish to see in the world.",
		"A delayed game is eventually good, but a rushed game is forever bad.",
		"Necessity is the mother of invention.",
		"I would be the least among men with dreams and the desire to fulfil them, rather than the greatest with no dreams and no desires.",
		"All men dream: but not equally. Those who dream by night in the dusty recesses of their minds wake in the day to find that it was vanity:" +
			" but the dreamers of the day are dangerous men, for they may act their dreams with open eyes, to make it possible."
	};

	public GameObject FallingLetter;

	string[] PhrasesShuffled;
	int phraseIndex = 0;

	// Variables
	string desiredWord;

	string wordMask = "";

	int index = 0;

	bool finishTimerStarted;
	float finishTime;

	float nextRevealTime;

	GameObject Canvas;

	// inclusive bounds
	int incorrectKeysRemaining = MaxIncorrectKeysToGuess;

	void ResetIncorrectKeys() {
		// +1 for inclusiveness
		incorrectKeysRemaining = Random.Range(MinIncorrectKeysToGuess, MaxIncorrectKeysToGuess + 1);
	}

	void ResetGame() {
		desiredWord = PhrasesShuffled[phraseIndex];
		phraseIndex = (phraseIndex + 1) % PhrasesShuffled.Length;
		finishTimerStarted = false;
		index = 0;
		RegenMask();
		Redraw();
		ResetIncorrectKeys();
		RevealLetters(Mathf.FloorToInt(wordMask.Count(x => x == '_') * InitialHintsRatio));
		nextRevealTime = Time.time + RevealRate;
	}

	void Awake() {
		Canvas = GameObject.Find("Canvas");
	}
		
	void Start () {
		PhrasesShuffled = Phrases.AsRandom().ToArray();
		ResetGame();
	}

	void RegenMask() {
		wordMask = new string(desiredWord.Select<char,char>(nonspaceToUnderscore).ToArray());
	}

	void RevealMatchingLetters(char c) {
		char[] mask = wordMask.ToCharArray();

		// REQUIRES: mask[index] == '_'

		bool found = false;

		for (int i = index; i < wordMask.Length; i++) {
			if (mask[i] == '_' && char.ToLower(desiredWord[i]) == char.ToLower(c)) {
				mask[i] = desiredWord[i];
				found = true;
				break;
			}
		}

		wordMask = new string(mask);

		SkipIndexToNextBlank();
		Redraw();
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
				Debug.LogWarning("Hit end of letters! Looking for " + indexToReveal + ", made it to " + unrevealedIndex);	
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

		string newMask = wordMask;

		string suffix = "_";//"<color=green>_</color>";
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
				finishTime = Time.time + NextLevelChange;
				finishTimerStarted = true;
			}
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
			RevealLetters(5);

		if (Input.GetKeyDown(KeyCode.RightShift))
			Debug.Log(desiredWord);

		if (Time.time > nextRevealTime) {
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
