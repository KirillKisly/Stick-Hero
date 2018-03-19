using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHero : MonoBehaviour
{
    [SerializeField] GameObject hero, emptyGameObject;
    [SerializeField] float speed = 2f;


    private bool isMoving = false, isNextPlatform = false;
    private Vector3 toHeroTarget;


    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
        set
        {
            isMoving = value;
        }
    }


    public bool IsNextPlatform
    {
        get
        {
            return isNextPlatform;
        }
        set
        {
            isNextPlatform = value;
        }
    }


    public Vector3 ToHeroTarget
    {
        set
        {
            toHeroTarget = value;
        }
    }


    void Update()
    {
        if (isMoving)
        {
            if (hero.transform.position != toHeroTarget)
            {
                hero.transform.position = Vector3.MoveTowards(hero.transform.position, toHeroTarget, Time.deltaTime * speed);
            }
            else
            {
                StartCoroutine(MovePlatform());
            }
        }
    }


    IEnumerator MovePlatform()
    {
        yield return new WaitForEndOfFrame();

        // Put platform as a child and put in the original position
        GameObject[] platforms, bridges, platformsBase, indicatorPlatforms;

        platforms = GameObject.FindGameObjectsWithTag("Platform");
        bridges = GameObject.FindGameObjectsWithTag("Bridge");
        platformsBase = GameObject.FindGameObjectsWithTag("PlatformBase");
        indicatorPlatforms = GameObject.FindGameObjectsWithTag("IndicatorPlatform");

        foreach (GameObject p in platforms)
        {
            p.transform.SetParent(emptyGameObject.transform);
            p.tag = "Untagged";
        }
        foreach (GameObject pb in platformsBase)
        {
            pb.tag = "Untagged";
        }
        foreach (GameObject b in bridges)
        {
            b.transform.SetParent(emptyGameObject.transform);
            b.tag = "Untagged";
            //Destroy(b, 1f);
        }
        foreach(GameObject ip in indicatorPlatforms)
        {
            ip.tag = "Untagged";
            ip.SetActive(false);
        }

        emptyGameObject.transform.SetParent(hero.transform);

        isNextPlatform = true;
        isMoving = false;
    }
}
