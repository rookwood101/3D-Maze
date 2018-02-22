using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPickuper : MonoBehaviour {
	private ScoreUpdater scoreUpdater;
    [SerializeField]
    private GameObject audioPrefab;


    void Start() {
        scoreUpdater = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreUpdater>();
    }

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Ball")) {
			scoreUpdater.IncrementScore ();
            GameObject pickupAudioClone = Instantiate(audioPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
	}

}
