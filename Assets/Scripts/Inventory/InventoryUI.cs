using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    CanvasGroup canvasGroup;

    PlayerInputActions inputActions;

    public GameObject slotPrefab;

    Player player;

    Inventory inven;

    ItemSlotUI[] slotUIs;

    Transform slotParent;
    private void Awake()
    {
        // �̸� ã�Ƴ���
        canvasGroup = GetComponent<CanvasGroup>();
        slotParent = transform.Find("ItemSlots");
        inputActions = new PlayerInputActions();
    }
    public void InitializeInventory(Inventory newInven)
    {
        inven = newInven;   //��� �Ҵ�
        if (Inventory.InvenSize != newInven.SlotCount)    // �⺻ ������� �ٸ��� �⺻ ����UI ����
        {
            // ���� ����UI ���� ����
            ItemSlotUI[] slots = GetComponentsInChildren<ItemSlotUI>();
            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
            }

            // ���� �����
            slotUIs = new ItemSlotUI[inven.SlotCount];
            for (int i = 0; i < inven.SlotCount; i++)
            {
                GameObject obj = Instantiate(slotPrefab, slotParent);
                obj.name = $"{slotPrefab.name}_{i}";            // �̸� �����ְ�
                slotUIs[i] = obj.GetComponent<ItemSlotUI>();
                slotUIs[i].Initialize((uint)i, inven[i]);       // �� ����UI�鵵 �ʱ�ȭ
            }
        }
        else
        {
            // ũ�Ⱑ ���� ��� ����UI���� �ʱ�ȭ�� ����
            slotUIs = slotParent.GetComponentsInChildren<ItemSlotUI>();
            for (int i = 0; i < inven.SlotCount; i++)
            {
                slotUIs[i].Initialize((uint)i, inven[i]);
            }
        }

       
      

        RefreshAllSlots();  // ��ü ����UI ����
    }

    /// <summary>
    /// ��� ������ Icon�̹����� ����
    /// </summary>
    private void RefreshAllSlots()
    {
        foreach (var slotUI in slotUIs)
        {
            slotUI.Refresh();
        }
    }

}
