using Unity.VisualScripting;
using UnityEngine;

public class QuitGameButton : GameMenuButton
{
    
    public override void OnClick()
    {
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
    public override void CleanUp()
    {
        //Do nothing :)
    }
}
