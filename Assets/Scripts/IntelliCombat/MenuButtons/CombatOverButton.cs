using UnityEngine;

namespace IntelliCombat.MenuButtons
{
    public class CombatOverButton : MenuButton
    {
        public override string Name { get; } = "CombatOverButton";
        private bool isPlayerWinner = false;
        
        public void SetWinner(bool isWinner)
        {
            isPlayerWinner = isWinner;
        }
        
        public void ContinueNextScene()
        {
            if (isPlayerWinner)
            {
                //UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }
    }
}