using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
	private LevelController levelController;
	void Start() {
		levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
	}
	public void OnButtonClick() {
		levelController.SetLevelCount(-1);
		SceneManager.LoadScene("Main");
	}
}
