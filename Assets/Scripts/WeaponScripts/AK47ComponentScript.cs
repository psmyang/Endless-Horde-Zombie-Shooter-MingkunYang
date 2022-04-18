using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47ComponentScript : WeaponComponentScript
{
    Vector3 HitLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void FireWeapon()
    {
        if (weaponStats.bulletsInClip > 0 && !isReloading && !weaponHandle.playerController.isRunning)
        {
            base.FireWeapon();
            if (MuzzleParticle)
            {
                MuzzleParticle.Play();
            }
            Ray screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));

            if (Physics.Raycast(screenRay, out RaycastHit hit, weaponStats.fireDistance, weaponStats.weaponHitLayers))
            {
                HitLocation = hit.point;

                DealDamage(hit);

                Vector3 hitDirection = hit.point - mainCamera.transform.position;

                Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * weaponStats.fireDistance, Color.red, 1.0f);
            }
            print("Bullet in clip: " + weaponStats.bulletsInClip);
        }
        else if (weaponStats.bulletsInClip <= 0)
        {
            weaponHandle.StartReloading();
        }
    }

    void DealDamage(RaycastHit hitInfo)
    {
        IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
        damageable?.TakeDamage(weaponStats.weaponDamage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(HitLocation, 0.2f);
    }
}
