using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateBridge : MonoBehaviour
{
    [SerializeField] private GameObject bridge, bridgeParent, bridgeTrigger, hero, firstPlatform;
    [SerializeField] private float speed = 5f;
    [SerializeField] Text bonusScore;

    private GameObject bridgeParentInstantiate, bridgeInstantiate, bridgeTriggerInstantiate;
    private Vector3 bridgePosition;
    private bool isClicked = false, isActivated = false;


    public bool IsActivated
    {
        get
        {
            return isActivated;
        }
        set
        {
            isActivated = value;
        }
    }


    private void OnEnable()
    {
        isActivated = true;
    }


    void FixedUpdate()
    {
        // Increase the bridge
        if (isClicked)
        {
            bridgeInstantiate.transform.localScale += new Vector3(0f, speed * Time.deltaTime, 0f);
            bridgeInstantiate.transform.localPosition += new Vector3(0f, (speed * Time.deltaTime) / 2, 0f);
        }
    }


    private void OnMouseDown()
    {
        if (isActivated)
        {
            isClicked = true;
            SpawnBridge();
        }
    }


    private void OnMouseUp()
    {
        if (isActivated)
        {
            isClicked = false;
            isActivated = false;

            // Initialize the parent bridge and the appended child
            bridgeParentInstantiate = Instantiate(bridgeParent, bridgePosition, Quaternion.identity) as GameObject;
            bridgeInstantiate.transform.SetParent(bridgeParentInstantiate.transform);

            // Add an empty object with a trigger on the platform and put it right
            bridgeTriggerInstantiate = Instantiate(bridgeTrigger, bridgePosition, Quaternion.identity);
            bridgeTriggerInstantiate.transform.SetParent(bridgeParentInstantiate.transform);
            bridgeTriggerInstantiate.transform.localPosition = new Vector3(0f, bridgeInstantiate.transform.localScale.y - 0.01f, 0f);

            StartCoroutine(WaitDownBridgeCoroutine());
        }
    }


    IEnumerator WaitDownBridgeCoroutine()
    {
        float waitTime = bridgeParentInstantiate.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(waitTime + 0.2f);

        // Is checked ground
        if (bridgeTriggerInstantiate.GetComponent<CheckGround>().IsCheckedGround)
        {
            hero.GetComponent<MoveHero>().IsMoving = true;
            FindCoordinatesPlatform();

            if (bridgeTriggerInstantiate.GetComponent<CheckGround>().IsCheckedIndicator)
            {
                bonusScore.gameObject.SetActive(true);
                yield return new WaitForSeconds(1.5f);
                bonusScore.gameObject.SetActive(false);
                bridgeTriggerInstantiate.GetComponent<CheckGround>().IsCheckedIndicator = false;
            }
        }
        else
        {
            GetComponent<AudioSource>().Play();
            GameObject.Find("Scene").GetComponent<Lose>().HasLosed = true;

            // Start animation death
        }
    }
   

    private void SpawnBridge()
    {
        bridgePosition = BindingComponent.BC.BridgeStartPosition;
        bridgeInstantiate = Instantiate(bridge, bridgePosition, Quaternion.identity) as GameObject;
    }


    private void FindCoordinatesPlatform()
    {
        // Finding the platform and the transfer of coordinates hero
        GameObject getPlatformBase = GameObject.FindWithTag("PlatformBase");
        GameObject getPlatform = GameObject.FindWithTag("Platform");

        float positionX = getPlatform.transform.localPosition.x + (getPlatformBase.transform.localScale.x / 2) - 0.3f;
        hero.GetComponent<MoveHero>().ToHeroTarget = new Vector3(positionX, hero.transform.localPosition.y, hero.transform.localPosition.z);

        firstPlatform.tag = "Platform";
    }
}
