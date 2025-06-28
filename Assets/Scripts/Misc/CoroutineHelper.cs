using UnityEngine;
using System.Collections;

public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineHelper _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void StartStaticCoroutine(IEnumerator coroutine)
    {
        if (_instance == null)
        {
            var go = new GameObject("CoroutineHelper");
            _instance = go.AddComponent<CoroutineHelper>();
            DontDestroyOnLoad(go);
        }
        _instance.StartCoroutine(coroutine);
    }
}