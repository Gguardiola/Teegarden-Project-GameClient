
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbandonShip : TriggerEvent
{
    public GameObject playerSpawner;
    private bool hasTriggered = false;
    public CameraFade cameraFadeEffect;
    protected override void OnEnterTrigger()
    {
        if (playerSpawner == null)
        {
            playerSpawner = GameObject.Find("PlayerSpawner");
        }
        
        if (hasTriggered) return;
        hasTriggered = true; 
        StartCoroutine(WaitAndTeleport());
        
    
    }

    private IEnumerator WaitAndTeleport()
    {
        yield return new WaitForSeconds(5f);
        cameraFadeEffect.FadeIn();

        yield return new WaitForSeconds(5f);
        CharacterController cc = triggerObject.GetComponent<CharacterController>();
        cc.enabled = false;
        triggerObject.transform.position = playerSpawner.transform.position;
        cc.enabled = true;
        LevelContext.Instance.currentLevelName = "Laboratory";
        GameObject.Find("PlayerUI").GetComponent<PlayerUI>().UIUpdateLevelName();
        cameraFadeEffect.FadeOut();      
          

    }

}
