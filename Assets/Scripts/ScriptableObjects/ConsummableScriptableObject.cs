using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable", order = 1)]
public class ConsummableScriptableObject : ItemScript
{
    public int itemEffect = 0;

    public override void UseItem(PlayerController playerController)
    {
        SetAmount(amountValue - 1);

        if (amountValue <= 0)
        {
            DeleteItem(playerController);
        }

    }
}
