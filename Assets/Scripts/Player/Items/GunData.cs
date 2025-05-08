using UnityEngine;
[CreateAssetMenu(fileName = "NewGunData", menuName = "Gun/GunData")]
public class GunData : ScriptableObject
{
    public string gunName;
    public LayerMask targetLayerMask;

    [Header("Gun Stats")] public float gunRange;
    public float fireRate;
    public float ammountOfBullets;
    public float reloadTime;
    public float recoilAmount;
    public Vector2 maxRecoil;
    public float recoilSpeed;
    public float resetRecoilSpeed;
    public GameObject bulletTrailPrefab;
    public float bulletSpeed;
    public float damage;

}
