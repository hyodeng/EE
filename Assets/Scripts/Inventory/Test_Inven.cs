using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Inven : MonoBehaviour
{
    void Start()
    {
        Inventory inven = new Inventory();
        InventoryUI invenUI = FindObjectOfType<InventoryUI>();
        invenUI.InitializeInventory(inven);
        inven.AddItem(ItemCode.Weapon,0);
        inven.PrintInventory();
    }
}
