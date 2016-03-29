using UnityEngine;
using System.Collections;

public struct Distraction {
	public bool HorizionalFlip;
	public bool VerticalFlip;
	public bool HFlipLoop;
	public float HFlipRate;

	public bool VFlipLoop;
	public float VFlipRate;
}

public class Distractinator : Singleton<Distractinator> {

	public Distraction currentDistraction;

	public readonly Distraction hard = new Distraction() {HFlipLoop = true, HFlipRate = 0.75f, VFlipLoop = true, VFlipRate = 0.66f};
	public readonly Distraction medium = new Distraction() {VerticalFlip = true};
	public readonly Distraction easy = new Distraction();

	RectTransform rt;

	void Awake() {
		rt = GetComponent<RectTransform>();
	}

	void Start () {
		currentDistraction = easy;
		PhraseSelector.Instance.NewPhrase += OnNewPhrase;
	}
	
	void Redraw() {
		bool hflip = currentDistraction.HorizionalFlip;
		bool vflip = currentDistraction.VerticalFlip;

		if (currentDistraction.HFlipLoop && Mathf.FloorToInt(Time.time / currentDistraction.HFlipRate) % 2 == 0)
			hflip = !hflip;

		if (currentDistraction.VFlipLoop && Mathf.FloorToInt(Time.time / currentDistraction.VFlipRate) % 2 == 0)
			vflip = !vflip;

		rt.localScale = new Vector3(hflip ? -1 : 1, vflip ? -1 : 1, 1);
	}

	void OnNewPhrase() {
//		int pnum = PhraseSelector.Instance.PhraseNumber;
//		int pct = PhraseSelector.Instance.PhraseCount;
//
//		if (pnum % 2 == 0 || pnum < Mathf.RoundToInt(pct * .5f)) {
//			currentDistraction = easy;
//		} else if (pnum < Mathf.RoundToInt(pct * .75f)) {
//			currentDistraction = medium;
//		} else {
//			currentDistraction = hard;
//		}

	}

	void Update () {
		Redraw();
	}
}
