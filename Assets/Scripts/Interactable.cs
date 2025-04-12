using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // sets or unsetts interactive state
    public bool useEvents;
    [SerializeField]
    public string promptMessage;

    public void BaseInteract()
    {
        if (useEvents)
        {
            GetComponent<InteractionEvent>().onInteract.Invoke();
        }
        Interact();
    }

    protected virtual void Interact()
    {
        //override this method in derived classes to implement specific interaction logic
    }
}
