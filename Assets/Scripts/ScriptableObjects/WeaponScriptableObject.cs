using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 2)]
public class WeaponScriptableObject : EquippableScriptableScript
{
    public WeaponStats weaponStats;

    public override void UseItem(PlayerController playerController)
    {
        if (equipped)
        {
            //unequip here and remove weapon from controller
            playerController.weaponHandler.UnEquipWeapon();
        }
        else
        {
            //invoke OnWeaponEquipped and equip weapon from weapon holder on playercontroller
            playerController.weaponHandler.EquipWeapon(this);
            PlayerEvents.InvokeOnWeaponEquipped(itemPrefab.GetComponent<WeaponComponentScript>());
        }

        base.UseItem(playerController);
    }
}
