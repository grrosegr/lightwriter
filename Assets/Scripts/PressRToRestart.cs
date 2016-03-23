using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PressRToRestart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	float keyDownStart;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Backslash))
			keyDownStart = Time.time;
		else if (Input.GetKey(KeyCode.Backslash) && (Time.time - keyDownStart > 1.0f)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
			

//		print(Input.GetKey(KeyCode.R) + " " + Input.GetKey(KeyCode.Backslash));
//
//		if ((Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.Backslash))
//			|| (Input.GetKey(KeyCode.R) && Input.GetKeyDown(KeyCode.Backslash)))
//			Application.LoadLevel(Application.loadedLevel);
	}
}
