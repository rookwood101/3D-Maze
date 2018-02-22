using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotator : MonoBehaviour {

	[SerializeField]
	int slerpSpeed;
	[SerializeField]
	private int maxTilt;
    [SerializeField]
    private GameObject rotateAudioPrefab;
    GameObject rotateAudioClone;
    private GameObject currentFace; //face with ball on it
	private Quaternion currentFaceDirection = Quaternion.identity;
    private bool tilt = false;
    private int viewDirection = 0;
    private float verticalTilt = 0;
    private float horizontalTilt = 0;

    void Start() {
		currentFace = GameObject.FindGameObjectWithTag ("Top");
    }

    void Update() {

        //gets value of view direction when rotating with Q and E
        if (Input.GetKeyDown(KeyCode.Q)) {
            viewDirection--;
            rotateAudioClone = Instantiate(rotateAudioPrefab, transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            viewDirection++;
            rotateAudioClone = Instantiate(rotateAudioPrefab, transform.position, transform.rotation);
        }
        if (viewDirection > 3)
            viewDirection = 0;
        if (viewDirection < 0)
            viewDirection = 3;

        switch (viewDirection) {
            case 0:
                verticalTilt = Input.GetAxis("Vertical") * maxTilt;
                horizontalTilt = -Input.GetAxis("Horizontal") * maxTilt;
                break;
            case 1:
                verticalTilt = Input.GetAxis("Horizontal") * maxTilt;
                horizontalTilt = Input.GetAxis("Vertical") * maxTilt;
                break;
            case 2:
                verticalTilt = -Input.GetAxis("Vertical") * maxTilt;
                horizontalTilt = Input.GetAxis("Horizontal") * maxTilt;
                break;
            case 3:
                verticalTilt = -Input.GetAxis("Horizontal") * maxTilt;
                horizontalTilt = -Input.GetAxis("Vertical") * maxTilt;
                break;
        }

        //rotate entire cube
        transform.localRotation = Quaternion.Slerp (transform.localRotation, currentFaceDirection, slerpSpeed * Time.deltaTime);
        //tilt from top
        transform.parent.localRotation = Quaternion.Slerp (transform.parent.localRotation, Quaternion.Euler (verticalTilt, 90*viewDirection, horizontalTilt), slerpSpeed * Time.deltaTime);

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