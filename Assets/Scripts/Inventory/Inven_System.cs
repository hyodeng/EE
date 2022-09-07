using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inven_System : MonoBehaviour
{
    PlayerInputAction invenAction;

    private void Awake()
    {
        invenAction = new();
    }
    void Start()
    {
        Inventory inven = new Inventory();
        InventoryUI invenUI = FindObjectOfType<InventoryUI>();
        invenUI.InitializeInventory(inven);

        //¿Œµ¶Ω∫ 
        inven.AddItem(ItemCode.Weapon, 0);
        inven.AddItem(ItemCode.Weapon, 1);
        
        inven.AddItem(ItemCode.Weapon, 2);
        inven.AddItem(ItemCode.Armor, 3);
        inven.PrintInventory();
        //inven.RemoveItem(3);
        //inven.PrintInventory();
    }
    private void OnEnable()
    {
        invenAction.Inven.Enable();
        invenAction.Inven.OnOff.performed += OnOffSwitch;
    }

    private void OnDisable()
    {
        invenAction.Inven.OnOff.performed -= OnOffSwitch;
        invenAction.Inven.Disable();
    }
    private void OnOffSwitch(InputAction.CallbackContext obj)
    {
        ItemDataManager.Inst.InvenUI.InventoryOnOffSwitch();
    }

}
