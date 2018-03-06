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
    [SerializeField]
    public int LevelCompletionState; //0 for unfinished, 1 for win, 2 for lose

	private int score = 0;
	private int pickupCount = 0;

	void Start() {
        LevelCompletionState = 0;
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

            LevelCompletionState = 1;

			if (levelController.GetGameMode() == LevelController.GameMode.Tutorial && levelController.GetLevelCount() == 1) {
				SceneManager.LoadScene("Menu");
			}

			int levelsCompleted = PlayerPrefs.GetInt("levelsCompleted", 0);
			levelsCompleted++;
			PlayerPrefs.SetInt("levelsCompleted", levelsCompleted);
			PlayerPrefs.Save();
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

        LevelCompletionState = 2;
	}
}
