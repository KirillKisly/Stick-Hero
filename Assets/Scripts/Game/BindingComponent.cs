using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindingComponent : MonoBehaviour
{
    const float HERO_POSITION_X = -2.3f;
    const float HERO_POSITION_Y = -1.75f;
    const float BRIDGE_POSITION_X = -2.1f;
    const float BRIDGE_POSITION_Y = -2f;
    const float PLATFORM_POSITION_X = -3f;
    const float PLATFORM_POSITION_Y = 1f;
    const float PLATFORM_SCALE_X = 2f;
    const float PLATFORM_SCALE_Y= 4f;


    public static BindingComponent BC;


    private Vector3 heroStartPosition = new Vector3(HERO_POSITION_X, HERO_POSITION_Y, 0f);
    private Vector3 bridgeStartPosition = new Vector3(BRIDGE_POSITION_X, BRIDGE_POSITION_Y, 0f);
    private Vector3 platformPosition = new Vector3(PLATFORM_POSITION_X, PLATFORM_POSITION_Y, 0f);
    private Vector3 platformScale = new Vector3(PLATFORM_SCALE_X, PLATFORM_SCALE_Y, 3f);


    public Vector3 HeroStartPosition
    {
        get
        {
            return heroStartPosition;
        }
    }


    public Vector3 BridgeStartPosition
    {
        get
        {
            return bridgeStartPosition;
        }
    }


    public Vector3 PlatformPosition
    {
        get
        {
            return platformPosition;
        }
    }


    public Vector3 PlatformScale
    {
        get
        {
            return platformScale;
        }
    }


    void Awake()
    {
        if (BC != null)
            GameObject.Destroy(BC);
        else
            BC = this;

        DontDestroyOnLoad(this);
    }
}
