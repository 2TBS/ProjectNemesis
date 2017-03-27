using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///Spawns minions from set speed
public class MinionSpawner : MonoBehaviour {

	Slider tempSlider;
	public Object minionPrefab;

	// Use this for initialization
	void Start () {
		tempSlider = GameObject.Find("tempMinionSpawn").GetComponent<Slider> ();
		StartCoroutine(spawnMinions());	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator spawnMinions () {
		while(true) {
			GameObject minion = Instantiate(minionPrefab, new Vector3(-100,25,0), Quaternion.identity) as GameObject;
			minion.transform.SetParent(GameObject.Find("The Map").transform);
			Debug.Log("Spawned minion. waiting " + 10/tempSlider.value + " secs");
			yield return new WaitForSeconds(10/tempSlider.value);
		}
		
	}
}
