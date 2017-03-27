using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	public Transform theTower, closestMinion = null;
	public GameObject projectile;
	public bool shooting = false;
	public const float MIN_DISTANCE = 2, //how close projectile must be to minion in order to register as hit
						PROJ_SPEED = 5,
						FIRING_RANGE = 20; //projectile speed in units per frame 

	protected GameObject currProj;


	// Use this for initialization
	void Start () {
		GetClosestMinion();
	}
	
	// Update is called once per frame
	void Update () {
		GetClosestMinion();
		if(!shooting)
			Shoot();
		if(shooting)
			MoveProjectile();
	}

	public void GetClosestMinion() {
		foreach(GameObject minion in GameObject.FindGameObjectsWithTag("Minion")) {
				if((theTower.position - minion.GetComponent<Transform>().position).magnitude < FIRING_RANGE || closestMinion == null)
				closestMinion = minion.GetComponent<Transform>();
			}
	}
	public void Shoot() {
		


			if(closestMinion != null) {
				currProj = GameObject.Instantiate(projectile, theTower.position, Quaternion.identity);
				currProj.transform.SetParent(GameObject.Find("The Map").GetComponent<Transform> ());
				currProj.transform.LookAt(closestMinion);

				shooting = true;
			}
		}

	public void MoveProjectile() {
		if((currProj.transform.position - closestMinion.position).magnitude < MIN_DISTANCE) {
				GameObject.Destroy(currProj);
				shooting = false;
			} else {
				currProj.transform.LookAt(closestMinion);
				currProj.transform.Translate(Vector3.forward * PROJ_SPEED);
			}
	}
}
