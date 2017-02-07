using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour {

	Transform trans;
	CharacterController controller;
	MinionState currState;

	const float SPEED = 0.5f;


	///Someone PLEASE make an animation controller with this 
	public enum MinionState {
		Stopped, Forward, ToTower, Fighting
	}

	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform> ();
		controller = GetComponent<CharacterController> ();
		currState = MinionState.Forward;
	}
	
	// Update is called once per frame
	void Update () {
		if(currState == MinionState.Forward)
			trans.Translate(new Vector3(0f, SPEED, 0f));
	}

	void OnTriggerEnter (Collider col) {
		//3 types of triggers: turn left, turn right, go to nearest tower.
        //...
		switch(col.tag) {

			case "TurnLeft":
				trans.Rotate(Vector3.left);
				break;
			case "TurnRight":
				trans.Rotate(Vector3.right);
				break;
			case "GotoTower":
				currState = MinionState.ToTower;
				break;
		}
	}
}