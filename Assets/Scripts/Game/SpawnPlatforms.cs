using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    [SerializeField] GameObject platform, bridge;
    [SerializeField] float speed = 5;


    private GameObject platformInstantiate, platformBase;
    private Vector3 platformPosition;
    private bool hasPlace = false;


    private void OnEnable()
    {
        SpawnPlatform();
        hasPlace = false;
    }


    void Update()
    {
        if (platformInstantiate.transform.position != platformPosition && !hasPlace)
        {
            // Moving the platform to target 
            platformInstantiate.transform.position = Vector3.MoveTowards(platformInstantiate.transform.position, platformPosition, Time.deltaTime * speed);
        }
        else
        {
            if (platformInstantiate.transform.position == platformPosition)
            {
                hasPlace = true;

                // Activate script creating bridge
                bridge.GetComponent<CreateBridge>().enabled = true;
            }
        }

        if (GameObject.Find("Hero").GetComponent<MovePlatforms>().IsMoved && GameObject.Find("Hero").GetComponent<MoveHero>().IsNextPlatform)
        {
            SpawnPlatform();

            if(GameObject.Find("Hero").GetComponent<MovePlatforms>().IsMoved)
            {
                GameObject.Find("Hero").GetComponent<MoveHero>().IsNextPlatform = false;
            }

            hasPlace = false;
        }
    }


    private void SpawnPlatform()
    {
        // Get the child element of the prefab 
        platformBase = platform.transform.Find("Platform Base").gameObject;

        // Initialization of the platform behind the screen 
        platformPosition = new Vector3(Random.Range(-1f, 2.5f), 1f, 0f);
        platformInstantiate = Instantiate(platform, new Vector3(7f, 1f, 0f), Quaternion.identity) as GameObject;
        platformBase.transform.localScale = new Vector3(Random.Range(0.5f, 1.5f), platformBase.transform.localScale.y, platformBase.transform.localScale.z);
    }
}
