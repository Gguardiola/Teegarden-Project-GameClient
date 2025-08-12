using UnityEngine;

public class SettingsButton : GameMenuButton
{
    public GameObject currentWindow;
    public override void OnClick()
    {
        currentWindow.SetActive(true);
        SettingsContext.Instance.TriggerSliders();
        SetBackgroundObjectOnScreen();

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
