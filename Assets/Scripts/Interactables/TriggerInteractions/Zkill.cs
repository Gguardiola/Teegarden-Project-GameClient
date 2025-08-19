public class Zkill : TriggerEvent
{
        protected override void OnEnterTrigger()
        {
                triggerObject.GetComponent<PlayerHealth>().TakeDamage(999);
        }
}