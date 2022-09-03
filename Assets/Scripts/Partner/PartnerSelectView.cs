using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

    SavePlayerData characterData = new SavePlayerData();
    JObject Jsonpartner;
    JToken jTokenPartner;


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
        //transform.getchild(0).getcomponent<button> �� �� �ȵɱ�?
        btnWarrior = GameObject.Find("Btn_Warrior").GetComponent<Button>();
        btnMage = GameObject.Find("Btn_Mage").GetComponent<Button>();
        btnCleric = GameObject.Find("Btn_Cleric").GetComponent<Button>();
        btnThief = GameObject.Find("Btn_Thief").GetComponent<Button>();
        btnPopstar = GameObject.Find("Btn_Popstar").GetComponent<Button>();
        btnChef = GameObject.Find("Btn_Chef").GetComponent<Button>();

        partnerboard = GameObject.Find("PartnerBoard").GetComponent<PartnerBoard>();
        selectboard = GameObject.Find("PartnerSelectBoard").GetComponent<PartnerSelectBoard>();
        //����1�� ����, ����
        partnerName1 = selectboard.transform.Find("Partner1").GetChild(1).GetComponent<TextMeshProUGUI>();
        skillName1 = selectboard.transform.Find("Partner1").GetChild(2).GetComponent<TextMeshProUGUI>();
        PartnerExplanation1 = selectboard.transform.Find("Partner1").GetChild(3).GetComponent<TextMeshProUGUI>();

        //����2�� ����, ����
        partnerName2 = selectboard.transform.Find("Partner2").GetChild(1).GetComponent<TextMeshProUGUI>();
        skillName2 = selectboard.transform.Find("Partner2").GetChild(2).GetComponent<TextMeshProUGUI>();
        PartnerExplanation2 = selectboard.transform.Find("Partner2").GetChild(3).GetComponent<TextMeshProUGUI>();

        //����3�� ����, ����
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

        btnWarrior.onClick.AddListener(() => DataSetUp(CharacterType.warrior));
        btnMage.onClick.AddListener(() => DataSetUp(CharacterType.mage));
        btnCleric.onClick.AddListener(() => DataSetUp(CharacterType.cleric));
        btnThief.onClick.AddListener(() => DataSetUp(CharacterType.thief));
        btnPopstar.onClick.AddListener(() => DataSetUp(CharacterType.popstar));
        btnChef.onClick.AddListener(() => DataSetUp(CharacterType.chef));

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
        //Character.Json���� ������ ������
        string character = File.ReadAllText(Application.dataPath + "/Resources/Json/" + "/Character.json");
        Jsonpartner = JObject.Parse(character);
    }

    private void DataSetUp(CharacterType type)
    {
        partnerboard.onPartnerSelectBoardOpen?.Invoke();
        int index = 0;

        switch (type)
        {
            case CharacterType.warrior:
                jTokenPartner = Jsonpartner["warrior"];

                //��Ʈ���� ������ �̹��� 
                ClearParts();
                InitializePartenrData(index);
                RefreshDataPartnerSelectBoard(type);

                break;
            case CharacterType.mage:
                jTokenPartner = Jsonpartner["mage"];

                ClearParts();
                InitializePartenrData(index);
                RefreshDataPartnerSelectBoard(type);

                break;
            case CharacterType.cleric:
                jTokenPartner = Jsonpartner["cleric"];

                ClearParts();
                InitializePartenrData(index);
                RefreshDataPartnerSelectBoard(type);
                break;
            case CharacterType.thief:
                jTokenPartner = Jsonpartner["thief"];
                ClearParts();
                InitializePartenrData(index);
                RefreshDataPartnerSelectBoard(type);

                break;
            case CharacterType.popstar:
                jTokenPartner = Jsonpartner["popstar"];
                ClearParts();
                InitializePartenrData(index);
                RefreshDataPartnerSelectBoard(type);
                break;
            case CharacterType.chef:
                jTokenPartner = Jsonpartner["chef"];
                ClearParts();
                InitializePartenrData(index);
                RefreshDataPartnerSelectBoard(type);
                break;
            default:
                Debug.Log("��Ʈ�� ���� ���� ����");
                break;
        }
    }

    //���� �̹��� �����_������
    void ClearParts()
    {
        //for (int i = 0; i < customized.parts.Length; i++)
        //{
        //    //customized.SetParts(i, "");
        //}
    }



    //������ ���� �̹����� ĳ���͸� �ʱ�ȭ
    private void InitializePartenrData(int index)
    {
        string jsonCharacter = File.ReadAllText(Application.dataPath + "/Resources/Json/" + "/Character.json");
        Jsonpartner = JObject.Parse(jsonCharacter);

        customized.AsyncParts(index);


        characterData._name = jTokenPartner["_name"].Value<string>();
        characterData.maxhp = jTokenPartner["hp"][0].Value<int>(); //maxHP
        characterData.hp = jTokenPartner["hp"][1].Value<int>(); //hp
        characterData.maxmp = jTokenPartner["mp"][0].Value<int>();
        characterData.mp = jTokenPartner["mp"][1].Value<int>();
        characterData.maxattack = jTokenPartner["attack"][0].Value<int>();
        characterData.attack = jTokenPartner["attack"][1].Value<int>();
        characterData.maxmagic = jTokenPartner["magic"][0].Value<int>();
        characterData.maxmagic = jTokenPartner["magic"][1].Value<int>();
        characterData.maxdefence = jTokenPartner["defence"][0].Value<int>();
        characterData.defence = jTokenPartner["defence"][1].Value<int>();
        characterData.maxspeed = jTokenPartner["speed"][0].Value<int>();
        characterData.speed = jTokenPartner["speed"][1].Value<int>();
        characterData.skillname = jTokenPartner["skill"][0].Value<string>();
        characterData.skilldesc = jTokenPartner["skill"][1].Value<string>();

        //���߿� �߰�
        //characterData.armor = jTokenplayer["armor"].Value<string>();
        //characterData.weapon = jTokenplayer["weapon"].Value<string>();
        characterData.desc = jTokenPartner["desc"].Value<string>();


    }

    void SavePartnerData()
    {
        string partner = JsonConvert.SerializeObject(jTokenPartner);
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + $"/Partner_{GameManager.Inst.partnerCount}.json", partner);

        Debug.Log($"��Ʈ�� ������_{GameManager.Inst.partnerCount}�� Json ����");
    }


    void RefreshDataPartnerSelectBoard(CharacterType type)
    {
        if (GameManager.Inst.partnerCount == 0)
        {
            onPartnerSelectBoard?.Invoke();

            partnerName1.text = characterData._name;
            skillName1.text = characterData.skillname;
            PartnerExplanation1.text = characterData.skilldesc;
        }
        else if (GameManager.Inst.partnerCount == 1)
        {
            onPartnerSelectBoard?.Invoke();
            partnerName2.text = characterData._name;
            skillName2.text = characterData.skillname;
            PartnerExplanation2.text = characterData.skilldesc;
        }
        else if (GameManager.Inst.partnerCount == 2)
        {
            onPartnerSelectBoard?.Invoke();
            partnerName3.text = characterData._name;
            skillName3.text = characterData.skillname;
            PartnerExplanation3.text = characterData.skilldesc;
        }
        else
        {
            Debug.Log("���ἱ�ø���â ����");
        }

    }



}
