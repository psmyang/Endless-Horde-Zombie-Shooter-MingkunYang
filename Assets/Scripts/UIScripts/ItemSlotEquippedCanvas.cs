using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotEquippedCanvas : MonoBehaviour
{
    EquippableScriptableScript equipableSCriptObj;
    [SerializeField] private Image EnabledImage;

    private void Awake()
    {
        HideWidget();
    }

    public void ShowWidget()
    {
        gameObject.SetActive(true);
    }

    public void HideWidget() 
    {
        gameObject.SetActive(false);
    }


    public void Initialize(ItemScript item)
    {
        if (!(item is EquippableScriptableScript eqItem)) return;

        equipableSCriptObj = eqItem;
        ShowWidget();
        equipableSCriptObj.OnEquipStatusChange += OnEquipmentChange;
        OnEquipmentChange();

    }

    private void OnEquipmentChange()
    {
        EnabledImage.gameObject.SetActive(equipableSCriptObj.equipped);
    }

    private void OnDisable()
    {
        if (equipableSCriptObj)
            equipableSCriptObj.OnEquipStatusChange -= OnEquipmentChange;
    }
}
