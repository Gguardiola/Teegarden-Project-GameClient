
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbandonShip : TriggerEvent
{
    public GameObject playerSpawner;
    private bool hasTriggered = false;
    public CameraFade cameraFadeEffect;
    public CameraRenderingPresets cameraRenderingPresets;
    public GameObject[] objectsToEnable;
    protected override void OnEnterTrigger()
    {
        if (playerSpawner == null)
        {
            playerSpawner = GameObject.Find("PlayerSpawner");
        }
        
        if (hasTriggered) return;
        hasTriggered = true;
        SoundManager.Instance.PlaySFX("PhantomEnters");
        StartCoroutine(WaitAndTeleport());
        
    
    }

    private IEnumerator WaitAndTeleport()
    {
        SoundManager.Instance.PlaySFX("ShipTakeoff");
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
        yield return new WaitForSeconds(15f);
        cameraFadeEffect.FadeIn();

        yield return new WaitForSeconds(5f);
        CharacterController cc = triggerObject.GetComponent<CharacterController>();
        cc.enabled = false;
        triggerObject.transform.position = playerSpawner.transform.position;
        cc.enabled = true;
        LevelContext.Instance.SetLevelToStation();
        GameObject.Find("PlayerUI").GetComponent<PlayerUI>().UIUpdateLevelName();
        cameraRenderingPresets.SetInteriorPreset();
        cameraFadeEffect.FadeOut();      
          

    }

}
