using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour {

	///x-coordinate of the GridTile
	public int x;
	///y-coordinate of the GridTile
	public int y;
	///What is currently inside the GridTile?
	public GridObject currObj; 
	///Owner player of this tile (who can place stuff here)
	public Player owner;
	///Grid that this GridTile belongs to
	public Grid theGrid;
	///Can owner place items in this tile?
	public bool placeable;

	// Use this for initialization
	void Start () {
		//TEMPORARY! Remove when players are fully implemented.
		owner = GameObject.Find("Player").GetComponent<Player>();

		placeable = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	///Instantiates an object into this grid.
	public void CreateObject (GameObject obj) {
		if(placeable) {
			currObj = Instantiate (obj, transform.position, Quaternion.identity).GetComponent<GridObject>();
			currObj.transform.SetParent (transform);
			placeable = false;
		}
		
	}
}
