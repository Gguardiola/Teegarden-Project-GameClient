using UnityEngine;

public class Keypad : Interactable
{
    [SerializeField] 
    private GameObject door;
    private bool doorOpen;

    protected override void Interact()
    {
        SoundManager.Instance.PlaySFX("DefaultInteractable");
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }
}
