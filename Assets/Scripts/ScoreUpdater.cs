using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour {
	[SerializeField]
	private Text scoreText;

	private int score = 0;

	public void IncrementScore () {
		score++;
		scoreText.text = score.ToString ();
	}
}
