using UnityEngine;
using System.Collections;

public enum GameState {Playing, GameOver}

public class GameManager : Singleton<GameManager> {

	event System.Action<GameState> _stateChanged;
	GameState _gameState;

	public static GameState GameState {
		get {
			return Instance._gameState;
		}

		set {
			Instance._gameState = value;
			if (Instance._stateChanged != null)
				Instance._stateChanged(value);
		}
	}

	public static event System.Action<GameState> StateChanged {
		add {
			Instance._stateChanged += value;
		}

		remove {
			Instance._stateChanged -= value;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
