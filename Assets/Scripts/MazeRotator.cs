using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotator : MonoBehaviour {

    /*float rotSpeed = 100;

    void OnMouseDrag() {
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

        transform.Rotate(Vector3.up, -rotX);
        transform.Rotate(Vector3.right, rotY);
    }*/

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

            /*
            mouseDelta.x -= transform.eulerAngles.x;
            mouseDelta.y -= transform.eulerAngles.y;
            mouseDelta.z -= transform.eulerAngles.z;
            Vector3 adjustedCross = Vector3.Cross(mouseDelta, intoCube);
            */

            //gets rotation axis from Cross of mouseDelta Vector and Camera vision direction vector
            cursorDelta = new Vector3(newMouseX - oldMouseX, newMouseY - oldMouseY, 0.0f);
            intoCube = new Vector3(0, 0, 1);//transform.forward;
            transform.rotation *= Quaternion.AngleAxis(1, Vector3.Cross(cursorDelta, intoCube));

            /*
            //gets rotation vector for maze
            rotaAxisX = rotate.eulerAngles.x;
            rotaAxisY = rotate.eulerAngles.y;
            rotaAxisZ = rotate.eulerAngles.z;
            Vector3 rotationAxis = new Vector3(rotaAxisX, rotaAxisY, rotaAxisZ);
            transform.Rotate(rotationAxis); //rotate
            transform.rotation = transform.rotation + rotate;
            */

        }
        else {
            /*
            relativeAxisX = transform.eulerAngles.x;
            relativeAxisY = transform.eulerAngles.y;
            relativeAxisZ = transform.eulerAngles.z;
            */
        }

        oldMouseX = newMouseX;
        oldMouseY = newMouseY;
    }
}