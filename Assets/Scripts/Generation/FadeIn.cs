using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    [SerializeField]
    public Image transitionImage;
    [SerializeField]
    public Animator anim;

    void Start () {
        anim.Play(anim.GetComponent<Animation>().clip.name);
    }
}
