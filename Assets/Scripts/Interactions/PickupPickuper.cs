using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPickuper : MonoBehaviour {
	private ScoreUpdater scoreUpdater;
    private TimeUpdater timeUpdater;
    [SerializeField]
    private GameObject audioPrefab;
    private bool destroyed = false;
    private bool registered = false;


    void Start() {
        if (!destroyed) {
            scoreUpdater = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreUpdater>();
            scoreUpdater.RegisterPickup();
            timeUpdater = GameObject.FindGameObjectWithTag("Time").GetComponent<TimeUpdater>();
            registered = true;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
			scoreUpdater.IncrementScore();
            if (timeUpdater != null) {
                timeUpdater.IncreaseTime();
            }
            Instantiate(audioPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        } else if (other.gameObject.CompareTag("Maze Wall")) {
            if (registered == true) {
                scoreUpdater.UnregisterPickup();
            }
            destroyed = true;
            Destroy(gameObject);
        }
	}

}
