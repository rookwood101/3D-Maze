﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject cube;

	// Use this for initialization
	void Start () {
		Debug.Log ("Beginning Cube Spawning");
        Instantiate(cube);
    }
	
	// Update is called once per frame
	void Update () {
	}
}