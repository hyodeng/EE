using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class PartnerSelectView : MonoBehaviour
{
    Button btnWarrior;
    Button btnMage;
    Button btnCleric;
    Button btnThief;
    Button btnPopstar;
    Button btnChef;

    Customized customized;

    Character partner;

    public SavePlayerData partnerData = new SavePlayerData();

    PartnerBoard partnerboard;

    PopupController popupController;

    PartnerSelectBoard selectboard;
    TextMeshProUGUI partnerName1;
    TextMeshProUGUI skillName1;
    TextMeshProUGUI PartnerExplanation1;

    TextMeshProUGUI partnerName2;
    TextMeshProUGUI skillName2;
    TextMeshProUGUI PartnerExplanation2;

    TextMeshProUGUI partnerName3;
    TextMeshProUGUI skillName3;
    TextMeshProUGUI PartnerExplanation3;

    public System.Action onPartnerSelectBoard;
    public System.Action offPartnerSelectBoard;


    private void Awake()
    {
        //transform.getchild(0).getcomponent<button> 은 왜 안될까?
        btnWarrior = GameObject.Find("Btn_Warrior").GetComponent<Button>();
        btnMage = GameObject.Find("Btn_Mage").GetComponent<Button>();
        btnCleric = GameObject.Find("Btn_Cleric").GetComponent<Button>();
        btnThief = GameObject.Find("Btn_Thief").GetComponent<Button>();
        btnPopstar = GameObject.Find("Btn_Popstar").GetComponent<Button>();
        btnChef = GameObject.Find("Btn_Chef").GetComponent<Button>();

        partnerboard = GameObject.Find("PartnerBoard").GetComponent<PartnerBoard>();
        selectboard = GameObject.Find("PartnerSelectBoard").GetComponent<PartnerSelectBoard>();
        //동료1의 직업, 설명
        partnerName1 = selectboard.transform.Find("Partner1").GetChild(1).GetComponent<TextMeshProUGUI>();
        skillName1 = selectboard.transform.Find("Partner1").GetChild(2).GetComponent<TextMeshProUGUI>();
        PartnerExplanation1 = selectboard.transform.Find("Partner1").GetChild(3).GetComponent<TextMeshProUGUI>();

        //동료2의 직업, 설명
        partnerName2 = selectboard.transform.Find("Partner2").GetChild(1).GetComponent<TextMeshProUGUI>();
        skillName2 = selectboard.transform.Find("Partner2").GetChild(2).GetComponent<TextMeshProUGUI>();
        PartnerExplanation2 = selectboard.transform.Find("Partner2").GetChild(3).GetComponent<TextMeshProUGUI>();

        //동료3의 직업, 설명
        partnerName3 = selectboard.transform.Find("Partner3").GetChild(1).GetComponent<TextMeshProUGUI>();
        skillName3 = selectboard.transform.Find("Partner3").GetChild(2).GetComponent<TextMeshProUGUI>();
        PartnerExplanation3 = selectboard.transform.Find("Partner3").GetChild(3).GetComponent<TextMeshProUGUI>();

        popupController = GameObject.Find("PopupController").GetComponent<PopupController>();

    }

    private void Start()
    {

        customized = FindObjectOfType<Customized>();

        this.gameObject.SetActive(false);

        LoadCharacterData();

        btnWarrior.onClick.AddListener(() => DataSetUp(CharacterType.Warrior));
        btnMage.onClick.AddListener(() => DataSetUp(CharacterType.Mage));
        btnCleric.onClick.AddListener(() => DataSetUp(CharacterType.Cleric));
        btnThief.onClick.AddListener(() => DataSetUp(CharacterType.Thief));
        btnPopstar.onClick.AddListener(() => DataSetUp(CharacterType.Popstar));
        btnChef.onClick.AddListener(() => DataSetUp(CharacterType.Chef));

        popupController.OnEnabledpartnerSelectView += OnOffViewState;

        DataManager.Instance.SavePartnerToJson = SavePartnerData;
    }


    private void OnOffViewState()
    {
        if (this.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    void LoadCharacterData()
    {
        //Character.Json에서 데이터 꺼내옴
        string character = File.ReadAllText(Application.dataPath + "/Resources/Json/" + "/Character.json");
        partner = JsonUtility.FromJson<Character>(character);
    }

    private void DataSetUp(CharacterType type)
    {
        partnerboard.onPartnerSelectBoardOpen?.Invoke();

        switch (type)
        {
            case CharacterType.Warrior:
                //파트너의 파츠별 이미지 
                ClearParts();
                SetPartnerParts(type);
                InitializePartenrData(type);
                RefreshDataPartnerSelectBoard(type);

                break;
            case CharacterType.Mage:
                ClearParts();
                SetPartnerParts(type);
                InitializePartenrData(type);
                RefreshDataPartnerSelectBoard(type);

                break;
            case CharacterType.Cleric:
                ClearParts();
                SetPartnerParts(type);
                InitializePartenrData(type);
                RefreshDataPartnerSelectBoard(type);
                break;
            case CharacterType.Thief:
                ClearParts();
                SetPartnerParts(type);
                InitializePartenrData(type);
                RefreshDataPartnerSelectBoard(type);

                break;
            case CharacterType.Popstar:
                ClearParts();
                SetPartnerParts(type);
                InitializePartenrData(type);
                RefreshDataPartnerSelectBoard(type);
                break;
            case CharacterType.Chef:
                ClearParts();
                SetPartnerParts(type);
                InitializePartenrData(type);
                RefreshDataPartnerSelectBoard(type);
                break;
            default:
                Debug.Log("파트너 직업 선택 오류");
                break;
        }
    }

    //파츠 이미지 지우기_수정중
    void ClearParts()
    {
        for (int i = 0; i < customized.parts.Length; i++)
        {
            //customized.SetParts(i, "");
        }
    }

    //동료의 파츠별 이미지 보여줌
    private void SetPartnerParts(CharacterType type)
    {
        for (int i = 0; i < customized.parts.Length; i++)
        {
            if (partner.character[(int)type].parts[i] != "")
            {
                customized.SetParts(i, partner.character[(int)type].parts[i].ToString());
            }
            else
            {
                //customized.SetParts(i, partner.character[(int)type].parts[2].ToString());

                // Debug.Log($"{type}의 빈 파츠 번호: {i}");
            }
        }
    }


    private void InitializePartenrData(CharacterType type)
    {
        partnerData._name = partner.character[(int)type]._name;
        partnerData.desc = partner.character[(int)type].desc;
        partnerData.maxhp = partner.character[(int)type].maxhp;
        partnerData.hp = partner.character[(int)type].hp;
        partnerData.maxmp = partner.character[(int)type].maxmp;
        partnerData.mp = partner.character[(int)type].mp;
        partnerData.attack = partner.character[(int)type].attack;
        partnerData.attackup = partner.character[(int)type].attackup;
        partnerData.magic = partner.character[(int)type].magic;
        partnerData.magicup = partner.character[(int)type].magicup;
        partnerData.defence = partner.character[(int)type].defence;
        partnerData.defenceup = partner.character[(int)type].defenceup;
        partnerData.speed = partner.character[(int)type].speed;
        partnerData.speedup = partner.character[(int)type].speedup;
        partnerData.skillname = partner.character[(int)type].skillname;
        partnerData.skilldesc = partner.character[(int)type].skilldesc;

        //partnerData.positionX;
        //partnerData.positionY;

    }

    void SavePartnerData()
    {
        string data = JsonUtility.ToJson(partnerData);
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + $"/Partner_{PartnerBoard.partnerCount}.json", data);
        Debug.Log($"동료_{PartnerBoard.partnerCount} 스탯 저장");
    }


    void RefreshDataPartnerSelectBoard(CharacterType type)
    {
        if (PartnerBoard.partnerCount == 0)
        {
            onPartnerSelectBoard?.Invoke();

            partnerName1.text = partner.character[(int)type]._name;
            skillName1.text = partner.character[(int)type].skillname;
            PartnerExplanation1.text = partner.character[(int)type].skilldesc;
        }
        else if (PartnerBoard.partnerCount == 1)
        {
            onPartnerSelectBoard?.Invoke();
            partnerName2.text = partner.character[(int)type]._name;
            skillName2.text = partner.character[(int)type].skillname;
            PartnerExplanation2.text = partner.character[(int)type].skilldesc;
        }
        else if (PartnerBoard.partnerCount == 2)
        {
            onPartnerSelectBoard?.Invoke();
            partnerName3.text = partner.character[(int)type]._name;
            skillName3.text = partner.character[(int)type].skillname;
            PartnerExplanation3.text = partner.character[(int)type].skilldesc;
        }
        else
        {
            Debug.Log("동료선택모음창 오류");
        }

    }



}
