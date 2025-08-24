using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public abstract class TriggerEvent : MonoBehaviour
{
    public GameObject triggerObject;
    void Start()
    {
        if (triggerObject == null)
        {
            triggerObject = GameObject.Find("Player");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == triggerObject)
        {
            OnEnterTrigger();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == triggerObject)
        {
            OnExitTrigger();
        }
    }

    protected virtual void OnEnterTrigger()
    {
        Debug.Log("collision with trigger detected");
    }
    
    protected virtual void OnExitTrigger()
    {
        Debug.Log("exiting trigger detected");
    }
}