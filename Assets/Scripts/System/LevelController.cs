using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	// This script should only be attached to one object!
	// It exists just to keep track of how many times the level
	// has been reloaded.
	public enum GameMode {Endless, TimeTrial};
    private static int levelCount = 1;
	private static bool addedOnSceneLoaded = false;
	private static GameMode gameMode = GameMode.Endless;

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
	}
}
