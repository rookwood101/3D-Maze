﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	// This script should only be attached to one object!
	// It exists just to keep track of how many times the level
	// has been reloaded.
	[SerializeField]
	private static int levelCount = 0;

	void Start () {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	
	public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		levelCount++;
	}

	public int GetLevelCount() {
		return levelCount;
	}
}