using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptMessage;

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        //override this method in derived classes to implement specific interaction logic
    }
}
