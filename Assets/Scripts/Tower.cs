﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : GridObject {

	public Transform theTower;
	public GameObject projectile;
	public bool shooting = false;
	public const float MIN_DISTANCE = 3, //how close projectile must be to minion in order to register as hit
						PROJ_SPEED = 5,  //projectile speed in units per frame 
						FIRING_RANGE = 3, //radius that the tower will fire into
						RANGE_SCALE_MULT = 1/250f; //amount to scale the Range circle image

	protected GameObject currProj;
	public Minion closestMinion;
	public Image range;

	// Use this for initialization
	new void Start () {
		GetClosestMinion();
		range.transform.localScale = new Vector2(FIRING_RANGE, FIRING_RANGE) * RANGE_SCALE_MULT * ownerTile.theGrid.tileSize;
	}
	
	// Update is called once per frame
	new void Update () {

		base.Update();

		if(!shooting)
			Shoot();
		if(shooting)
			MoveProjectile();
		
		range.enabled = lClick;

	}

	public void GetClosestMinion() {
		foreach(GameObject minion in GameObject.FindGameObjectsWithTag("Minion")) {
			if((theTower.position - minion.transform.position).magnitude < FIRING_RANGE * ownerTile.theGrid.tileSize)
					closestMinion = minion.GetComponent<Minion>();
			else 
				closestMinion = null;
		}
	}

	public void Shoot() {
			GetClosestMinion();

			if(closestMinion != null) {
				currProj = GameObject.Instantiate(projectile, theTower.position, Quaternion.identity);
				currProj.transform.SetParent(GameObject.Find("The Map").GetComponent<Transform> ());

				shooting = true;
			}
		}

	public void MoveProjectile() {
		try {
			//Debug.Log("Projectile destroyed");
			if((currProj.transform.position - closestMinion.transform.position).magnitude < MIN_DISTANCE) {
				
				Destroy(currProj);
				GetClosestMinion();
				shooting = false;
			} else {
				currProj.transform.position = Vector3.MoveTowards(currProj.transform.position, closestMinion.transform.position, 2f);
			}
		} catch (MissingReferenceException) { //Target Not Found (probably destroyed)
			GameObject.Destroy(currProj);
			shooting = false;
		}
		
	}

}
