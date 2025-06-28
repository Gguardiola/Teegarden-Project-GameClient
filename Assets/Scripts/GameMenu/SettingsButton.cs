using UnityEngine;

public class SettingsButton : GameMenuButton
{
    
    public override void OnClick()
    {
        Debug.Log("Settings button clicked!");
        SetBackgroundObjectOnScreen();

    }
}
