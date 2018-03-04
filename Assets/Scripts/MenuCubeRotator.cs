using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCubeRotator : MonoBehaviour {
	[SerializeField]
	private float rotationSpeed;
	void Update () {
		transform.Rotate(rotationSpeed*Time.deltaTime*0.5f, rotationSpeed * Time.deltaTime, 0);
	}
}
