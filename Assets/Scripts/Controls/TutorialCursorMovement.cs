using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCursorMovement : MonoBehaviour {

	private Vector3 startPosition;

	[SerializeField]
	private Sprite[] sprites;
	[SerializeField]
	private float speed;

	private SpriteRenderer rend;

	// Use this for initialization
	void Start () {
		startPosition = transform.localPosition;
		rend = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		float time = speed * Time.timeSinceLevelLoad;
		transform.localPosition = startPosition + new Vector3 (-Mathf.Sin(time), Mathf.Sin(time), 0);

		if (Mathf.Cos(time) < 0) {
			rend.sprite = sprites [1];
		} else {
			rend.sprite = sprites [0];
		}
	}
}
