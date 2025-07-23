using UnityEngine;

public class RotateLoop : MonoBehaviour
{
    private Transform transformToRotate;
    public float rotateSpeed = 20f;
    void Start()
    {
        transformToRotate = GetComponent<Transform>();
    }

    void Update()
    {
        transformToRotate.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotateSpeed);
    }
}
