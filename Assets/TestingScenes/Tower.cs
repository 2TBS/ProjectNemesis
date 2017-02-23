using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	public Transform theTower, closestMinion = null;
	public GameObject projectile;
	public bool shooting = false;
	protected const float MIN_DISTANCE = 2, //how close projectile must be to minion in order to register as hit
						   PROJ_SPEED = 2; //projectile speed in units per frame 


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!shooting)
			Shoot();
	}

	public void Shoot() {
		
		shooting = true;

		foreach(GameObject minion in GameObject.FindGameObjectsWithTag("minion")) {
			if((theTower.position - minion.GetComponent<Transform>().position).magnitude < closestMinion.position.magnitude || closestMinion == null)
				closestMinion = minion.GetComponent<Transform>();
		}

		GameObject currProj = GameObject.Instantiate(projectile, theTower.position, Quaternion.identity);
		currProj.transform.LookAt(closestMinion);

		if((currProj.transform.position - closestMinion.position).magnitude < MIN_DISTANCE) {
			GameObject.Destroy(currProj);
			shooting = false;
		} else {
			currProj.transform.Translate(Vector3.forward * PROJ_SPEED);
		}
	}
}
