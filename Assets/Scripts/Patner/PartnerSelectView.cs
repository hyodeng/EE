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
    SavePlayerData partnerData = new SavePlayerData();

    PartnerBoard partnerboard;
    PartnerSelectBoard selectboard;

    private void Awake()
    {
        //transform.find("Btn_Warrior").getcomponent<button> �� �� �ȵɱ�?
        btnWarrior = GameObject.Find("Btn_Warrior").GetComponent<Button>();
        btnMage = GameObject.Find("Btn_Mage").GetComponent<Button>();
        btnCleric = GameObject.Find("Btn_Cleric").GetComponent<Button>();
        btnThief = GameObject.Find("Btn_Thief").GetComponent<Button>();
        btnPopstar = GameObject.Find("Btn_Popstar").GetComponent<Button>();
        btnChef = GameObject.Find("Btn_Chef").GetComponent<Button>();

        partnerboard = GameObject.Find("PartnerBoard").GetComponent<PartnerBoard>();
        selectboard = GameObject.Find("PartnerSelectBoard").GetComponent<PartnerSelectBoard>();
    }

    private void Start()
    {
        customized = FindObjectOfType<Customized>();

        string character = File.ReadAllText(Application.dataPath + "/Resources/Json/" + "/Character.json");
        partner = JsonUtility.FromJson<Character>(character);

        btnWarrior.onClick.AddListener( () => DataSetUp(CharacterType.Warrior));
        btnMage.onClick.AddListener(() => DataSetUp(CharacterType.Mage));
        btnCleric.onClick.AddListener(() => DataSetUp(CharacterType.Cleric));
        btnThief.onClick.AddListener(() => DataSetUp(CharacterType.Thief));
        btnPopstar.onClick.AddListener(() => DataSetUp(CharacterType.Popstar));
        btnChef.onClick.AddListener(() => DataSetUp(CharacterType.Chef));

    }



    private void DataSetUp(CharacterType type)
    {
        partnerboard.OnPartnerSelectBoardOpen?.Invoke();

        switch (type)
        {
            case CharacterType.Warrior:
                //��Ʈ���� ������ �̹��� 
                SetPartnerParts(type);
                //��Ʈ�ʼ���Ʈ������ ����â ���� ����
                LoadCharacterJsonData(type);
                //���� ��Ʈ�������� json�������� �ʱ�ȭ 
                InitializePartenrData(type);

                break;
            case CharacterType.Mage:
                SetPartnerParts(type);
                LoadCharacterJsonData(type);
                InitializePartenrData(type);
                break;
            case CharacterType.Cleric:
                SetPartnerParts(type);
                LoadCharacterJsonData(type);
                InitializePartenrData(type);
                break;
            case CharacterType.Thief:
                SetPartnerParts(type);
                LoadCharacterJsonData(type);
                InitializePartenrData(type);
                break;
            case CharacterType.Popstar:
                SetPartnerParts(type);
                LoadCharacterJsonData(type);
                InitializePartenrData(type);
                break;
            case CharacterType.Chef:
                SetPartnerParts(type);
                LoadCharacterJsonData(type);
                InitializePartenrData(type);
                break;
            default:
                Debug.Log("��Ʈ�� ���� ���� ����");
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
        //partnerData.partnerNum = 
        //partnerData.positionX;
        //partnerData.positionY;

        //������ ����
        string data = JsonUtility.ToJson(partnerData);
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + "/Partner.json", data );
        Debug.Log("��Ʈ�� ���� �ʱ�ȭ");
    }

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
                Debug.Log($"{type}�� �� ���� ��ȣ: {i}");
            }
        }
    }

    void LoadCharacterJsonData(CharacterType type)
    {
        if (PartnerBoard.partnerCount == 0)
        {
            selectboard.partner1.SetActive(true);
            selectboard.partnerName1.text = partner.character[(int)type]._name;
            //�� �κ��� ���߿� ���� �ʿ�
            selectboard.PartnerExplanation1.text += partner.character[(int)type].skillname + partner.character[(int)type].skilldesc;
        }else if(PartnerBoard.partnerCount == 1)
        {
            selectboard.partner1.SetActive(true);
            selectboard.partner2.SetActive(true);
            selectboard.partnerName2.text = partner.character[(int)type]._name;
            //�� �κ��� ���߿� ���� �ʿ�
            selectboard.PartnerExplanation2.text += partner.character[(int)type].skillname + partner.character[(int)type].skilldesc;
        }
        else
        {
            Debug.Log("���ἱ�ø���â ����");
        }
    }



    void ClearPartnerSelectBoard()
    {

    }

}
