using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class MazeRotator : MonoBehaviour {

	[SerializeField]
	int slerpSpeed;
	[SerializeField]
	private int maxTilt;
	[SerializeField]
	private GameObject top, right, left, front, back; //represent the parents of maze
	private GameObject currentFace; //face with ball on it
	private Quaternion currentFaceDirection = Quaternion.identity;
    private AudioSource sfx;
    public AudioClip Tilt, Reset;
    private bool tilt = false;

	void Start() {
		currentFace = GameObject.FindGameObjectWithTag ("Top");
	}

    void Update() {
        //TODO make cube rotate depending on which side the ball is on

		float verticalTilt = Input.GetAxis("Vertical")*maxTilt;
		float horizontalTilt = -Input.GetAxis ("Horizontal")*maxTilt;

        //TODO put this into a prefab that disappears after 1 second so that more than one sound plays at a time
        //TODO make the tilt sound play at a higher pitch if there is already a button pressed
        //TODO make the reset sound play when no buttons are pressed but the cube is still tilted
        if (Input.GetKeyDown("up") || Input.GetKeyDown("down") || Input.GetKeyDown("left") || Input.GetKeyDown("right"))
        {
            GetComponent<AudioSource>().Play();
        }

        transform.localRotation = Quaternion.Slerp (transform.localRotation, currentFaceDirection, slerpSpeed * Time.deltaTime);
		transform.parent.localRotation = Quaternion.Slerp (transform.parent.localRotation, Quaternion.Euler (verticalTilt, 0, horizontalTilt), slerpSpeed * Time.deltaTime);

	}

	public void SetCurrentFace (GameObject face) {
		currentFace = face;

		if (face.CompareTag ("Right")) {
			currentFaceDirection = Quaternion.Euler (0, 0, 90) * currentFaceDirection;
		} else if (face.CompareTag ("Left")) {
			currentFaceDirection = Quaternion.Euler (0, 0, -90) * currentFaceDirection;
		} else if (face.CompareTag ("Front")) {
			currentFaceDirection = Quaternion.Euler (90, 0, 0) * currentFaceDirection;
		} else if (face.CompareTag ("Back")) {
			currentFaceDirection = Quaternion.Euler (-90, 0, 0) * currentFaceDirection;
		}
	}
}