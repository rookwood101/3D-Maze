using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour {

	[SerializeField]
    int rotationSpeed = 100;
	[SerializeField]
	private GameObject cameraRotator;
	[SerializeField]
	private GameObject camera;

    private bool camPosSaved = false;
    private Quaternion savedCamPos;
    private float oldMouseX, oldMouseY, newMouseX, newMouseY;
    private Vector3 cursorDelta, intoCube;

	// Use this for initialization
	void Start () {
        //savedCamPos = camera.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

        //gets the position differential of the cursor between frames to calculate rotation of maze
        newMouseX = Input.mousePosition.x;
        newMouseY = Input.mousePosition.y;
        if (Input.GetMouseButton(0))
        {
            if (camPosSaved == false) {
                //savedCamPos = camera.transform.rotation;
                camPosSaved = true;
            }
            //gets rotation axis from Cross of mouseDelta Vector and Camera vision direction vector
            cursorDelta = new Vector3(newMouseX - oldMouseX, newMouseY - oldMouseY, 0.0f);

            intoCube = cameraRotator.transform.position - camera.transform.position;
            transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.Cross(cursorDelta, intoCube));
        } else {
            //camera.transform.rotation = savedCamPos;
            camPosSaved = false;
        }
        oldMouseX = newMouseX;
        oldMouseY = newMouseY;

        /*
        if (Input.GetKey(KeyCode.UpArrow)) {
			transform.rotation = transform.rotation * Quaternion.Euler (rotationSpeed * Time.deltaTime, 0, 0);
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			transform.rotation = transform.rotation * Quaternion.Euler (-rotationSpeed * Time.deltaTime, 0, 0);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.rotation = Quaternion.Euler (0, -rotationSpeed * Time.deltaTime, 0) * transform.rotation;
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.rotation = Quaternion.Euler (0, rotationSpeed * Time.deltaTime, 0) * transform.rotation;
		}
        if (Input.GetKey(KeyCode.Space)) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        */
    }


}
