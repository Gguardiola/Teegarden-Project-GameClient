using UnityEngine.SceneManagement;

public class StartNewGameButton : GameMenuButton
{
    public override void OnClick()
    {
        StartNewGame();
    }
    
    private void StartNewGame()
    {
        SoundManager.Instance.StopMusic("MainMenu");
        SceneManager.LoadScene("SampleScene");
    }
    
}
