using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRotator : MonoBehaviour {
	[SerializeField]
	private float rotationSpeed;
	void Start () {
		transform.localRotation = Quaternion.Euler(0, Random.Range(0, 359), 0);
	}
	void Update () {
		transform.Rotate(0, rotationSpeed*Time.deltaTime, 0);
	}
}
