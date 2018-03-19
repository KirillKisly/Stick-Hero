using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameArrangement : MonoBehaviour
{
    [SerializeField] GameObject buttonPlay, buttons, hero;
    [SerializeField] Text gameNameText, scoreText;
    [SerializeField] Animator animatorPlatforms;


    private int getHashCondition = 0;


    private void Awake()
    {
        getHashCondition = Animator.StringToHash("Condition");
    }
    

    private void Start()
    {
        if (buttonPlay.GetComponent<Buttons>().IsStarted)
        {
            StartCoroutine(InitializationObjects());               
            StartCoroutine(StartMovePlatformCoroutune());
            StartCoroutine(WaitEndAnimationCoroutine());
        }
    }


    IEnumerator InitializationObjects()
    {
        yield return new WaitForSeconds(0.2f);

        buttons.gameObject.SetActive(false);
        gameNameText.gameObject.SetActive(false);
        scoreText.text = "0";
        scoreText.gameObject.SetActive(true);
    }


    IEnumerator StartMovePlatformCoroutune()
    {
        yield return new WaitForSeconds(0.3f);
        
        animatorPlatforms.SetBool(getHashCondition, true);
        GetComponent<SpawnPlatforms>().enabled = true;
    }


    IEnumerator WaitEndAnimationCoroutine()
    {
        yield return new WaitForSeconds(animatorPlatforms.GetCurrentAnimatorStateInfo(0).length);

        hero.transform.SetParent(null);
        animatorPlatforms.enabled = false;
    }
}
