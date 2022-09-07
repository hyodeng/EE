using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Item Data", menuName = "Scriptable Object/Item Data - Armor", order = 3)]
public class ItemData_Armor : ItemData
{
    [Header("방어구 데이터")]
    public int defense = 5;
    public int hp = 10;
}
