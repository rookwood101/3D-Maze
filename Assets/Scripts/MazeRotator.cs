using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotator : MonoBehaviour {

	[SerializeField]
    int rotationSpeed = 100;
	[SerializeField]
	private GameObject cameraRotator;
	[SerializeField]
	private GameObject camera;

    private float oldMouseX, oldMouseY, newMouseX, newMouseY;
    private Vector3 cursorDelta, intoCube;
    private float rotaAxisX, rotaAxisY, rotaAxisZ; //Axis of Quaternion rotation
    private float relativeAxisX, relativeAxisY, relativeAxisZ; //adjusted axies to simulate cube having 0, 0, 0 rotation

    void Update()
    {

        //gets the position differential of the cursor between frames to calculate rotation of maze
        newMouseX = Input.mousePosition.x;
        newMouseY = Input.mousePosition.y;
        if (Input.GetMouseButton(0))
        {
            //gets rotation axis from Cross of mouseDelta Vector and Camera vision direction vector
            cursorDelta = new Vector3(newMouseX - oldMouseX, newMouseY - oldMouseY, 0.0f);

			intoCube = cameraRotator.transform.position - camera.transform.position;
			transform.rotation = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.Cross(cursorDelta, intoCube)) * transform.rotation;
        }

        oldMouseX = newMouseX;
        oldMouseY = newMouseY;
    }
}