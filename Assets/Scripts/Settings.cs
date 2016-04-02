using UnityEngine;
using System.Collections;

public class Settings : Singleton<Settings> {

	public int BonusTime = 10;

	public int QuotesPerGame = 10;

	public bool LongestQuotesOnly;
}
