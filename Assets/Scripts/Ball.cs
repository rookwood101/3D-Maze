using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    Vector3 gravity;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }
	
	// Update is called once per frame
	void Update () {

    }
}
