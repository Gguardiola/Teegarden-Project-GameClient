using System.Collections;
using TMPro;
using UnityEngine;

public class Bananagun : Gun
{
    public TextMeshPro ammoDisplay;
    private Color defaultDisplayColor;

    public override void Start()
    {
        base.Start();
        ammoDisplay = ammoDisplay.GetComponent<TextMeshPro>();
        defaultDisplayColor = ammoDisplay.GetComponent<TextMeshPro>().color;
        UpdateAmmoDisplay();
    }

    public override void Update()
    {
        base.Update();
        UpdateAmmoDisplay();
    }
    public override void Shoot()
    {
        UpdateAmmoDisplay();
        Vector3 target = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position,cameraTransform.forward, out hit, gunData.gunRange, gunData.targetLayerMask))
        {
            Debug.Log("Hit: " + hit.collider.name);
            target = hit.point;
        }

        StartCoroutine(BulletFire(target, hit));
    }

    private IEnumerator BulletFire(Vector3 target, RaycastHit hit)
    {
        GameObject bulletTrail = Instantiate(gunData.bulletTrailPrefab, gunMuzzle.position, Quaternion.identity);

        while (bulletTrail != null && Vector3.Distance(bulletTrail.transform.position, target) > 0.1f)
        {
            bulletTrail.transform.position = Vector3.MoveTowards(bulletTrail.transform.position, target, gunData.bulletSpeed * Time.deltaTime);
            yield return null;
        }
        
        Destroy(bulletTrail);
        if(hit.collider != null)
        {
            BulletHitVFX(hit);
        }
    }

    private void BulletHitVFX(RaycastHit hit)
    {
        Vector3 hitPoint = hit.point + hit.normal * 0.01f;
        GameObject bulletHole = Instantiate(bulletHolePrefab, hitPoint, Quaternion.LookRotation(hit.normal));
        GameObject hitParticle = Instantiate(bulletHitParticlePrefab, hitPoint, Quaternion.LookRotation(hit.normal));
        bulletHole.transform.parent = hit.collider.transform;
        hitParticle.transform.parent = hit.collider.transform;
        Destroy(hitParticle);
        Destroy(bulletHole, 5f);

    }
    
    private void UpdateAmmoDisplay()
    {
        if (currentAmmo > 0)
        {
            ammoDisplay.text = currentAmmo.ToString();
            ammoDisplay.color = defaultDisplayColor;
        }
        else
        {
            ammoDisplay.text = "0";
            ammoDisplay.color = Color.red;
        }   
    
    }
}
