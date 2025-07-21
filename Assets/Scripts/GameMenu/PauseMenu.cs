using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public bool IsPaused() 
    {
        return isPaused;
    }
    public void TogglePauseMenu()
    {
        if (isPaused)
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
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void AbandonGame()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu"); //TODO: logica de abandonament
        
        
    }
    
}