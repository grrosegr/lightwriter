using UnityEngine;
using System.Collections;

public class AssetHolder : Singleton<AssetHolder> {

	public AudioClip Win;
	public AudioClip[] Keypresses;
	public AudioClip Incorrect;
	public AudioClip SoftIncorrect;
	public AudioClip Lose;
	public AudioClip Tick;
}
