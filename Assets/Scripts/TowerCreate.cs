using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///Logic for the creation of towers. Attach to TowerCreateButton.
public class TowerCreate : MonoBehaviour {

	public Image towerImage; //Which tower is this?
	public Image followMouseSprite; //follows the mouse until put down
	public Sprite blankImage;
	public GameObject towerPrefab;
	public GameObject map;
	public GameObject towerClone;
	public bool placing; //Is the player currently placing a tower?

	// Use this for initialization
	void Start () {
		//followMouseSprite.sprite = towerImage.sprite;
		followMouseSprite.transform.position = new Vector3(100000000, 0, 0); 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && placing) {
			towerClone = Instantiate (towerPrefab, new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), Quaternion.identity) as GameObject;
			//towerClone.transform.parent = map.transform;
			towerClone.transform.SetParent (map.transform, false);
			followMouseSprite.sprite = blankImage;
			placing = !placing;
		} else if (placing) {
			followMouseSprite.transform.position = Input.mousePosition;
		}
			//if(Input.GetMouseButtonDown(0))
	}

	///Add to OnClickEvent for TowerCreateButton
	public void clickAction () {

		if (!placing) {
			followMouseSprite.sprite = towerImage.sprite;
		} else if (placing) {
			followMouseSprite.sprite = blankImage;
		}
		placing = !placing;

	}
}
