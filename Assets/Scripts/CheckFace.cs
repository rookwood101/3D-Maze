using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFace : MonoBehaviour {

	private GameObject maze;
	private GameObject ball;
	private Coroutine SetBallParent;

	void Start() {
		maze = GameObject.FindGameObjectWithTag ("Maze");
		ball = GameObject.FindGameObjectWithTag ("Ball");
	}


	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Ball")) {
			ball.transform.parent = maze.transform;
			maze.GetComponent<MazeRotator>().SetCurrentFace (gameObject);
			Invoke ("UpdateBallParent", 2f);

		}
	}

	void UpdateBallParent() {
		ball.transform.parent = null;
	}
}
