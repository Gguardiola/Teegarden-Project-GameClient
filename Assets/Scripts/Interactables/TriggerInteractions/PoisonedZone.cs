using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonedZone : TriggerEvent
{
        private Coroutine coroutine;
        protected override void OnEnterTrigger()
        {
                coroutine = StartCoroutine(HurtOverTime());
        }
        
        private IEnumerator HurtOverTime()
        {
                while (true)
                {
                        triggerObject.GetComponent<PlayerHealth>().TakeDamage(3);
                        yield return new WaitForSeconds(1f);
                }
        }

        protected override void OnExitTrigger()
        {
                if (coroutine != null)
                {
                        StopCoroutine(coroutine);
                }
        }
}