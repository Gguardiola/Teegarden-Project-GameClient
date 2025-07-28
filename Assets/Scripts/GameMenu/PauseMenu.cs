using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    private bool _isPaused = false;
    public bool isPaused() 
    {
        return _isPaused;
    }
    public void TogglePauseMenu(InputActionMap currentGameplayMap, bool isLocked)
    {
        if (_isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
        CheckIfPaused(currentGameplayMap, isLocked);
    }
    private void PauseGame()
    {
        _isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    
    public void CheckIfPaused(InputActionMap currentGameplayMap, bool isLocked)
    {
        if (_isPaused)
        {
            currentGameplayMap.Disable();
        }
        else
        {
            currentGameplayMap.Enable();
            Cursor.lockState =  isLocked ? CursorLockMode.Locked : CursorLockMode.Confined;
            Cursor.visible = !isLocked;
        }
    }
    
    public void FreezeGame(InputActionMap   currentGameplayMap, InputActionMap menuMap, bool isLocked)
    {
        currentGameplayMap.Disable();
        menuMap.Disable();
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.Confined;
        Cursor.visible = !isLocked;
        Time.timeScale = 0f;
    }
    public void UnFreezeGame(InputActionMap currentGameplayMap, InputActionMap menuMap, bool isLocked)
    {
        currentGameplayMap.Enable();
        menuMap.Enable();
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.Confined;
        Cursor.visible = !isLocked;
        Time.timeScale = 1f;
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