using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	public Transform theTower;
	public GameObject projectile;
	public bool shooting = false;
	public const float MIN_DISTANCE = 2, //how close projectile must be to minion in order to register as hit
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
				if(DistanceFrom(minion) < FIRING_RANGE)
					closestMinion = minion.GetComponent<Minion>();
			}
	}

	public void Shoot() {
			GetClosestMinion();

			if(closestMinion != null) {
				currProj = GameObject.Instantiate(projectile, theTower.position, Quaternion.identity);
				currProj.transform.SetParent(GameObject.Find("The Map").GetComponent<Transform> ());
				currProj.transform.LookAt(closestMinion.transform);

				shooting = true;
			}
		}

	public void MoveProjectile() {
		try {
			
			if(DistanceFrom(closestMinion.gameObject) < MIN_DISTANCE) {
				GameObject.Destroy(currProj);
				GetClosestMinion();
				shooting = false;
			} else {
				currProj.transform.LookAt(closestMinion.transform);
				currProj.transform.Translate(Vector3.forward * PROJ_SPEED);
			}
		} catch (MissingReferenceException ex) { //Target Not Found (probably destroyed)
			GameObject.Destroy(currProj);
			shooting = false;
		}
		
	}

	///Returns the magnitude of the vector drawn between this Tower and otherObj.
	protected float DistanceFrom (GameObject otherObj) {
		return (theTower.position - otherObj.transform.position).magnitude;
	}
}
