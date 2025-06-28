using UnityEngine;

public class RotateLoop : MonoBehaviour
{
    private Transform transformToRotate;
    public float rotateSpeed = 20f;
    void Start()
    {
        transformToRotate = GetComponent<Transform>();
        if (transformToRotate == null)
        {
            Debug.LogError("RotateLoop: No Transform component found on the GameObject.");
        }
    }

    void Update()
    {
        transformToRotate.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotateSpeed);
    }
}
