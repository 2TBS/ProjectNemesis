using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	public void switchScene() {
		SceneManager.LoadScene (0);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
