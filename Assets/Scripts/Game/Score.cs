using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score = 0;


    private Text scoreTxt;
    private bool isStarted;


    private void Start()
    {
        score = 0;
        scoreTxt = GetComponent<Text>();
        scoreTxt.text = "0";
    }


    private void Update()
    {
        if (!GameObject.Find("Scene").GetComponent<Lose>().HasLosed)
        {
            if (scoreTxt.text == "0")
                isStarted = true;
            if (isStarted)
            {
                scoreTxt.text = score.ToString();

                if (PlayerPrefs.GetInt("Best") < score)
                {
                    PlayerPrefs.SetInt("Best", score);
                }
            }
        }
        else
        {
            scoreTxt.gameObject.SetActive(false);
        }
    }
}
