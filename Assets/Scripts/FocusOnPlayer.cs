using UnityEngine;

public class FocusOnPlayer : MonoBehaviour
{
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.LookAt(player.transform);
        transform.Rotate(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        transform.Rotate(0, 180, 0);
    }
}
