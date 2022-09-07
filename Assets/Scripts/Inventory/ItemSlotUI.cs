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
    protected TextMeshProUGUI countText;
    Inventory inven;
    PopUpMenu popUpMenu;

    Image equipment;
    string eName;

    public uint ID { get => id; }
    public ItemSlot ItemSlot { get => itemSlot; }
    protected virtual void Awake()  // 오버라이드 가능하도록 virtual 추가
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();    // 아이템 표시용 이미지 컴포넌트 찾아놓기
        countText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        
        popUpMenu = FindObjectOfType<PopUpMenu>();
        
        equipment = GameObject.Find("Equipment").transform.Find("EquipImage").GetComponent<Image>();

    }


    private void Start()
    {
        inven = new Inventory();

        string partner1 = File.ReadAllText(Application.dataPath + "/Resources/Json/" + $"/PlayerParts.json");
        jobject = JObject.Parse(partner1);

        //기존 장비 보여주기
        eName= jobject["5"].ToString();
        equipment.sprite = Resources.Load<Sprite>($"Character/Weapons/{eName.Replace(".png", "")}");
    }


    public void Initialize(uint newID, ItemSlot targetSlot)
    {
        invenUI = ItemDataManager.Inst.InvenUI;

        id = newID;
        itemSlot = targetSlot;
        itemSlot.onSlotItemChange = Refresh; 
    }

    private void Update()
    {
        if (popUpMenu.isChange)
        {
            ChangeEqipment();
        }
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

    GameObject equipmentObj;


    public void OnPointerClick(PointerEventData eventData)
    {
        // 마우스 왼쪽 버튼 클릭일 때
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // 그냥 클릭한 상황
            if (!ItemSlot.IsEmpty())
            {
                //1. 팝업창 : 기존장비 완전 삭제 새장비 사용할 지 Q
                popUpMenu.OnOffSwitch();

                //2. save 버튼 누르면 장비 바뀜
                equipmentObj = eventData.pointerCurrentRaycast.gameObject;
            }
        }
    }

    JObject jobject = new JObject();
    JObject equip = new JObject();

    public void ChangeEqipment()
    {
        Sprite temp = equipment.sprite;

        if (equipmentObj != null)
        {

            Debug.Log(this.name);
            //새 이미지 추가
            equipment.sprite = equipmentObj.GetComponent<Image>().sprite;
            uint tempNum = uint.Parse(equipmentObj.name.Substring(8));

            //나중에 오류 확인 필요 : 슬롯UI 이미지만 사라지게
            equipmentObj.GetComponentInParent<Image>().sprite = null;
            popUpMenu.isChange = false;

        }


        jobject.Remove("5");
        jobject.Add("5", $"{equipment.sprite.name}.png");

        string partner = JsonConvert.SerializeObject(jobject, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + $"/PlayerParts.json", partner);
        /*{GameManager.Inst.partnerCount}*/

        Debug.Log($"eqipment_{equipment.sprite.name}장비 추가 ");
    }

}
