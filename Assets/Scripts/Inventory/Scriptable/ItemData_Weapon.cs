using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item Data", menuName = "Scriptable Object/Item Data - Weapon", order = 2)]
public class ItemData_Weapon : ItemData, IEquipItem
{
    [Header("무기 데이터")]
    public int attack = 10;
    public int magic = 10;

    public void EquipItem()
    {

    }
}
