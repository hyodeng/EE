using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int InvenSize = 6;
    ItemSlot[] slots = null;

    public int SlotCount => slots.Length;
    public ItemSlot this[int index] => slots[index]; 
    public Inventory(int size = InvenSize)
    {
        slots = new ItemSlot[size];     
        for (int i = 0; i < size; i++)
        {
            slots[i] = new ItemSlot();
        }
    }
    public bool AddItem(uint id)
    {
        return AddItem(ItemDataManager.Inst.ItemData[id]);
    }
    public bool AddItem(ItemCode code)
    {
        return AddItem(ItemDataManager.Inst.ItemData[code]);
    }
    public bool AddItem(ItemData data)
    {
        bool result = false;
        //Debug.Log($"인벤토리에 {data.itemName}을 추가합니다");

        ItemSlot target = FindSameItem(data);   // 같은 종류의 아이템이 인벤토리에 있는지 찾기
        if (target != null)
        {
            // 같은 종류의 아이템이 있으니 1만 증가시킨다.
            target.IncreaseSlotItem();
            result = true;
            //Debug.Log($"{data.itemName}을 하나 증가시킵니다.");
        }
        else
        {
            // 같은 종류의 아이템이 없다.
            ItemSlot empty = FindEmptySlot();    // 적절한 빈 슬롯 찾기
            if (empty != null)
            {
                empty.AssignSlotItem(data);      // 아이템 할당
                result = true;
                //Debug.Log($"아이템 슬롯에 {data.itemName}을 할당합니다.");
            }
            else
            {
                // 모든 슬롯에 아이템이 들어있다.(인벤토리가 가득찼다.)
                //Debug.Log($"실패 : 인벤토리가 가득찼습니다.");
            }
        }

        return result;
    }
    public bool AddItem(uint id, uint index)
    {
        return AddItem(ItemDataManager.Inst.ItemData[id], index);
    }
    public bool AddItem(ItemCode code, uint index)
    {
        return AddItem(ItemDataManager.Inst.ItemData[code], index);
    }
    public bool AddItem(ItemData data, uint index)
    {
        bool result = false;

        ItemSlot slot = slots[index];   // index번째의 슬롯 가져오기

        if (slot.IsEmpty())              // 찾은 슬롯이 비었는지 확인
        {
            slot.AssignSlotItem(data);  // 비어있으면 아이템 추가
            result = true;
            Debug.Log($"추가에 성공했습니다.");
        }
        else
        {
            if (slot.SlotItemData == data)  // 같은 종류의 아이템인가?
            {
                if (slot.IncreaseSlotItem() == 0)  // 들어갈 자리가 있는가?
                {
                    result = true;
                    Debug.Log($"아이템 갯수 증가에 성공했습니다.");
                }
                else
                {
                    Debug.Log($"실패 : 슬롯이 가득 찼습니다.");
                }
            }
            else
            {
                Debug.Log($"실패 : {index} 슬롯에는 다른 아이템이 들어있습니다.");
            }
        }

        return result;
    }
    private ItemSlot FindSameItem(ItemData itemData)
    {
        ItemSlot slot = null;
        for (int i = 0; i < SlotCount; i++)
        {
            if (slots[i].SlotItemData == itemData && slots[i].ItemCount < slots[i].SlotItemData.maxStackCount)
            {
                slot = slots[i];
                break;
            }
        }
        return slot;
    }
    public bool RemoveItem(uint slotIndex, uint decreaseCount = 1)
    {
        bool result = false;

        //Debug.Log($"인벤토리에서 {slotIndex} 슬롯의 아이템을 {decreaseCount}개 비웁니다.");
        if (IsValidSlotIndex(slotIndex))        // slotIndex가 적절한 범위인지 확인
        {
            ItemSlot slot = slots[slotIndex];
            slot.DecreaseSlotItem(decreaseCount);
            //Debug.Log($"삭제에 성공했습니다.");
            result = true;
        }
        else
        {
            Debug.Log($"실패 : 잘못된 인덱스입니다.");
        }

        return result;
    }
    private ItemSlot FindEmptySlot()
    {
        ItemSlot result = null;

        foreach (var slot in slots)  
        {
            if (slot.IsEmpty())    
            {
                result = slot;      
                break;
            }
        }
        return result;
    }
    public void TempRemoveItem(uint from, uint count = 1, bool equiped = false)
    {
        if (IsValidAndNotEmptySlot(from))  // from이 절절한 슬롯이면
        {
            ItemSlot slot = slots[from];
            slot.DecreaseSlotItem(count);   // from 슬롯에서 해당 갯수만큼 감소            
        }
    }
    private bool IsValidSlotIndex(uint index) => (index < SlotCount);
    
    private bool IsValidAndNotEmptySlot(uint index)
    {
        ItemSlot testSlot = null;

        return (IsValidSlotIndex(index) && !testSlot.IsEmpty());
    }
    public void PrintInventory()
    {
        // 현재 인벤토리 내용을 콘솔창에 출력하는 함수

        string printText = "[";
        for (int i = 0; i < SlotCount - 1; i++)         // 슬롯이 전체6개일 경우 0~4까지만 일단 추가(5개추가)
        {
            if (slots[i].SlotItemData != null)
            {
                printText += $"{slots[i].SlotItemData.itemName}({slots[i].ItemCount})";
            }
            else
            {
                printText += "(빈칸)";
            }
            printText += ",";
        }
        ItemSlot slot = slots[SlotCount - 1]; 
        if (!slot.IsEmpty())
        {
            printText += $"{slot.SlotItemData.itemName}({slot.ItemCount})]";
        }
        else
        {
            printText += "(빈칸)]";
        }

        Debug.Log(printText);
    }
}
