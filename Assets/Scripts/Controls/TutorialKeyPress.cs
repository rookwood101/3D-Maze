using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialKeyPress : MonoBehaviour {

	[SerializeField]
	private KeyCode myKey;
	private Renderer rend;

	void Start() {
		rend = GetComponent<Renderer>();
	}


	void Update () {
		if (Input.GetKeyDown(myKey)) {
			Fade ();
		}
	}

	void Fade() {
		if (rend.material.color.a > 0) {
			Color color = rend.material.color;
			color.a -= 0.1f;
			rend.material.color = color;
			Invoke ("Fade", 0.05f);
		}
	}
}
