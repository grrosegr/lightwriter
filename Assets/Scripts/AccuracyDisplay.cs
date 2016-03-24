using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AccuracyDisplay : MyMonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		myText.text = string.Format("Accuracy: {0:0.0}%", Writer.Instance.GetAccuracy() * 100);
	}
}
