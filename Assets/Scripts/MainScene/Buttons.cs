using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject scene;
    [SerializeField] Sprite soundOn, soundOff;


    private bool isStarted = false;


    public bool IsStarted
    {
        get
        {
            return isStarted;
        }
    }


    private void Start()
    {
        // If the sound was turned off then set the desired sprite
        if (gameObject.name == "Sound")
        {
            if(PlayerPrefs.GetString("Sound") == "off")
            {
                GetComponent<Image>().sprite = soundOff;
                Camera.main.GetComponent<AudioListener>().enabled = false;
            }
        }
    }


    private void OnMouseDown()
    {
        transform.localScale = new Vector3(1.03f, 1.03f, 1.03f);
       
    }


    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        
    }


    private void OnMouseUpAsButton()
    {
        GetComponent<AudioSource>().Play();

        switch (gameObject.name)
        {
            case "Play":
                scene.GetComponent<GameArrangement>().enabled = true;
                isStarted = true;
                break;
            case "Sound":
                if (PlayerPrefs.GetString("Sound") == "off")
                {
                    GetComponent<Image>().sprite = soundOn;
                    PlayerPrefs.SetString("Sound", "on");
                    Camera.main.GetComponent<AudioListener>().enabled = true;
                }
                else
                {
                    GetComponent<Image>().sprite = soundOff;
                    PlayerPrefs.SetString("Sound", "off");
                    Camera.main.GetComponent<AudioListener>().enabled = false;
                }
                break;
            case "Restart":
                scene.GetComponent<RestartGame>().enabled = true;
                scene.GetComponent<RestartGame>().RestartTheGame();
                Score.score = 0;
                break;
            default:
                break;
        }
    }
}
