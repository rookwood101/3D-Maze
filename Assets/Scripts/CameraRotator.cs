﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow)) {
			transform.rotation = transform.rotation * Quaternion.Euler (1, 0, 0);
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			transform.rotation = transform.rotation * Quaternion.Euler (-1, 0, 0);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.rotation = Quaternion.Euler (0, -1, 0) * transform.rotation;
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.rotation = Quaternion.Euler (0, 1, 0) * transform.rotation;
		}
	}
}