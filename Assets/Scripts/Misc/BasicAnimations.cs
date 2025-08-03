
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimations : MonoBehaviour
{
    private HashSet<GameObject> runningAnimations = new HashSet<GameObject>();

    public IEnumerator AnimationPulse(GameObject target, Vector3 originalScale, Vector3 targetScale, float duration)
    {
        if (target == null || runningAnimations.Contains(target))
            yield break;

        runningAnimations.Add(target);

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
        runningAnimations.Remove(target);
    }
}