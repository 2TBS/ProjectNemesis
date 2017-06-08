using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///Logic for the creation of towers. Attach to TowerCreateButton.
public class TowerCreate : MonoBehaviour {

	public Image towerImage; //Which tower is this?
	public Image followMouseSprite; //follows the mouse until put down
	public Sprite blankImage;
	public Object towerPrefab;
	public bool placing; //Is the player currently placing a tower?

	// Use this for initialization
	void Start () {
		//followMouseSprite.sprite = towerImage.sprite;
		followMouseSprite.transform.position = new Vector3(100000000, 0, 0); 
	}
	
	// Update is called once per frame
	void Update () {
		
			followMouseSprite.transform.position = Input.mousePosition;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
			//if(Input.GetMouseButtonDown(0))
=======
=======
>>>>>>> d498cabc876474023394e41717f54b3894a8aae9
=======
>>>>>>> origin/develop
            if (Input.GetMouseButtonDown(0))
            {

            }
		}
>>>>>>> origin/develop
	}

	///Add to OnClickEvent for TowerCreateButton
	public void clickAction () {
		placing = !placing;
		if (placing) {
			var v3 = Input.mousePosition;
			v3.z = 10.0f;
			//v3 = Camera.main.ScreenToWorldPoint (v3);
			GameObject tower = Instantiate (towerPrefab, v3, Quaternion.identity) as GameObject;
			followMouseSprite.sprite = blankImage;
		} else {
			followMouseSprite.sprite = towerImage.sprite;
		}
	}
}
