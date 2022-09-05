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
    protected virtual void Awake()  // �������̵� �����ϵ��� virtual �߰�
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();    // ������ ǥ�ÿ� �̹��� ������Ʈ ã�Ƴ���
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
            // �� ���Կ� �������� ������� ��
            itemImage.sprite = itemSlot.SlotItemData.sprite;  // ������ �̹��� �����ϰ�
            itemImage.color = Color.white;  // �������ϰ� �����
            countText.text = itemSlot.ItemCount.ToString();


        }
        else
        {
            // �� ���Կ� �������� ���� ��
            itemImage.sprite = null;        // ������ �̹��� �����ϰ�
            itemImage.color = Color.clear;  // �����ϰ� �����
        }
    }


    //������ �̹����� �ҷ����� �Լ�

    
    //�˾�â�� ����� ���⸦ �ٲ�ðڽ��ϱ�? 
    //yes�� ������ �ٲ�� 

    public void OnPointerClick(PointerEventData eventData)
    {
        // ���콺 ���� ��ư Ŭ���� ��
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // �׳� Ŭ���� ��Ȳ
            if (!ItemSlot.IsEmpty())
            {
                //�÷��̾� ���Ⱑ �ٲ�� �Լ�
            }
        }
    }
}
