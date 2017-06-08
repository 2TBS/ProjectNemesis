using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///Logic for the creation of towers. Attach to TowerCreateButton.
public class TowerCreate : MonoBehaviour {

	public Image towerImage; //Which tower is this?
	public Image followMouseSprite; //follows the mouse until put down
	public bool placing; //Is the player currently placing a tower?

	// Use this for initialization
	void Start () {
		followMouseSprite.sprite = towerImage.sprite;
	}
	
	// Update is called once per frame
	void Update () {
		if(placing) {
			followMouseSprite.transform.position = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {

            }
		}
	}

	///Add to OnClickEvent for TowerCreateButton
	public void clickAction () {
		placing = !placing;

		followMouseSprite.enabled = placing;
	}
}
