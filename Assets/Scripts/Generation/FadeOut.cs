using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

    public GameObject transition;
    private Image transitionImage;
    private Animator anim;
    private bool fading;

    void Start () {
        anim = transition.GetComponent<Animator>();
        transitionImage = transition.GetComponent<Image>();
        transitionImage.color = new Color(1, 1, 1, 0);
    }
	
	void Update () {
        if (fading)
            transitionImage.color = new Color(1, 1, 1, transitionImage.color.a + 4*Time.deltaTime);
    }

    public void Fade() {
        fading = true;
        //anim.Play(transition.GetComponent<Animation>().clip.name);
    }
}
