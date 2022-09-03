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

    //동료 인원 최대 : 3명?? 다시 확인 필요, TEST 필요
    SavePlayerData partnerData = new SavePlayerData();

    PartnerBoard partnerboard;
    PartnerSelectBoard selectboard;

    Button savebutton;

    bool IsReadySave = false;
    uint index;

    private void Awake()
    {
        //transform.find("Btn_Warrior").getcomponent<button> 은 왜 안될까?
        btnWarrior = GameObject.Find("Btn_Warrior").GetComponent<Button>();
        btnMage = GameObject.Find("Btn_Mage").GetComponent<Button>();
        btnCleric = GameObject.Find("Btn_Cleric").GetComponent<Button>();
        btnThief = GameObject.Find("Btn_Thief").GetComponent<Button>();
        btnPopstar = GameObject.Find("Btn_Popstar").GetComponent<Button>();
        btnChef = GameObject.Find("Btn_Chef").GetComponent<Button>();

        partnerboard = GameObject.Find("PartnerBoard").GetComponent<PartnerBoard>();
        selectboard = GameObject.Find("PartnerSelectBoard").GetComponent<PartnerSelectBoard>();

        savebutton = GameObject.Find("SaveButton").GetComponent<Button>();

    }

    private void Start()
    {    
        customized = FindObjectOfType<Customized>();

        //Character.Json에서 데이터 꺼내옴
        string character = File.ReadAllText(Application.dataPath + "/Resources/Json/" + "/Character.json");
        partner = JsonUtility.FromJson<Character>(character);

        btnWarrior.onClick.AddListener( () => DataSetUp(CharacterType.Warrior));
        btnMage.onClick.AddListener(() => DataSetUp(CharacterType.Mage));
        btnCleric.onClick.AddListener(() => DataSetUp(CharacterType.Cleric));
        btnThief.onClick.AddListener(() => DataSetUp(CharacterType.Thief));
        btnPopstar.onClick.AddListener(() => DataSetUp(CharacterType.Popstar));
        btnChef.onClick.AddListener(() => DataSetUp(CharacterType.Chef));
        savebutton.onClick.AddListener(SelectCompletePartner);
    }



    private void SelectCompletePartner()
    {

        //스태틱변수 : PartnerBoard.partnerCount;
        //DataManager.Instance.PartnerBoard. ?? partnerCount 로는 접근이 안되나?

        IsReadySave = true;
        SavePartnerDataToJson();

    }



    private void DataSetUp(CharacterType type)
    {
        partnerboard.OnPartnerSelectBoardOpen?.Invoke();

        switch (type)
        {
            case CharacterType.Warrior:
                //파트너의 파츠별 이미지 
                SetPartnerParts(type);
                //파트너셀렉트보드의 내용을 CharacterJson에서 가져와 변경
                UpdatePartnerSelectBoard(type);
                //선택한 파트너정보를 json저장으로 초기화 
                InitializePartenrData(type);
                //save버튼을 누르면 json으로 저장
                SavePartnerDataToJson();
                break;
            case CharacterType.Mage:
                SetPartnerParts(type);
                UpdatePartnerSelectBoard(type);
                InitializePartenrData(type);
                SavePartnerDataToJson();
                break;
            case CharacterType.Cleric:
                SetPartnerParts(type);
                UpdatePartnerSelectBoard(type);
                InitializePartenrData(type);
                break;
            case CharacterType.Thief:
                SetPartnerParts(type);
                UpdatePartnerSelectBoard(type);
                InitializePartenrData(type);
                break;
            case CharacterType.Popstar:
                SetPartnerParts(type);
                UpdatePartnerSelectBoard(type);
                InitializePartenrData(type);
                break;
            case CharacterType.Chef:
                SetPartnerParts(type);
                UpdatePartnerSelectBoard(type);
                InitializePartenrData(type);
                break;
            default:
                Debug.Log("파트너 직업 선택 오류");
                break;
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

        Debug.Log("파트너 스탯 입력");

        //partnerData.partnerNum = 
        //partnerData.positionX;
        //partnerData.positionY;

    }

    //Sava버튼을 누르면 저장
    void SavePartnerDataToJson()
    {
        if (IsReadySave)
        {
            Debug.Log($"동료정보Json 저장 전: {PartnerBoard.partnerCount}");
            string data = JsonUtility.ToJson(partnerData);
            File.WriteAllText(Application.dataPath + "/Resources/Json/" + $"/Partner_{PartnerBoard.partnerCount}.json", data);
            Debug.Log("파트너 스탯 초기화");
        }
        //동료 인원수 증가
        PartnerBoard.partnerCount++;
        Debug.Log($"동료정보Json 저장 후: {PartnerBoard.partnerCount}");
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
                Debug.Log($"{type}의 빈 파츠 번호: {i}");
            }
        }
    }

    
    void UpdatePartnerSelectBoard(CharacterType type)
    {
        if (PartnerBoard.partnerCount == 0)
        {
            selectboard.partner1.SetActive(true);
            selectboard.partnerName1.text = partner.character[(int)type]._name;
            //이 부분은 나중에 수정 필요
            selectboard.PartnerExplanation1.text += partner.character[(int)type].skillname + partner.character[(int)type].skilldesc;
        }else if(PartnerBoard.partnerCount == 1)
        {
            selectboard.partner1.SetActive(true);
            selectboard.partner2.SetActive(true);
            selectboard.partnerName2.text = partner.character[(int)type]._name;
            //이 부분은 나중에 수정 필요
            selectboard.PartnerExplanation2.text += partner.character[(int)type].skillname + partner.character[(int)type].skilldesc;
        }
        else
        {
            Debug.Log("동료선택모음창 오류");
        }
    }



    void ClearPartnerSelectBoard()
    {

    }

}
