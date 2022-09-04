using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    public uint id = 0;                   
    public string itemName = "æ∆¿Ã≈€"; 
    public GameObject prefab;
    public Sprite sprite;
    public uint maxStackCount = 1;
}
