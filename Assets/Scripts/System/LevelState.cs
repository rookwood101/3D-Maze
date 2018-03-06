using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelState : MonoBehaviour {

    int state;
    bool displayState;

    void Start() {
        DontDestroyOnLoad(gameObject);
        displayState = false;
    }


	void Update () {
        state = GameObject.Find("Score").GetComponent<ScoreUpdater>().LevelCompletionState;
        if (state > 0) {
            //GameObject.Find("WinLoseText").GetComponent<CanvasRenderer>().text = "Level Complete";
            gameObject.GetComponent<CanvasRenderer>().SetAlpha(1);
            displayState = true;
            Invoke(disableDisplayState(), 2);
        } else if (displayState == false){
            //GameObject.Find("WinLoseText").GetComponent<CanvasRenderer>(). = "";
            gameObject.GetComponent<CanvasRenderer>().SetAlpha(1);
        }
    }

    string disableDisplayState() {
        displayState = false;
        return "";
    }
}
