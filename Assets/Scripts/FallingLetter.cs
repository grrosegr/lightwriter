using UnityEngine;
using System.Collections;

public class FallingLetter : MyMonoBehaviour {

	public float power = 500;

	// Use this for initialization
	void Start () {
		Vector2 vel = Random.insideUnitCircle.normalized * power;
		if (vel.y < 0)
			vel.y *= -1;

		myRigidBody2D.velocity = vel;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
