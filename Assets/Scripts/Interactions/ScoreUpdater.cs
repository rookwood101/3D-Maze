using System.Collections;
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

			int levelsCompleted = PlayerPrefs.GetInt("levelsCompleted", 0);
			levelsCompleted++;
			PlayerPrefs.SetInt("levelsCompleted", levelsCompleted);
			PlayerPrefs.Save();
			
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

	public void TimeUp() {
		Debug.Log("Level Failed :(");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
