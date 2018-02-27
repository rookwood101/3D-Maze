﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreUpdater : MonoBehaviour {
	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private Text pickupCountText;

	private int score = 0;
	private int pickupCount = 0;

	public void IncrementScore () {
		score++;
		scoreText.text = score.ToString ();

		if (score == pickupCount) {
			Debug.Log ("Level Complete!");
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
	public void RegisterPickup() {
		pickupCount++;
		pickupCountText.text = pickupCount.ToString();
	}
	public void UnregisterPickup() {
		pickupCount--;
		pickupCountText.text = pickupCount.ToString();
	}
}
