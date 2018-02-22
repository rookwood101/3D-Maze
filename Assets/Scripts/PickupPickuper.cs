using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPickuper : MonoBehaviour {
	private ScoreUpdater scoreUpdater;
    private AudioClip ding;

    void Start() {
		scoreUpdater = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreUpdater> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Ball")) {
			scoreUpdater.IncrementScore ();
            //GetComponent<AudioSource>().Play();
            Destroy (gameObject);
		}
	}
}
