using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool isFiring;
    public bool isReloading;
    public bool isRunning;
    public bool isJumping;
    public bool isAiming;
    public bool isInventoryOpen;

    public InventoryComponent inventory;
    public GameUIController UIController;
    public WeaponHandleScript weaponHandler;

    private void Awake()
    {
        if (inventory == null)
        {
            inventory = GetComponent<InventoryComponent>();
        }

        if (UIController == null)
        {
            UIController = FindObjectOfType<GameUIController>();
        }

        if (weaponHandler == null)
        {
            weaponHandler = GetComponent<WeaponHandleScript>();
        }
    }

    public void OnInventory(InputValue value)
    {
        isInventoryOpen = !isInventoryOpen;
        AppEvents.InvokeOnMouseCursorEnable(isInventoryOpen);
        OpenInventory(isInventoryOpen);
    }

    private void OpenInventory(bool b)
    {
        if (b)
        {
            UIController.EnableInventoryMenu();
        }
        else
        {
            UIController.EnableGameMenu();
        }
    }

    
}
