using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossComputerON : Interactable
{
    PlayerInventory playerInventory;
    void Start()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();
    }
    
    protected override void Interact()
    {
        SoundManager.Instance.PlaySFX("DefaultInteractable");

        if (playerInventory.GetItem("Item_disk") != null)
        {
            if (playerInventory.DeleteItem("Item_disk"))
            {
                LoadCombatScene();
            }
        }
        else
        {
            StartCoroutine(DiskNotFoundPrompt());
        }
    }
    
    private IEnumerator DiskNotFoundPrompt()
    {
        this.promptMessage = "You need the disk to access the computer!";
        this.promptTextColor = Color.red;
        yield return new WaitForSeconds(2);
        this.promptTextColor = Color.white;
        promptMessage = "Insert disk";
    }

    private void LoadCombatScene()
    {
        SceneManager.LoadScene("BossCombatScene");
    }
}
