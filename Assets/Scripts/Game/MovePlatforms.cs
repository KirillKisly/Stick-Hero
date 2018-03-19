using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatforms : MonoBehaviour
{

    [SerializeField] GameObject emptyGameObject;
    [SerializeField] float speed = 10f;


    private bool isMoved = false, isNextPlatform = false;
    private Vector3 target;


    public bool IsMoved
    {
        get
        {
            return isMoved;
        }
        set
        {
            isMoved = value;
        }
    }


    void Start()
    {
        target = BindingComponent.BC.HeroStartPosition;
    }


    void Update()
    {
        isNextPlatform = GetComponent<MoveHero>().IsNextPlatform;

        if (isNextPlatform)
        {
            if (transform.position != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
                isMoved = false;
            }
            else
            {
                if (transform.position == target)
                {
                    emptyGameObject.transform.SetParent(null);
                    isMoved = true;
                    GameObject.Find("Detect Clicks").GetComponent<CreateBridge>().IsActivated = true;
                }
            }
        }
    }
}
