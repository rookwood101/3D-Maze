using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFace : MonoBehaviour {

	private GameObject ball, maze;
    [SerializeField]
    private GameObject audioPrefab;

    void Start() {
		maze = GameObject.FindGameObjectWithTag ("Maze");
		ball = GameObject.FindGameObjectWithTag ("Ball");
	}


	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Ball")) {
			Debug.Log("Collided with ball");
            maze.GetComponent<MazeRotator>().SetCurrentFace (gameObject);
            GameObject rotateAudioClone = Instantiate(audioPrefab, transform.position, transform.rotation);
		}
	}
}
