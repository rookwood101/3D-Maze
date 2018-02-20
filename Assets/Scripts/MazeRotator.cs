using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotator : MonoBehaviour {

	[SerializeField]
	int slerpSpeed;
	[SerializeField]
	private int maxTilt;
	[SerializeField]
	private GameObject top, bottop, right, left, front, back; //represent the parents of maze
	private GameObject currentFace; //face with ball on it
	private Quaternion currentFaceDirection = Quaternion.identity;

	void Start() {
		currentFace = GameObject.FindGameObjectWithTag ("Top");
	}

    void Update() {
        //TODO make cube rotate depending on which side the ball is on

		float verticalTilt = Input.GetAxis("Vertical")*maxTilt;
		float horizontalTilt = -Input.GetAxis ("Horizontal")*maxTilt;

		transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.Euler (verticalTilt, 0, horizontalTilt) * currentFaceDirection, slerpSpeed * Time.deltaTime);

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

	public void SetCurrentFace (GameObject face) {
		currentFace = face;

		if (face.CompareTag ("Top")) {
			currentFaceDirection = Quaternion.Euler (0, 0, 0) * currentFaceDirection;
		} else if (face.CompareTag ("Bottom")) {
			currentFaceDirection = Quaternion.Euler (0, 0, 180) * currentFaceDirection;
		} else if (face.CompareTag ("Right")) {
			currentFaceDirection = Quaternion.Euler (0, 0, 90) * currentFaceDirection;
		} else if (face.CompareTag ("Left")) {
			currentFaceDirection = Quaternion.Euler (0, 0, -90) * currentFaceDirection;
		} else if (face.CompareTag ("Front")) {
			currentFaceDirection = Quaternion.Euler (90, 0, 0) * currentFaceDirection;
		} else if (face.CompareTag ("Back")) {
			currentFaceDirection = Quaternion.Euler (-90, 0, 0) * currentFaceDirection;
		}
			
		//set the face's Y as the new Y of the cube, and use the "transform.localRotation = Quaternion.Slerp"
		//tilt correction to face it up, while temporarily parenting the ball to the maze so it doesn't spaz out
		//front = -z
		//back = z
		//top = y
		//bottom = -y
		//right = x
		//left = -x

	}
}