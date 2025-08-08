using UnityEngine;

public class BossComputerOFF : Interactable
{
    [SerializeField]
    public GameObject computerONPrefab;
    
    protected override void Interact()
    {
        SoundManager.Instance.PlaySFX("DefaultInteractable");

        gameObject.SetActive(false);
        if (computerONPrefab != null)
        {
            GameObject computerON = Instantiate(computerONPrefab, transform.localPosition, Quaternion.identity);
            computerON.transform.position = transform.position;
            computerON.transform.rotation = transform.rotation;
            Destroy(gameObject);
        }
    }
}
