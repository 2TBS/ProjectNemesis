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
	private GameObject towerClone;
	public bool placing; //Is the player currently placing a tower?
	public Grid gridObj; //GameObject containing the script for the Grid

	// Use this for initialization
	void Start () {
		//followMouseSprite.sprite = towerImage.sprite;
		followMouseSprite.transform.position = new Vector3(100000000, 0, 0);
		placing = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
			mouseAction();
		else if (placing)
			followMouseSprite.transform.position = Input.mousePosition;
		
		foreach(GameObject tile in gridObj.GridArray) tile.GetComponent<Image>().color = (placing) ? new Color (0f, 1f, 0f, 0.3f) : Color.clear;
		
	}

	///Fires whenever mouse button is clicked. 
	void mouseAction () {

		
		if(placing) {
			
			towerClone = Instantiate (towerPrefab, gridObj.Raycast().transform.position, Quaternion.identity) as GameObject;
			towerClone.transform.SetParent (map.transform, false);
			followMouseSprite.sprite = blankImage;
			placing = !placing;
			
		}
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
