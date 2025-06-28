using UnityEngine;

public class NewGameButton : GameMenuButton
{
    public GameObject currentWindow;
    public override void OnClick()
    {
        if (currentWindow != null)
        {
            SetBackgroundObjectOnScreen();
            currentWindow.SetActive(true);
        }

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
