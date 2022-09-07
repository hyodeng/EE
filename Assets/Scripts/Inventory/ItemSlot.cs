using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    ItemData slotItemData;
    uint itemCount;
    public ItemSlot() { }

    public System.Action onSlotItemChange;

    public ItemSlot(ItemData data, uint count)
    {
        slotItemData = data;
        itemCount = count;
    }
    public ItemData SlotItemData
    {
        get => slotItemData;
        private set
        {
            if (slotItemData != value)
            {
                slotItemData = value;
                onSlotItemChange?.Invoke(); 

            }
        }
    }
    public uint ItemCount
    {
        get => itemCount;
        private set
        {
            itemCount = value;
            onSlotItemChange?.Invoke();

        }
    }
    public ItemSlot(ItemSlot other)
    {
        slotItemData = other.SlotItemData;
        itemCount = other.ItemCount;
    }
    public uint IncreaseSlotItem(uint count = 1)
    {
        uint newCount = ItemCount + count;
        int overCount = (int)newCount - (int)SlotItemData.maxStackCount;
        if (overCount > 0)
        {
            ItemCount = SlotItemData.maxStackCount;
        }
        else
        {
            ItemCount = newCount;
            overCount = 0;
        }
        return (uint)overCount; 
    }

    public void DecreaseSlotItem(uint count = 1)
    {
        int newCount = (int)ItemCount - (int)count;
        if (newCount < 1)   // 최종적으로 갯수가 0이되면 완전 비우기
        {
            // 다 뺀다.
            ClearSlotItem();
        }
        else
        {
            ItemCount = (uint)newCount;
        }
    }
    public void AssignSlotItem(ItemData itemData, uint count = 1)
    {
        ItemCount = count;
        SlotItemData = itemData;
    }
    public void UseSlotItem(GameObject target = null)
    {
        IUsable usable = SlotItemData as IUsable;   // 이 아이템이 사용가능한 아이템인지 확인
        if (usable != null)
        {
            // 아이템이 사용가능하면
            usable.Use(target); // 아이템 사용하고
            DecreaseSlotItem(); // 갯수 하나 감소
        }
    }
    public bool IsEmpty()
    {
        return slotItemData == null;
    }
    public void ClearSlotItem()
    {
        SlotItemData = null;
        ItemCount = 0;
    }
}
