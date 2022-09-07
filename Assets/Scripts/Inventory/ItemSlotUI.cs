using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
    protected virtual void Awake()  // 오버라이드 가능하도록 virtual 추가
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();    // 아이템 표시용 이미지 컴포넌트 찾아놓기
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
            countText.text = "";
        }
    }


    
    public void OnPointerClick(PointerEventData eventData)
    {
        // 마우스 왼쪽 버튼 클릭일 때
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // 그냥 클릭한 상황
            if (!ItemSlot.IsEmpty())
            {
                //팝업창 띄우기
                OnOffPopup?.Invoke();

                //플레이어 무기가 바뀌는 함수
                TestChange();
            }
        }
    }

    string shieldName;
    string weaponName;

    //랜덤 장비이미지
    public void PopEquipmentImage()
    {
        //shield : 리소스/실드 폴더 9개 이미지
        int shieldIndex = Random.Range(0, 10);
        shieldName = $"Shield_{shieldIndex}";

        //weapon : 리소스/웨폰 폴더 sword 6개 이미지 중 
        int weaponIndex = Random.Range(0, 7);
        weaponName = $"Sword_{weaponIndex}";
    }

    JObject jobject = new JObject();
    JArray array = new JArray();
    JToken jToken;

    public void TestChange()
    {
        string partner1 = File.ReadAllText(Application.dataPath + "/Resources/Json/" + $"/PlayerParts.json");
        jobject = JObject.Parse(partner1);


        jobject.Add("weapon", array);
        string partner = JsonConvert.SerializeObject(jobject, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + $"/PlayerParts.json", partner);
        /*{GameManager.Inst.partnerCount}*/

        Debug.Log($"파트너_1번 장비 추가 ");
    }

}
