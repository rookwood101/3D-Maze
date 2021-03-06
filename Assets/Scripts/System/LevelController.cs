﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	// This script should only be attached to one object!
	// It exists just to keep track of how many times the level
	// has been reloaded.
	public enum GameMode {Endless, TimeTrial, Tutorial};
    private static int levelCount = 1;
	private static bool addedOnSceneLoaded = false;
	private static GameMode gameMode = GameMode.Endless;
    [SerializeField]
    public GameObject FadeOut;

	void OnEnable () {
		if (!addedOnSceneLoaded) {
			SceneManager.sceneLoaded += OnSceneLoaded;
			addedOnSceneLoaded = true;
		}
	}
	
	public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		levelCount++;
	}

	public int GetLevelCount() {
		return levelCount;
	}
	public void SetLevelCount(int newLevelCount) {
		levelCount = newLevelCount;
	}

	public GameMode GetGameMode() {
		return gameMode;
	}

	public void SetGameMode(GameMode newGameMode) {
		gameMode = newGameMode;
        
        Invoke("LoadLevel", 0.5f);
        FadeOut.GetComponent<FadeOut>().Fade();
    }

    public void LoadLevel()
    {
        SetLevelCount(-1);
        SetGameMode(gameMode);
        SceneManager.LoadScene("Main");
    }

}
