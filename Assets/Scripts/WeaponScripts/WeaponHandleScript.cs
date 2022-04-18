using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandleScript : MonoBehaviour
{
    [Header("Weapon To Hold"), SerializeField]
    GameObject weaponToSpawn;

    public PlayerController playerController;
    Animator playerWeaponAnimator;
    Sprite aimCrossSprite;
    public WeaponComponentScript equippedWeapon;

    [SerializeField]
    GameObject weaponSocket;
    [SerializeField]
    Transform gripSocketLocationIK;

    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");

    bool firingPressed = false;

    GameObject spawnedWeapon;
    public WeaponScriptableObject startingWeaponScriptableObj;

    public WeaponAmmoUI weaponAmmoUI;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerWeaponAnimator = GetComponent<Animator>();
        //EquipWeapon(startingWeaponScriptableObj);
        //spawnedWeapon = Instantiate(weaponToSpawn, weaponSocket.transform.position, weaponSocket.transform.rotation, weaponSocket.transform);
        playerController.inventory.AddItem(startingWeaponScriptableObj, 1);
        //startingWeaponScriptableObj.UseItem(playerController);
        //equippedWeapon = spawnedWeapon.GetComponent<WeaponComponentScript>();
        //For spawning with a weapon
        //equippedWeapon.Initialize(this, startingWeaponScriptableObj);
        //PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (equippedWeapon)
        {
            playerWeaponAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            playerWeaponAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gripSocketLocationIK.transform.position);
        }
    }

    public void OnFire(InputValue value)
    {
        firingPressed = value.isPressed;
        

        if (!equippedWeapon) return;

        if (firingPressed)
        {
            
            StartFiring();
            
        }
        else
        {
            StopFiring();
        }
    }

    public void StartFiring()
    {
        if (!equippedWeapon) return;

        if (equippedWeapon.weaponStats.bulletsInClip <= 0)
        {
            StartReloading();
            return;
        }

        playerWeaponAnimator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiring();
        
    }

    public void StopFiring()
    {
        if (!equippedWeapon) return;

        playerWeaponAnimator.SetBool(isFiringHash, false);
        playerController.isFiring = false;
        equippedWeapon.StopFiring();
    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        StartReloading();
    }

    public void StartReloading()
    {
        if (!equippedWeapon) return;

        if (equippedWeapon.isReloading || equippedWeapon.weaponStats.bulletsInClip == equippedWeapon.weaponStats.clipSize) return;

        if (playerController.isFiring)
        {
            StopFiring();
        }
        if (equippedWeapon.weaponStats.totalBullets <= 0) return;

        playerWeaponAnimator.SetBool(isReloadingHash, true);
        equippedWeapon.StartReloading();

        InvokeRepeating(nameof(StopReloading), 0, 0.1f);
    }

    public void StopReloading()
    {
        if (!equippedWeapon) return;

        if (playerWeaponAnimator.GetBool(isReloadingHash)) return;

        playerController.isReloading = false;
        equippedWeapon.StopReloading();
        playerWeaponAnimator.SetBool(isReloadingHash, false);
        CancelInvoke(nameof(StopReloading));
    }

    public void EquipWeapon(WeaponScriptableObject weapon)
    {
        if (!weapon) return;

        spawnedWeapon = Instantiate(weapon.itemPrefab, weaponSocket.transform.position, weaponSocket.transform.rotation, weaponSocket.transform);

        if (!spawnedWeapon) return;

        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponentScript>();
        if (!equippedWeapon) return;

        equippedWeapon.Initialize(this, weapon);
        PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
        gripSocketLocationIK = equippedWeapon.weaponGripLocation;
        weaponAmmoUI.OnWeaponEquipped(equippedWeapon);
    }

    public void UnEquipWeapon()
    {
        if (!equippedWeapon) return;

        Destroy(equippedWeapon.gameObject);
        equippedWeapon = null;
    }
}
