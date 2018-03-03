using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSideArrow : MonoBehaviour
{

    private Vector3 startPosition;

    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private float speed;
    private Renderer rend;
    private GameObject ball;
    private float dist;
    private Color color;
    private LevelController levelController;

    // Use this for initialization
    void Start()
    {
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
        if (levelController.GetLevelCount() != 1) {
            Destroy(gameObject);
        }
        rend = GetComponent<Renderer>();
        color = rend.material.color;
        ball = GameObject.FindGameObjectWithTag("Ball");
        startPosition = transform.localPosition;
        dist = Vector3.Distance(ball.GetComponent<Transform>().position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        dist = Vector3.Distance(ball.GetComponent<Transform>().position, transform.position);
        if (dist < 4)
        {
            color = rend.material.color;
            color.a = 2f - (dist) / 2;
            rend.material.color = color;
        }
        else {
            color.a = 0;
            rend.material.color = color;
        }

        float time = speed * Time.timeSinceLevelLoad;
        transform.localPosition = startPosition + new Vector3(0, Mathf.Sin(time), 0);
    }
}
