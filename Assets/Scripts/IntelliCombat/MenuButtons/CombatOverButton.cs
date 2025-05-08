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
        
        //change to the next scene depnding on the winner
        public void ContinueNextScene()
        {
            if (isPlayerWinner)
            {
                //load next scene level
                UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
            }
            else
            {
                //back to main menu WIP
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }
    }
}