using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : GridObject {

	public Transform theTower;
	public GameObject projectile;
	public bool shooting = false;
	public const float MIN_DISTANCE = 3, //how close projectile must be to minion in order to register as hit
						PROJ_SPEED = 5,  //projectile speed in units per frame 
						FIRING_RANGE = 50; //radius that the tower will fire into

	protected GameObject currProj;
	public Minion closestMinion;


	// Use this for initialization
	void Start () {
		GetClosestMinion();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!shooting)
			Shoot();
		if(shooting)
			MoveProjectile();


	}

	public void GetClosestMinion() {
		foreach(GameObject minion in GameObject.FindGameObjectsWithTag("Minion")) {
			if((theTower.position - minion.transform.position).magnitude < FIRING_RANGE)
					closestMinion = minion.GetComponent<Minion>();
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
