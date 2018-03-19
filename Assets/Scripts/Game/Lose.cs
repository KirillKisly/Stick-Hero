using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : MonoBehaviour
{
    [SerializeField] Text scoreTxt, bestTxt;
    [SerializeField] GameObject losePanel, restartButton;


    private bool hasLosed = false;


    public bool HasLosed
    {
        get
        {
            return hasLosed;
        }
        set
        {
            hasLosed = value;
        }
    }


    void Update()
    {
        if (hasLosed)
        {
            scoreTxt.text = Score.score.ToString();
            bestTxt.text = PlayerPrefs.GetInt("Best").ToString();
            losePanel.SetActive(true);
        }
    }
}
