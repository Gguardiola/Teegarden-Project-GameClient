using System.Collections;
using UnityEngine;

public static class InputBlocker
{
    private static bool blocked = false;

    public static bool isBlocked => blocked;

    public static void BlockForOneFrame()
    {
        CoroutineHelper.StartStaticCoroutine(BlockCoroutine());
    }

    private static IEnumerator BlockCoroutine()
    {
        blocked = true;
        yield return null;
        blocked = false;
    }
    
    public static void BlockForSeconds(float seconds)
    {
        CoroutineHelper.StartStaticCoroutine(BlockCoroutine(seconds));
    }

    private static IEnumerator BlockCoroutine(float seconds)
    {
        blocked = true;
        yield return new WaitForSecondsRealtime(seconds);
        blocked = false;
    }
}