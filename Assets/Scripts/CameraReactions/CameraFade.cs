using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
        public Image fadeImage;
        
        
        public void FadeIn()
        {
            StartCoroutine(FadeCoroutine(0, 1));
        }
        
        public void FadeOut()
        {
            StartCoroutine(FadeCoroutine(1, 0));
        }

        private IEnumerator FadeCoroutine(float startAlpha, float endAlpha)
        {
            //turn alpha from raw image to de endalpha using lerp
            float elapsedTime = 0f;
            float duration = 1f; // Duration of the fade in seconds
            Color color = fadeImage.color;
            color.a = startAlpha;
            fadeImage.color = color;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                color.a = alpha;
                fadeImage.color = color;
                yield return null;
            }
        }

}