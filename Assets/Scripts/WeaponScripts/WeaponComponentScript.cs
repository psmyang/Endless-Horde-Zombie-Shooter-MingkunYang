using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponentScript : MonoBehaviour
{
    public Transform weaponGripLocation;
    public Transform MuzzleFiringFXLocation;

    protected WeaponHandleScript weaponHandle;
    [SerializeField]
    protected ParticleSystem MuzzleParticle;

    [SerializeField]
    public WeaponStats weaponStats;

    public bool isFiring = false;
    public bool isReloading = false;

    protected Camera mainCamera;
    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
        if (MuzzleParticle)
        {
            MuzzleParticle.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(WeaponHandleScript handle, WeaponScriptableObject weaponScriptable)
    {
        weaponHandle = handle;

        if (weaponScriptable)
        {
            weaponStats = weaponScriptable.weaponStats;
        }
    }

    public virtual void StartFiring()
    {
        isFiring = true;

        if (weaponStats.repeating)
        {
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate);
            
        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiring()
    {
        isFiring= false;
        CancelInvoke(nameof(FireWeapon));
        if (MuzzleParticle.isPlaying)
        {
            MuzzleParticle.Stop();
        }
    }

    protected virtual void FireWeapon()
    {
        SoundManager.PlaySound("fire");
        print("firing");
        weaponStats.bulletsInClip--;
        //switch statement based on the firing pattern
    }

    public virtual void StartReloading()
    {
        SoundManager.PlaySound("reloading");
        isReloading = true;
        ReloadWeapon();
    }

    public virtual void StopReloading()
    {
        isReloading= false;
    }

    protected void ReloadWeapon()
    {
        if (MuzzleParticle.isPlaying)
        {
            MuzzleParticle.Stop();
        }

        // if there's a firing effect, hide it here
        int bulletsToReload = weaponStats.totalBullets - (weaponStats.clipSize - weaponStats.bulletsInClip);

        // -------------- COD style reload, subtract bullets ----------------------
        if (bulletsToReload > 0)
        {
            weaponStats.totalBullets = bulletsToReload;
            weaponStats.bulletsInClip = weaponStats.clipSize;
        }
        else
        {
            weaponStats.bulletsInClip += weaponStats.totalBullets;
            weaponStats.totalBullets = 0;
        }
    }
}

public enum WeaponType
{
    NONE,
    PISTOL,
    RIFLE,
    GRANADES
}

public enum WeaponFiringPattern
{
    SINGLE_SHOT,
    THREE_BURST,
    SEMI_AUTO,
    FULL_AUTO
}

[System.Serializable]
public struct WeaponStats
{
    public string weaponName;
    public float weaponDamage;
    public int bulletsInClip;
    public int clipSize;
    public int totalBullets;
    public float fireStartDelay;
    public float fireRate;
    public float fireDistance;
    public bool repeating;
    public LayerMask weaponHitLayers;
    public WeaponFiringPattern firingPattern;
    public WeaponType weaponType;
}