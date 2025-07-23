using System;
using UnityEngine;

public class GameMenuButton : MonoBehaviour
{
    public GameObject backgroundObject;
    private GameObject defaultWindowComponent;
    public Transform inCameraBeacon;
    public Transform offCameraBeacon;
    private Transform backgroundTransform;
    public bool isSimpleMenuButton = false;
    
    private void Awake()
    {
        defaultWindowComponent = GameObject.Find("DefaultWindowComponent");
        if (!isSimpleMenuButton)
        {
            backgroundTransform = backgroundObject.GetComponent<Transform>();    
        }
        
    }
    protected void SetBackgroundObjectOnScreen()
    {
        backgroundTransform.position = inCameraBeacon.position;
        if (defaultWindowComponent != null)
        {
            defaultWindowComponent.SetActive(false);
        }
    }

    private void SetBackgroundObjectOffScreen()
    {
        if (backgroundTransform != null)
        {
            backgroundTransform.position = offCameraBeacon.position;            
        }
        if (defaultWindowComponent != null)
        {
            defaultWindowComponent.SetActive(true);
        }
    }
    
    public virtual void CleanUp()
    {
        SetBackgroundObjectOffScreen();
    }

    public virtual void OnClick()
    {
        //Do nothing :)
    }
}