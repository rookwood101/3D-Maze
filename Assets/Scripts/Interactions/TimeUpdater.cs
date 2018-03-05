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
    private float allowedTime = 10f;
    private LevelController levelController;
    private ScoreUpdater scoreUpdater;

	void Start() {
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
        if (levelController.GetGameMode() != LevelController.GameMode.TimeTrial
          ||levelController.GetLevelCount() < 1) {
            Destroy(minutesText);
            Destroy(secondsText);
            Destroy(colonText);
            Destroy(gameObject);
        }
        startTime = Time.timeSinceLevelLoad;
        scoreUpdater = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreUpdater>();
    }

    void Update() {
        float timeRemaining = startTime + allowedTime - Time.time;
        if (timeRemaining > 0) {
            minutesText.text = Mathf.Floor(timeRemaining / 60f).ToString("00");
            secondsText.text = Mathf.Floor(timeRemaining % 60f).ToString("00");
        } else {
            minutesText.text = "00";
            secondsText.text = "00";
            Destroy(gameObject);
            scoreUpdater.TimeUp();
        }
    }
}
