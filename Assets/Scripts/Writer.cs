using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System.Text;

public class Writer : Singleton<Writer> {

	const string ColorFormatString = "<color={0}>{1}</color>";

	// Events

	public event System.Action NewLevel;

	private void OnNewLevel() {
		if (NewLevel != null) {
			NewLevel();
		}
	}

	public event System.Action<char> Mistake;

	private void OnMistake(char incorrectChar) {
		if (Mistake != null) {
			Mistake(incorrectChar);
		}
	}

	// Constants
	const int MaxIncorrectKeysToGuess = 20;
	const int MinIncorrectKeysToGuess = 10;
	const float NextLevelChange = 0.5f;

	public bool GlobalReplace;
	public bool AutoReveal = true;
	public float RevealRate = 2.0f;

	public GameObject FallingLetter;

	// Variables
	string desiredWord;
	public Phrase CurrentPhrase {get; private set;}

	string wordMask = "";

	bool finishTimerStarted;
	float finishTime;

	float nextRevealTime;
	bool stopped = false;

	int correctLetters;
	int totalLetters;

	public int LettersRemaining {get; private set;}

	public float GetAccuracy() {
		if (totalLetters == 0)
			return 0;

		return correctLetters / (float)totalLetters;
	}

	public bool IsFastMode {get; private set;}

	bool speedTutorialShown;
	bool accuracyTutorialShown;

	bool[] originallyHidden = new bool[1];

	void LoadNewLevel() {
		pressSpace.enabled = false;
		quoteFailed = false;
		lastFilledIndex = -1;

		if (Random.value < Settings.Instance.SwitchProbability)
			IsFastMode = !IsFastMode;

		if (PhraseSelector.Instance.PhraseNumber == 0)
			IsFastMode = false; // first quote is always slow

		if (IsFastMode) {
			speedInfo.SetActive(!speedTutorialShown);
			accuracyInfo.SetActive(false);

			CurrentPhrase = PhraseSelector.Instance.GetNextLongPhrase();
			float maxTime = CurrentPhrase.Quote.Length * Settings.Instance.TimePerChar;
			maxTime += Settings.Instance.BonusTime;
			maxTime = Mathf.Max(Settings.Instance.MinTime, maxTime);
			maxTime = Mathf.Min(Settings.Instance.MaxTime, maxTime);
			if (!speedTutorialShown)
				maxTime += 10.0f; // Newbie Bonus

			Countdown.Instance.MaxTime = Mathf.RoundToInt(maxTime);
			Countdown.Instance.Activate();
			LivesCounter.Instance.Deactivate();
			slowMusic.Pause();
			fastMusic.Play();

			speedTutorialShown = true;

		} else {
			speedInfo.SetActive(false);
			accuracyInfo.SetActive(!accuracyTutorialShown);
			accuracyTutorialShown = true;

			CurrentPhrase = PhraseSelector.Instance.GetNextShortPhrase();
			Countdown.Instance.Deactivate();
			LivesCounter.Instance.Activate();

			slowMusic.Play();
			fastMusic.Pause();
		}

//		CurrentPhrase = PhraseSelector.Instance.GetNextPhrase();
		desiredWord = CurrentPhrase.Quote;
		sourceText.text = CurrentPhrase.Source;
		finishTimerStarted = false;
		RegenMask();
		Redraw();
		LettersRemaining = wordMask.Count(x => x == '_');
		float hintRatio = IsFastMode ? Settings.Instance.SpeedHintsRatio : Settings.Instance.InitialHintsRatio;
		RevealLetters(Mathf.FloorToInt(LettersRemaining * hintRatio));
		originallyHidden = wordMask.Select(x => x == '_').ToArray();
		nextRevealTime = Time.time + RevealRate;
		OnNewLevel();
	}

	public void Stop() {
		stopped = true;
		Redraw();
	}


	AudioSource slowMusic;
	AudioSource fastMusic;

	GameObject speedInfo;
	GameObject accuracyInfo;

	void Awake() {
		sourceText = GameObject.Find("Source").GetComponent<Text>();
		slowMusic = GameObject.Find("SlowMusic").GetComponent<AudioSource>();
		fastMusic = GameObject.Find("FastMusic").GetComponent<AudioSource>();
		speedInfo = GameObject.Find("Info").transform.Find("SpeedInfo").gameObject;
		accuracyInfo = GameObject.Find("Info").transform.Find("AccuracyInfo").gameObject;
	}

	Text sourceText;
	Text pressSpace;
		
	void Start () {
		pressSpace = GameObject.Find("PressSpace").GetComponent<Text>();
		LoadNewLevel();
	}

	void RegenMask() {
		wordMask = new string(desiredWord.Select<char,char>(nonspaceToUnderscore).ToArray());
	}

	float nextSound;
	int soundIndex;
	AudioClip[] soundsShuffled;

	void PlayKeypress() {
		if (Time.time < nextSound)
			return;

		nextSound = Time.time + Random.Range(0.02f, 0.04f);

		if (soundsShuffled == null || soundIndex == 0)
			soundsShuffled = AssetHolder.Instance.Keypresses.AsRandom().ToArray();
		
//		int soundIndex = Random.Range(0, AssetHolder.Instance.Keypresses.Length);
		var sound = soundsShuffled[soundIndex];		
		myAudio.PlayOneShot(sound);
		soundIndex = (soundIndex + 1) % AssetHolder.Instance.Keypresses.Length;
	}

	int lastFilledIndex = -1;

	void RevealMatchingLetters(char c) {
		if (LettersRemaining <= 0)
			return;

		char[] mask = wordMask.ToCharArray();

		bool found = false;

		for (int i = 0; i < wordMask.Length; i++) {
			if (mask[i] == '_' && char.ToLower(desiredWord[i]) == char.ToLower(c)) {
				mask[i] = desiredWord[i];
				LettersRemaining -= 1;
				lastFilledIndex = i;
				found = true;
				if (!GlobalReplace)
					break;
			}
		}

		if (found)
			correctLetters += 1;
		else {
			OnMistake(c);
		}

		totalLetters += 1;

		wordMask = new string(mask);

		Redraw();
	}

	void RevealLetters(int num) {
		char[] mask = wordMask.ToCharArray();
		num = Mathf.Min(LettersRemaining, num);

		// reveal num letters (or however many are left)
		for (int i = 0; i < num; i++) {

			// reveals indexToReveal (out of those that are unrevealed so far)
			int indexToReveal = Random.Range(0, LettersRemaining);
			int j = 0;
			int unrevealedIndex = 0;

			while (j < mask.Length && (unrevealedIndex < indexToReveal || mask[j] != '_')) {

				if (mask[j] == '_')
					unrevealedIndex += 1;

				j += 1;
			}

			if (j == mask.Length) {
//				Debug.LogWarning("Hit end of letters! Looking for " + indexToReveal + ", made it to " + unrevealedIndex);	
			} else if (unrevealedIndex == indexToReveal) {
				mask[j] = desiredWord[j];
				LettersRemaining -= 1;
			}
		}

		wordMask = new string(mask);
		Redraw();
	}

	bool quoteFailed;
	float continueAfterFailingTime;
	public void FailQuote() {
		pressSpace.enabled = true;
		quoteFailed = true;
		continueAfterFailingTime = Time.time + Settings.Instance.LevelChangeWait;
		myAudio.PlayOneShot(AssetHolder.Instance.Lose);
		Redraw();
		fastMusic.Pause();
		slowMusic.Pause();
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

		if (Settings.Instance.BrowseMode) {
			myText.text = desiredWord;
			return;
		}

		string result = wordMask;

		if (quoteFailed) {
			myText.text = "<color=red>" + desiredWord + "</color>";

			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < result.Length; i++) {
				if (result[i] == '_') {
					builder.AppendFormat("<color={0}>{1}</color>", "red", desiredWord[i]);
				} else {
					builder.Append(result[i]);
				}
			}
			result = builder.ToString();

		} else if (LettersRemaining <= 0) {
			result = "<color=green>" + result + "</color>";
		} else {

			HashSet<char> missingChars = new HashSet<char>();

			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < result.Length; i++) {
				if (result[i] != '_') {
					if (i == lastFilledIndex && Settings.Instance.ColorLastFilled) 
						builder.AppendFormat(ColorFormatString, Settings.Instance.LastFilledColor, result[i]);
					else if (!IsFastMode && Settings.Instance.ColorAllFilled && i < originallyHidden.Length && originallyHidden[i]) {
						builder.AppendFormat(ColorFormatString, Settings.Instance.FilledColor, result[i]);
					} else {
						builder.Append(result[i]);
					}

					continue;
				} 

				char canonical = char.ToLowerInvariant(desiredWord[i]);
				string color = "";

				if (!Settings.Instance.HighlightFillable || missingChars.Contains(canonical)) {
					color = Settings.Instance.UnfillableColor;
				} else {
					color = Settings.Instance.FillableColor;
					missingChars.Add(canonical);
				}
				builder.Append(string.Format("<color={0}>_</color>", color));
			}
			result = builder.ToString();
		}

		myText.text = result;
	}
	
	// Update is called once per frame
	void Update () {
		if (stopped)
			return;

		if (quoteFailed) {
			if (Input.GetKeyDown(KeyCode.Space) && Time.time > continueAfterFailingTime) {
				LoadNewLevel();
			}
			return;
		}

		if (LettersRemaining <= 0 || finishTimerStarted) {
			
			if (finishTimerStarted) {

				if (Time.time > finishTime && !pressSpace.enabled)
					pressSpace.enabled = true;

				if (Input.GetKeyDown(KeyCode.Space) && Time.time > finishTime)
					LoadNewLevel();
			} else {
				finishTime = Time.time + Settings.Instance.LevelChangeWait;
				finishTimerStarted = true;
				Countdown.Instance.Paused = true;
				myAudio.PlayOneShot(AssetHolder.Instance.Win);
				
				fastMusic.Pause();
				slowMusic.Pause();
			}
		}			

		if (Input.GetKeyDown(KeyCode.F1)) 
			RevealLetters(10);

		if (Input.GetKeyDown(KeyCode.F12))
			LoadNewLevel();

		if (AutoReveal && Time.time > nextRevealTime) {
			RevealLetters(1);
			nextRevealTime = Time.time + RevealRate;
		}


		if (Input.inputString != "" && Input.anyKeyDown) {
			PlayKeypress();

			foreach (char c in Input.inputString) {
				if (!char.IsLetter(c))
					continue;

				RevealMatchingLetters(c);
			}
		}
	}
}
