using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{

    [SerializeField] GameObject scene, detectClicks, hero, emptyGameObject, platform, losePanel;
    [SerializeField] Text scoreText;


    private Vector3 positionStartHero;
    private GameObject platformInstantiate, platformBase;


    private void SpawnPlatform(Vector3 position, Vector3 scale)
    {
        // Get the child element of the prefab 
        platformBase = platform.transform.Find("Platform Base").gameObject;
        platformBase.transform.localScale = scale;

        // Initialization of the platform
        platformInstantiate = Instantiate(platform, position, Quaternion.identity) as GameObject;
    }


    private void DestroyAllChild()
    {
        for (int i = 0; i < emptyGameObject.transform.childCount; i++) 
        {
            // The platform in the initial screen is not removed
            if (emptyGameObject.transform.GetChild(i).name == "Platforms")
            {
                continue;
            }

            Destroy(emptyGameObject.transform.GetChild(i).gameObject);
        }
    }


    public void RestartTheGame()
    {    
        // Put character at position
        positionStartHero = BindingComponent.BC.HeroStartPosition;
        hero.transform.position = positionStartHero;

        // Deleted all the old game objects 
        StartCoroutine(MovePlatform());

        // Preparation of other components
        losePanel.SetActive(false);
        GameObject.Find("Scene").GetComponent<Lose>().HasLosed = false;
        GameObject.Find("Detect Clicks").GetComponent<CreateBridge>().IsActivated = true;
        scoreText.gameObject.SetActive(true);
        Score.score = 0;
        
        GetComponent<RestartGame>().enabled = false;
    }


    IEnumerator MovePlatform()
    {
        yield return new WaitForEndOfFrame();
        scene.GetComponent<SpawnPlatforms>().enabled = false;

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
        foreach (GameObject ip in indicatorPlatforms)
        {
            ip.tag = "Untagged";
            ip.SetActive(false);
        }

        // Create platform
        Vector3 platformPositionSecond = BindingComponent.BC.PlatformPosition;
        Vector3 scalePlatformSecond = BindingComponent.BC.PlatformScale;
        SpawnPlatform(platformPositionSecond, scalePlatformSecond);
        platformInstantiate.tag = "Untagged";

        scene.GetComponent<SpawnPlatforms>().enabled = true;

        emptyGameObject.transform.SetParent(null);
        DestroyAllChild();
        platformInstantiate.transform.SetParent(emptyGameObject.transform);
    }
}
