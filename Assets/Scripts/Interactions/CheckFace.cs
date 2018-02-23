using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFace : MonoBehaviour {

	private MazeRotator mazeRotator;
    [SerializeField]
    private GameObject audioPrefab;

    void Start() {
		mazeRotator = GameObject.FindGameObjectWithTag ("Maze").GetComponent<MazeRotator>();
	}


	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Ball")) {
			mazeRotator.SetCurrentFace (gameObject);
            Instantiate(audioPrefab, transform.position, transform.rotation);
		}
	}
}
