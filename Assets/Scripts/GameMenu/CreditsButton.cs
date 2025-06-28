using TMPro;
using UnityEngine;

public class CreditsButton : GameMenuButton
{

    public GameObject creditsLabel;

    public override void OnClick()
    {
        ShowCredits();
        SetBackgroundObjectOnScreen();
    }

    private void ShowCredits()
    {
        creditsLabel.SetActive(true);
    }

    public override void CleanUp()
    {
        base.CleanUp();
        creditsLabel.SetActive(false); }
    
}
