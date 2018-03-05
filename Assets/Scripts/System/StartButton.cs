using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
	[SerializeField]
	private LevelController.GameMode gameMode;
	[SerializeField]
	private int startLevel;
	private LevelController levelController;
	void Start() {
		levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
	}
	public void OnButtonClick() {
		levelController.SetLevelCount(startLevel - 1);
		levelController.SetGameMode(gameMode);
		SceneManager.LoadScene("Main");
	}
}
