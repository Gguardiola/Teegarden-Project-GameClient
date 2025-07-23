using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    private bool _isPaused = false;
    public bool isPaused() 
    {
        return _isPaused;
    }
    public void TogglePauseMenu()
    {
        if (_isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    
    private void PauseGame()
    {
        _isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void ResumeGame()
    {
        _isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void AbandonGame()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu"); //TODO: if necessary, gather game run stats and show it con the game over screen
        
        
    }
    
}