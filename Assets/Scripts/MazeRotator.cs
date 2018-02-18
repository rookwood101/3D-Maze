using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotator : MonoBehaviour {

    [SerializeField]
    int rotationSpeed;

    private bool rotating = false;
    private IEnumerator coroutine;
    public bool isTop, isBot, isRight, isLeft, isFront, isBack;
    
    void Update() {
        //TODO make cube rotate depending on which side the ball is on
        //if (Quaternion.LookRotation )

        //TODO make cube tilt correctly, at this point only W and A work
        if (Input.GetKey(KeyCode.W) && (transform.eulerAngles.x % 90) < 15)
        {
            transform.rotation *= Quaternion.Euler(rotationSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S) && (transform.eulerAngles.x % 90) < 15)
        {
            transform.rotation *= Quaternion.Euler(-rotationSpeed * Time.deltaTime, 0, 0);
        }
        else if ((transform.eulerAngles.x % 90) > 0) {
            //TODO rotate gradually back to x = 0
        }

        if (Input.GetKey(KeyCode.A) && (transform.eulerAngles.z % 90) < 15) {
            transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) && (transform.eulerAngles.z % 90) < 15) {
            transform.rotation *= Quaternion.Euler(0, 0, -rotationSpeed * Time.deltaTime);
        }
        else if ((transform.eulerAngles.x % 90) > 0) {
            //TODO rotate gradually back to z = 0
        }

        //makes cube rotate 90°, but also moves ball, so either find a way to make
        //this work in CameraRotator or freeze ball physics while rotating == true
        if (Input.GetKeyDown(KeyCode.Q) && !rotating)
        {
            coroutine = WaitForSeconds(0.01f, 1);
            StartCoroutine(coroutine);
        }
        if (Input.GetKeyDown(KeyCode.E) && !rotating)
        {
            coroutine = WaitForSeconds(0.01f, -1);
            StartCoroutine(coroutine);
        }

        /*
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
        */
    }

    private IEnumerator WaitForSeconds(float delay, int direction) {
        rotating = true;
        for (int i = 0; i < 45; i++) {
            yield return new WaitForSeconds(delay);
            transform.rotation *= Quaternion.AngleAxis(direction * 2, new Vector3(0, 1, 0));
        }
        rotating = false;
    }
}