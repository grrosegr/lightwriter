using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MyMonoBehaviour : MonoBehaviour {
	
	// TODO; save these all to private variables lazily

	private Text _myText;
	protected Text myText {
		get {
			if (_myText == null) _myText = GetComponent<Text>();
			return _myText;
		}
	}

	private SpriteRenderer _spriteRenderer;
	protected SpriteRenderer mySpriteRenderer {
		get {
			if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
			return _spriteRenderer;
		}
	}

	protected SpriteRenderer sr {
		get {
			return mySpriteRenderer;
		}
	}
	
	protected Sprite sprite {
		get {
			return mySpriteRenderer.sprite;
		}
		
		set {
			mySpriteRenderer.sprite = value;
		}
	}
	
	new protected Renderer renderer {
		get {
			return GetComponent<Renderer>();
		}
	}
	
	public Color color {
		get {
			return mySpriteRenderer.color;
		}
		
		set {
			mySpriteRenderer.color = value;
		}
	}
	
	protected Animator anim {
		get {
			return GetComponent<Animator>();
		}
	}
	
	protected AudioSource myAudio {
		get {
			return GetComponent<AudioSource>();
		}
	}
	
	private Collider2D _collider2D;
	protected Collider2D myCollider2D {
		get {
			if (_collider2D == null) _collider2D = GetComponent<Collider2D>();
			return _collider2D;
		}
	}
	
	private Rigidbody2D _rigidBody2D;
	protected Rigidbody2D myRigidBody2D {
		get {
			if (_rigidBody2D == null) _rigidBody2D = GetComponent<Rigidbody2D>();
			return _rigidBody2D;
		}
	}
}