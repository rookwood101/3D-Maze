using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPickuper : MonoBehaviour {
	private ScoreUpdater scoreUpdater;

	void Start() {
		scoreUpdater = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreUpdater> ();
		scoreUpdater.RegisterPickup ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Ball")) {
			scoreUpdater.IncrementScore ();
			Destroy (gameObject);
		}
	}
}
