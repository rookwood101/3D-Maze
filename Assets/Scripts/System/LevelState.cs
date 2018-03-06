using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelState : MonoBehaviour {

    int state;
    bool handled;
    Color invisible;
    Color visible;

    void Start() {
        handled = false;
        invisible = new Color(1, 1, 1, 0);
        visible = new Color (1, 1, 1, 1);
    }


	void Update () {
        state = GameObject.Find("Score").GetComponent<ScoreUpdater>().LevelCompletionState;
        if (state > 0 && !handled) {
            Debug.Log("here");
            handled = true;
            if (state == 1) {
                // win
                GameObject.Find("WinText").GetComponent<Image>().color = visible;
            }
            if (state == 2) {
                // lose
                GameObject.Find("LoseText").GetComponent<Image>().color = visible;
            }
            Invoke("DisableDisplayState", 2);
        }
    }

    void DisableDisplayState() {
        SceneManager.LoadScene (SceneManager.GetActiveScene().name);
    }
}
