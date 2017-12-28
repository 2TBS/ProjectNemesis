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
	public Player owner; //Who is pressing the button?

	// Use this for initialization
	void Start () {
		//followMouseSprite.sprite = towerImage.sprite;
		followMouseSprite.transform.position = new Vector3(100000000, 0, 0);
		placing = false;
		owner = GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
			mouseAction();
		else if (placing)
			followMouseSprite.transform.position = Input.mousePosition;
		
		foreach(GridTile tile in gridObj.gridArray) tile.GetComponent<Image>().color = (!placing) ? Color.clear : (tile.placeable) ? new Color (0f, 1f, 0f, 0.3f) : new Color (1f, 0f, 0f, 0.3f);
		
	}

	///Fires whenever mouse button is clicked. 
	void mouseAction () {

		
		if(placing) {
			
			gridObj.GetGridTile(owner).CreateObject(towerPrefab);
			followMouseSprite.sprite = blankImage;
			placing = !placing;
			
		}
	}

	///Add to OnClickEvent for TowerCreateButton
	public void clickAction () {
		placing = !placing;
		followMouseSprite.sprite = (placing) ? towerImage.sprite : blankImage;
	}
}
