using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public GunData gunData;
    [HideInInspector]public Transform cameraTransform;
    public PlayerLook playerLook;
    private Transform gunPosition;
    protected float currentAmmo = 0f;
    protected float lastAmmo = 0f;
    private Vector3 targetNotReadyPosition = new Vector3(18f, 0, 0);
    private Vector3 startGunRotation;
    private Vector3 startGunPosition;
    private bool isReloading = false; 
    private float nextTimeToFire = 0f;
    public Transform gunMuzzle;
    public GameObject bulletHitParticlePrefab;
    public GameObject bulletHolePrefab;
    private Coroutine recoilCoroutine;

    
    public virtual void Start()
    {
        currentAmmo = gunData.ammountOfBullets;
        cameraTransform = playerLook.cam.transform;
        gunPosition = GetComponent<Transform>();
        startGunRotation = gunPosition.transform.localRotation.eulerAngles;
        startGunPosition = gunPosition.transform.localPosition;
    }

    public virtual void Update()
    {
        playerLook.ResetRecoil(gunData);
        
    }

    public void TryReload()
    {
        if (!isReloading && currentAmmo < gunData.ammountOfBullets)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        StartCoroutine(AnimateGunNotReady());
        yield return new WaitForSeconds(gunData.reloadTime);
        currentAmmo = gunData.ammountOfBullets;
        lastAmmo = gunData.ammountOfBullets;
        isReloading = false;
        StartCoroutine(AnimateGunReady());
    }

    private IEnumerator AnimateGunReady()
    {
        if (!isReloading)
        {
            float time = 0f;
            while (time < 2f)
            {
                time += Time.deltaTime * 5f;
                gunPosition.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(targetNotReadyPosition), Quaternion.Euler(startGunRotation), time);
                yield return null;
            }
            
        }
    }
    
    private IEnumerator AnimateGunNotReady()
    {
        if (isReloading || currentAmmo == 0f)
        {
            float time = 0f;
            while (time < 2f)
            {
                time += Time.deltaTime * 5f;
                gunPosition.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(startGunRotation),
                    Quaternion.Euler(targetNotReadyPosition), time);
                yield return null;
            }      
        }
    }

    private void StartGunRecoil()
    {
        if (recoilCoroutine != null)
        {
            StopCoroutine(recoilCoroutine);
        }
        recoilCoroutine = StartCoroutine(ApplyTransformRecoil());
    }
    
    private IEnumerator ApplyTransformRecoil()
    {
        Vector3 targetPosition = new Vector3(startGunPosition.x, startGunPosition.y, startGunPosition.z + -0.2f);
        float time = 0f;
        while (time < 2f)
        {
            time += Time.deltaTime * 5f;
            gunPosition.transform.localPosition = Vector3.Lerp(targetPosition, startGunPosition, time);
            yield return null;
        }
    }

    public void TryShoot()
    {
        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0f)
        {
            StartCoroutine(AnimateGunNotReady());
            return;
        }

        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + (1/gunData.fireRate);
            HandleShoot();
        }
    }

    private void HandleShoot()
    {
        currentAmmo--;
        Shoot();
        playerLook.ApplyRecoil(gunData);
        StartGunRecoil();
    }
    
    public abstract void Shoot();
}
