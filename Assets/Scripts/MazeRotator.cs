using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotator : MonoBehaviour {

    //public float horizontalSpeed = 2.0F;
    //public float verticalSpeed = 2.0F;
    private float oldMouseX, oldMouseY, newMouseX, newMouseY;
    private Vector3 cursorDelta;
    private float rotaAxisX, rotaAxisY, rotaAxisZ; //Axis of Quaternion rotation
    private float relativeAxisX, relativeAxisY, relativeAxisZ; //adjusted axies to simulate cube having 0, 0, 0 rotation

    void Update() {

        //gets the position differential of the cursor between frames to calculate rotation of maze
        newMouseX = Input.mousePosition.x;
        newMouseY = Input.mousePosition.y;
        if (Input.GetMouseButton(0)) {
            cursorDelta = new Vector3 (newMouseX-oldMouseX, newMouseY - oldMouseY, 0.0f);

            /*
            mouseDelta.x -= transform.eulerAngles.x;
            mouseDelta.y -= transform.eulerAngles.y;
            mouseDelta.z -= transform.eulerAngles.z;
            Vector3 adjustedCross = Vector3.Cross(mouseDelta, intoCube);
            */
            //gets rotation axis from Cross of mouseDelta Vector and Camera vision direction vector
            Vector3 intoCube = new Vector3(0, 0, 1);
            Quaternion rotate = Quaternion.AngleAxis(1, Vector3.Cross(cursorDelta, intoCube)); //- Quaternion.identity;

            //gets rotation vector for maze
            rotaAxisX = rotate.eulerAngles.x;
            rotaAxisY = rotate.eulerAngles.y;
            rotaAxisZ = rotate.eulerAngles.z;
            Vector3 rotationAxis = new Vector3(rotaAxisX, rotaAxisY, rotaAxisZ);
            transform.Rotate(rotationAxis); //rotate
        } else {
            relativeAxisX = transform.eulerAngles.x;
            relativeAxisY = transform.eulerAngles.y;
            relativeAxisZ = transform.eulerAngles.z;
        }
        oldMouseX = newMouseX;
        oldMouseY = newMouseY;

        



        /* THIS ONLY WORKS IN A SINGLE DIRECTION
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(v, h, 0);
        */
    }
}