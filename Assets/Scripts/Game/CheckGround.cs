using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckGround : MonoBehaviour
{
    [SerializeField] AudioClip indicatorAudio, platformAudio;


    private bool isCheckedGround = false, isCheckedIndicator = false;


    public bool IsCheckedGround
    {
        get
        {
            return isCheckedGround;
        }
    }


    public bool IsCheckedIndicator
    {
        get
        {
            return isCheckedIndicator;
        }
        set
        {
            isCheckedIndicator = value;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "IndicatorPlatform")
        {
            Score.score += 2;
            isCheckedGround = true;
            isCheckedIndicator = true;
            GetComponent<AudioSource>().clip = indicatorAudio;
        }
        else
        {
            if (other.gameObject.tag == "PlatformBase" && !isCheckedIndicator)
            {
                Score.score += 1;
                isCheckedGround = true;
                GetComponent<AudioSource>().clip = platformAudio;
            }
        }

        GetComponent<AudioSource>().Play();
    }
}
