using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour {

	CharacterController controller;
	MinionState currState;

	public const float SPEED = 0.5f;

	public int mX, mY;
	public Vars.Rotation currRotation;

	///Someone PLEASE make an animation controller with this 
	public enum MinionState {
		Stopped, Forward, ToTower, Fighting, Dead
	}

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		currState = MinionState.Forward;
		currRotation = Vars.Rotation.forward;
	}
	
	// Update is called once per frame
	void Update () {
		if(currState == MinionState.Dead) {
			Destroy(gameObject);
			return;
		} else if (currState == MinionState.Forward) {
			
			switch (currRotation) {
			case Vars.Rotation.forward:
				mX = 1;
				mY = 0;
				break;
			case Vars.Rotation.back:
				mX = -1;
				mY = 0;
				break;
			case Vars.Rotation.left:
				mX = 0;
				mY = -1;
				break;
			case Vars.Rotation.right:
				mX = 0;
				mY = 1;
				break;
			} 
		} 
			transform.Translate(new Vector2(mX,mY));
	}

	void OnTriggerEnter2D (Collider2D col) {
		//3 types of triggers: turn left, turn right, go to nearest tower.
        //...

		Debug.Log("Collide");

		switch(col.tag) {

			case "TurnLeft":
				currRotation = (Vars.Rotation)((int)(currRotation) + 1);
				break;
			case "TurnRight":
				currRotation = (Vars.Rotation)((int)(currRotation) - 1);
				break;
			case "GotoTower":
				currState = MinionState.ToTower;
				break;
			case "Destroy":
				currState = MinionState.Dead;
				break;
		}
	}
}