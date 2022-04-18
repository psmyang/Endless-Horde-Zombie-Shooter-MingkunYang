using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponAmmoUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI WeaponNameText;
    [SerializeField]
    TextMeshProUGUI WeaponAmmoText;

    [SerializeField]
    WeaponComponentScript weaponComponent;

    private void OnEnable()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEquipped;
    }

    private void OnDisable()
    {
        PlayerEvents.OnWeaponEquipped -= OnWeaponEquipped;
    }

    public void OnWeaponEquipped(WeaponComponentScript _weaponComponent)
    {
        weaponComponent = _weaponComponent;
    }

    // Update is called once per frame
    void Update()
    {
        if (!weaponComponent)
        {
            return;
        }

        WeaponNameText.text = weaponComponent.weaponStats.weaponName;
        WeaponAmmoText.text = weaponComponent.weaponStats.bulletsInClip + "/" + weaponComponent.weaponStats.totalBullets;
    }
}
