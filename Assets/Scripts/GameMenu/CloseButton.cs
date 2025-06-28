using System;
using UnityEngine;

public class CloseButton : GameMenuButton
{
    public GameObject currentWindow;

    public override void OnClick()
    {
        CleanUp();
    }

    public override void CleanUp()
    {
       base.CleanUp();
        if (currentWindow != null)
        {
            currentWindow.SetActive(false);

        }
    }
}
