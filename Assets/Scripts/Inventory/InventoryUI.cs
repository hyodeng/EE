using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public System.Action OnInventoryOpen;
    public System.Action OnInventoryClose;
    private void Awake()
    {
        // 미리 찾아놓기
        canvasGroup = GetComponent<CanvasGroup>();
        slotParent = transform.Find("ItemSlots");
        inputActions = new PlayerInputActions(); 
        //Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        //closeButton.onClick.AddListener(Close);
    }

    private void Start()
    {
        Close();
    }

    public void InitializeInventory(Inventory newInven)
    {
        inven = newInven;   //즉시 할당
        if (Inventory.InvenSize != newInven.SlotCount)    // 기본 사이즈와 다르면 기본 슬롯UI 삭제
        {
            // 기존 슬롯UI 전부 삭제
            ItemSlotUI[] slots = GetComponentsInChildren<ItemSlotUI>();
            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
            }

            // 새로 만들기
            slotUIs = new ItemSlotUI[inven.SlotCount];
            for (int i = 0; i < inven.SlotCount; i++)
            {
                GameObject obj = Instantiate(slotPrefab, slotParent);
                obj.name = $"{slotPrefab.name}_{i}";            // 이름 지어주고
                slotUIs[i] = obj.GetComponent<ItemSlotUI>();
                slotUIs[i].Initialize((uint)i, inven[i]);       // 각 슬롯UI들도 초기화
            }
        }
        else
        {
            // 크기가 같을 경우 슬롯UI들의 초기화만 진행
            slotUIs = slotParent.GetComponentsInChildren<ItemSlotUI>();
            for (int i = 0; i < inven.SlotCount; i++)
            {
                slotUIs[i].Initialize((uint)i, inven[i]);
            }
        }

        RefreshAllSlots();  // 전체 슬롯UI 갱신
    }

    /// <summary>
    /// 모든 슬롯의 Icon이미지를 갱신
    /// </summary>
    private void RefreshAllSlots()
    {
        foreach (var slotUI in slotUIs)
        {
            slotUI.Refresh();
        }
    }
    public void InventoryOnOffSwitch()
    {
        if (canvasGroup.blocksRaycasts)  // 캔버스 그룹의 blocksRaycasts를 기준으로 처리
        {
            Close();
        }
        else
        {
            Open();
        }
    }
    void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        OnInventoryOpen?.Invoke();
    }
    void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        OnInventoryClose?.Invoke();
    }
}
