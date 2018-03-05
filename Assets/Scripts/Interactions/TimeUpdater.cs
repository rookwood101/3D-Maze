using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeUpdater : MonoBehaviour {
	enum TimerPart {Minutes, Seconds, Colon};

    [SerializeField]
    private TimerPart timerPart;
    private Text textComponent;
    private float startTime;
    private float allowedTime = 600f;
    private LevelController levelController;

	void Start() {
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
        if (levelController.GetGameMode() != LevelController.GameMode.TimeTrial
          ||levelController.GetLevelCount() < 1) {
            Destroy(gameObject);
        }
        textComponent = GetComponent<Text>();
        startTime = Time.timeSinceLevelLoad;
    }

    void Update() {
        float timeRemaining = startTime + allowedTime - Time.time;
        switch (timerPart) {
            case TimerPart.Minutes:
                textComponent.text = Mathf.Floor(timeRemaining / 60f).ToString("00");
            break;
            case TimerPart.Seconds:
                textComponent.text = Mathf.Floor(timeRemaining % 60f).ToString("00");
            break;
        }
    }
}
