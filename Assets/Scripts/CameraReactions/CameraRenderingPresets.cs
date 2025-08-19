using UnityEngine;

public class CameraRenderingPresets : MonoBehaviour
{
    
    public Camera cam;
    
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        
        SetShipPreset();
    }
    
    public void SetShipPreset()
    {
        if (cam != null)
        {
            cam.farClipPlane = 5000f;
            RenderSettings.fog = false;
        }
    }
    
    public void SetInteriorPreset()
    {
        if (cam != null)
        {
            cam.farClipPlane = 1000f;
            RenderSettings.fog = true;
            RenderSettings.fogDensity = 0.05f;
        }
    }
        
}