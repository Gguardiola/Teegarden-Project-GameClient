using UnityEngine;

using UnityEngine;
using System.Collections;

public class UpnDown : MonoBehaviour
{
    private Transform transformToAnimate;
    public float moveInterval = 2f;
    public float moveSpeed = 1f;
    public float moveHeight = 1f;

    void Start()
    {
        transformToAnimate = GetComponent<Transform>();
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Vector3 startPos = transformToAnimate.position;
        Vector3 targetPos = startPos + Vector3.up * moveHeight;

        while (true)
        {

            while (Vector3.Distance(transformToAnimate.position, targetPos) > 0.01f)
            {
                transformToAnimate.position = Vector3.MoveTowards(transformToAnimate.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(moveInterval);

            while (Vector3.Distance(transformToAnimate.position, startPos) > 0.01f)
            {
                transformToAnimate.position = Vector3.MoveTowards(transformToAnimate.position, startPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(moveInterval);
        }
    }
}
