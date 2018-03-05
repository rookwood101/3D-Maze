using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeUpdater : MonoBehaviour {
    [SerializeField]
    private Text minutesText;
    [SerializeField]
    private Text secondsText;
    [SerializeField]
    private Text colonText;
    private float startTime;
    private float allowedTime = 20.99f;
    private float extraTimePerPickup = 7;
    private bool isTimeUp = false;
    private LevelController levelController;
    private ScoreUpdater scoreUpdater;
    private Animator timeAnimator;

	void Start() {
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
        if (levelController.GetGameMode() != LevelController.GameMode.TimeTrial) {
            Destroy(minutesText);
            Destroy(secondsText);
            Destroy(colonText);
            Destroy(gameObject);
        }
        startTime = Time.timeSinceLevelLoad;
        scoreUpdater = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreUpdater>();
        timeAnimator = GetComponent<Animator>();
    }

    void Update() {
        float timeRemaining = startTime + allowedTime - Time.timeSinceLevelLoad;
        if (timeRemaining > 0) {
            minutesText.text = Mathf.Floor(timeRemaining / 60f).ToString("00");
            secondsText.text = Mathf.Floor(timeRemaining % 60f).ToString("00");
        } else if (!isTimeUp) {
            isTimeUp = true;
            minutesText.text = "00";
            secondsText.text = "00";
            scoreUpdater.TimeUp();
            Destroy(gameObject);
        }
    }

    public void IncreaseTime() {
        allowedTime += extraTimePerPickup;
        timeAnimator.SetTrigger("Shake");
    }
}
