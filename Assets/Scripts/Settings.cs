using UnityEngine;
using System.Collections;

public class Settings : Singleton<Settings> {

	public int BonusTime = 10;

	public int QuotesPerGame = 10;

	public bool LongestQuotesOnly;

	public string FillableColor = "#FFB3B3FF";

	public bool HighlightFillable = true;

	public string UnfillableColor = "#494949FF";

	public bool ColorLastFilled = true;
	public string LastFilledColor = "green";

	public bool SortQuotes = false;
	public bool NewestQuotesOnly = false;

	public float InitialHintsRatio = .6f;

	public bool BrowseMode = false;

	public bool TonyMod = false;
}
