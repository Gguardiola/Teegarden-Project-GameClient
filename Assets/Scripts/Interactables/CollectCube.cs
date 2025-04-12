using System;
using UnityEngine;

public class CollectCube : Interactable
{
    public GameObject particle;
    public GameObject cube;

    protected override void Interact()
    {
        Destroy(cube);
        Instantiate(particle, cube.transform.position, Quaternion.LookRotation(Vector3.up));
        
    }
}
