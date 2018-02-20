using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotator : MonoBehaviour {

    [SerializeField]
    int rotationSpeed;
	[SerializeField]
	int slerpSpeed;
	[SerializeField]
	int maxTilt;
    
    void Update() {
        //TODO make cube rotate depending on which side the ball is on

		float verticalTilt = Input.GetAxis("Vertical")*maxTilt;
		float horizontalTilt = -Input.GetAxis ("Horizontal")*maxTilt;

		transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.Euler (verticalTilt, 0, horizontalTilt), slerpSpeed * Time.deltaTime);
    

		Vector3[] dirVectors = {
			transform.up, //up
			-transform.up, //down
			transform.right, //right
			-transform.right, //left
			-transform.forward, //front
			transform.forward //back
		};
		Vector3 facingUp = new Vector3();
		float dot1 = 0;

		foreach (Vector3 v in dirVectors) {
			float dot2 = Vector3.Dot(v, Vector3.up);
			if (dot2 > dot1)
			{
				dot1 = dot2;
				facingUp = v;
			}

		}
	}
}