﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Attach to camera to allow screen to move with mouse.
public class CameraMove : MonoBehaviour {

	Camera theCamera;
	float fov;
	public const float SCROLL_SPEED = 200f;
	public const float MOUSE_SCROLL_SPEED = 8f;

	// Use this for initialization
	void Start () {
		theCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.mousePosition.y >= Screen.height *0.95) 
             transform.Translate(Vector3.up * Time.deltaTime * SCROLL_SPEED, Space.World);
    	else if (Input.mousePosition.y <= Screen.height*0.05) 
			 transform.Translate(Vector3.down * Time.deltaTime * SCROLL_SPEED, Space.World);
		else if (Input.mousePosition.x >= Screen.width*0.95) 
			 transform.Translate(Vector3.right * Time.deltaTime * SCROLL_SPEED, Space.World);
		else if (Input.mousePosition.x <= Screen.width*0.05) 
			 transform.Translate(Vector3.left * Time.deltaTime * SCROLL_SPEED, Space.World);

		fov = theCamera.fieldOfView;

		if (Input.GetAxis("Mouse ScrollWheel") < 0 && fov < 60)
			theCamera.fieldOfView += MOUSE_SCROLL_SPEED;
        	
     	if (Input.GetAxis("Mouse ScrollWheel") > 0 && fov > 20)
			 theCamera.fieldOfView -= MOUSE_SCROLL_SPEED;
	}
}
