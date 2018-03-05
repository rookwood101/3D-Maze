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
	private Animator scoreAnimator;
	private LevelController levelController;

	private int score = 0;
	private int pickupCount = 0;

	void Start() {
		scoreAnimator = GetComponent<Animator>();
		levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
	}

	public void IncrementScore () {
		score++;
		scoreText.text = score.ToString ();
		if (levelController.GetGameMode() != LevelController.GameMode.TimeTrial) {
			scoreAnimator.SetTrigger("Shake");
		}
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
