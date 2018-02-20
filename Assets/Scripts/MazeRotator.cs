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
		float otherticalTilt = Input.GetAxis ("Othertical") * maxTilt;

		transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.Euler (verticalTilt, otherticalTilt, horizontalTilt), slerpSpeed * Time.deltaTime);
    }
}