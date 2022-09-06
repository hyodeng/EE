using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    uint id;

    protected ItemSlot itemSlot;
    InventoryUI invenUI;
    protected Image itemImage;
    Inventory inven;
    protected TextMeshProUGUI countText;

    public System.Action OnOffPopup;


    public uint ID { get => id; }
    public ItemSlot ItemSlot { get => itemSlot; }
    protected virtual void Awake()  // �������̵� �����ϵ��� virtual �߰�
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();    // ������ ǥ�ÿ� �̹��� ������Ʈ ã�Ƴ���
        countText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        //popupMenu = FindObjectOfType<PopUpMenu>();
        //popupMenu.gameObject.SetActive(false);
        OnOffPopup?.Invoke();
    }

    private void Start()
    {
        PopEquipmentImage();
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

    string shieldName;

    //���� ����̹���
    public void PopEquipmentImage()
    {
        //shield : ���ҽ�/�ǵ� ���� 9�� �̹���
        int shieldIndex = Random.Range(0, 10);
        shieldName = $"Shield_{shieldIndex}";

        //weapon : ���ҽ�/���� ���� sword 6�� �̹��� �� 
        int weaponIndex = Random.Range(0, 7);
        string weaponName = $"Sword_{weaponIndex}";
    }

    
    public void OnPointerClick(PointerEventData eventData)
    {
        // ���콺 ���� ��ư Ŭ���� ��
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // �׳� Ŭ���� ��Ȳ
            if (!ItemSlot.IsEmpty())
            {
                //�˾�â ����
                OnOffPopup?.Invoke();

                //�÷��̾� ���Ⱑ �ٲ�� �Լ�
                TestChange();
            }
        }
    }

    JObject jobject = new JObject();

    public void TestChange()
    {
        string partner1 = File.ReadAllText(Application.dataPath + "/Resources/Json/" + $"/PartnerParts_1.json");
        jobject = JObject.Parse(partner1);

        jobject.Add("weapon", shieldName);
        string partner = JsonConvert.SerializeObject(jobject, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + $"/PartnerParts_1.json", partner);
        /*{GameManager.Inst.partnerCount}*/

        Debug.Log($"��Ʈ��_1�� ��� �߰� ");
    }

}
