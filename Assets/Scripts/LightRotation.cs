using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour {

	void Update () {
        transform.RotateAround(new Vector3(0,15,0), new Vector3(0,1,0), 0.2f);
	}

}
