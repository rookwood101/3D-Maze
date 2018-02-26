using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPickuper : MonoBehaviour {
	private ScoreUpdater scoreUpdater;
    [SerializeField]
    private GameObject audioPrefab;
    private bool destroyed = false;


    void Start() {
        if (!destroyed) {
            scoreUpdater = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreUpdater>();
            scoreUpdater.RegisterPickup();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
			scoreUpdater.IncrementScore ();
            GameObject pickupAudioClone = Instantiate(audioPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        } else if (other.gameObject.CompareTag("Maze Wall")) {
            destroyed = true;
            Destroy(gameObject);
        }
	}

}
