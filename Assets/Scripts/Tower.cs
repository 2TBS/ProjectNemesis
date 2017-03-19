using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	public RectTransform theTower, closestMinion = null;
	public GameObject projectile;
	public bool shooting = false;
	public const float MIN_DISTANCE = 2, //how close projectile must be to minion in order to register as hit
						PROJ_SPEED = 2; //projectile speed in units per frame 

	protected GameObject currProj;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!shooting)
			Shoot();
		if(shooting)
			MoveProjectile();
	}

	public void Shoot() {
		
			foreach(GameObject minion in GameObject.FindGameObjectsWithTag("Minion")) {
				if((theTower.position - minion.GetComponent<RectTransform>().position).magnitude < closestMinion.position.magnitude || closestMinion == null)
				closestMinion = minion.GetComponent<RectTransform>();
			}

			currProj = GameObject.Instantiate(projectile, theTower.position, Quaternion.identity);
			currProj.transform.SetParent(GameObject.Find("The Map").GetComponent<Transform> ());
			currProj.transform.LookAt(closestMinion);
			
			shooting = true;
		}

	public void MoveProjectile() {
		if((currProj.transform.position - closestMinion.position).magnitude < MIN_DISTANCE) {
				GameObject.Destroy(currProj);
				Debug.Log("Done shooting");
				shooting = false;
			} else {
				currProj.transform.LookAt(closestMinion);
				currProj.transform.Translate(Vector3.forward * PROJ_SPEED);
				Debug.Log("Shooting");
			}
	}
}
