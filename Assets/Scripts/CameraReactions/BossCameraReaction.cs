using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class BossCameraReaction : MonoBehaviour
{
    
    private Camera playerCamera;
    [SerializeField]
    private Volume sceneVolume;
    private LensDistortion lensDistortion;
    private float targetScale = 1.0f;
    private float targetIntensity = 0.2f;
    private float lerpSpeed = 5f;
    private float hue;
    private float targetHue = 0.8f;
    void Start()
    {
        playerCamera = GetComponent<Camera>();
        hue = 0.7f;

        if (sceneVolume.profile.TryGet(out lensDistortion))
        {
            lensDistortion.scale.Override(0.15f);
            lensDistortion.intensity.Override(-1f);
        }
    }

    void Update()
    {
        if (playerCamera != null)
        {
            hue = Mathf.Lerp(hue, targetHue, 1f * Time.deltaTime);

            if (Mathf.Abs(hue - targetHue) < 0.001f)
            {
                targetHue = (targetHue == 0.8f) ? 0.7f : 0.8f;
            }

            playerCamera.backgroundColor = Color.HSVToRGB(hue, 1f, 1f);
        }

        if (lensDistortion != null)
        {
            float current = lensDistortion.scale.value;
            float newValue = Mathf.Lerp(current, targetScale, lerpSpeed * Time.deltaTime);
            lensDistortion.scale.Override(newValue);
            current = lensDistortion.intensity.value;
            newValue = Mathf.Lerp(current, targetIntensity, lerpSpeed * Time.deltaTime);
            lensDistortion.intensity.Override(newValue);
        }
    }
}
