
using System.Collections;
using UnityEngine;

public class BasicAnimations : MonoBehaviour
{
    public IEnumerator AnimationPulse(GameObject target, Vector3 originalScale, Vector3 targetScale, float duration)
    {
        if (target != null)
        {
            Transform t = target.transform;

            float speed = duration;
            float threshold = 0.01f;
            while ((t.localScale - targetScale).magnitude > threshold)
            {
                t.localScale = Vector3.Lerp(t.localScale, targetScale, Time.deltaTime * speed);
                yield return null;
            }
            while ((t.localScale - originalScale).magnitude > threshold)
            {
                t.localScale = Vector3.Lerp(t.localScale, originalScale, Time.deltaTime * speed);
                yield return null;
            }
            t.localScale = originalScale;
        }
    }
}
