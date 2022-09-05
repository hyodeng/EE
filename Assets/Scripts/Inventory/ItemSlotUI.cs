using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;


public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    uint id;

    protected ItemSlot itemSlot;
    InventoryUI invenUI;
    protected Image itemImage;
    Inventory inven;
    protected TextMeshProUGUI countText;

    public uint ID { get => id; }
    public ItemSlot ItemSlot { get => itemSlot; }
    protected virtual void Awake()  // 오버라이드 가능하도록 virtual 추가
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();    // 아이템 표시용 이미지 컴포넌트 찾아놓기
        countText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        
    }
    public void Initialize(uint newID, ItemSlot targetSlot)
    {
        invenUI = ItemDataManager.Inst.InvenUI;

        id = newID;
        itemSlot = targetSlot;
        itemSlot.onSlotItemChange = Refresh; 
    }
    public void Refresh()
    {
        if (itemSlot.SlotItemData != null)
        {
            // 이 슬롯에 아이템이 들어있을 때
            itemImage.sprite = itemSlot.SlotItemData.sprite;  // 아이콘 이미지 설정하고
            itemImage.color = Color.white;  // 불투명하게 만들기
            countText.text = itemSlot.ItemCount.ToString();


        }
        else
        {
            // 이 슬롯에 아이템이 없을 때
            itemImage.sprite = null;        // 아이콘 이미지 제거하고
            itemImage.color = Color.clear;  // 투명하게 만들기
        }
    }


    //랜덤한 이미지를 불러오는 함수

    
    //팝업창을 띄워서 무기를 바뀌시겠습니까? 
    //yes를 누르면 바뀌는 

    public void OnPointerClick(PointerEventData eventData)
    {
        // 마우스 왼쪽 버튼 클릭일 때
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // 그냥 클릭한 상황
            if (!ItemSlot.IsEmpty())
            {
                //플레이어 무기가 바뀌는 함수
            }
        }
    }
}
