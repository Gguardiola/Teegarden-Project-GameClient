using UnityEngine;

public class BossComputerOFF : Interactable
{
    [SerializeField]
    public GameObject computerONPrefab;
    
    protected override void Interact()
    {
        gameObject.SetActive(false);
        if (computerONPrefab != null)
        {
            GameObject computerON = Instantiate(computerONPrefab, transform.localPosition, Quaternion.identity);
            //set the same position as the current game object before destroying it
            computerON.transform.position = transform.position;
            computerON.transform.rotation = transform.rotation;
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Computer ON prefab is not assigned.");
        }
    }
}
