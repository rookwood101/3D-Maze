using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

	[SerializeField]
	private LevelController.GameMode gameMode;
	private LevelController levelController;
    [SerializeField]
    public Image transitionImage;
    [SerializeField]
    public Animator anim;

    void Start() {
		levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
	}
	public void OnButtonClick() {
        levelController.SetGameMode(gameMode);
	}


}
