using UnityEngine;

public class FocusOnPlayer : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        transform.LookAt(player.transform);
        transform.Rotate(0, 180, 0);
    }

    void Update()
    {
        transform.LookAt(player.transform);
        transform.Rotate(0, 180, 0);
    }
}
