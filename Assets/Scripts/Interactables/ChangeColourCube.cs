using UnityEngine;

public class ChangeColourCube : Interactable
{
    MeshRenderer mesh;

    public Color[] colors;

    private int colourIndex;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material.color = Color.red;
    }

    protected override void Interact()
    {
        colourIndex++;
        if (colourIndex >= colors.Length -1)
        {
            colourIndex = 0;
        }
        mesh.material.color = colors[colourIndex];
    }
}
