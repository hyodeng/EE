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

    PartnerBoard partnerboard;

    PopupController popupController;

    PartnerSelectBoard selectboard;
    TextMeshProUGUI partnerName1;
    TextMeshProUGUI skillName1;
    TextMeshProUGUI PartnerExplanation1;
    Image skillImage1;

    TextMeshProUGUI partnerName2;
    TextMeshProUGUI skillName2;
    TextMeshProUGUI PartnerExplanation2;
    Image skillImage2;

    TextMeshProUGUI partnerName3;
    TextMeshProUGUI skillName3;
    TextMeshProUGUI PartnerExplanation3;
    Image skillImage3;

    //Json 저장용 
    SavePlayerData characterData = new SavePlayerData();
    public JObject jsonAllpartner;
    public JToken jTokenPartner;

    bool isStart = false;

    public System.Action onPartnerSelectBoard;
    public System.Action offPartnerSelectBoard;


    private void Awake()
    {
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


        skillImage1 = selectboard.transform.Find("Partner1").Find("SkillImage").GetComponent<Image>();
        skillImage2 = selectboard.transform.Find("Partner2").Find("SkillImage").GetComponent<Image>();
        skillImage3 = selectboard.transform.Find("Partner3").Find("SkillImage").GetComponent<Image>();
        popupController = GameObject.Find("PopupNextSceneController").GetComponent<PopupController>();

    }

    private void Start()
    {
        //프리팹 character1의 정보를 저장하여 다음씬에 프리팹을 새로 생성
        customized = GameObject.Find("Character1").GetComponent<Customized>();

        this.gameObject.SetActive(false);

        LoadCharacterData();

        btnWarrior.onClick.AddListener(() => DataSetUp(CharacterType.warrior));
        btnMage.onClick.AddListener(() => DataSetUp(CharacterType.mage));
        btnCleric.onClick.AddListener(() => DataSetUp(CharacterType.cleric));
        btnThief.onClick.AddListener(() => DataSetUp(CharacterType.thief));
        btnPopstar.onClick.AddListener(() => DataSetUp(CharacterType.popstar));
        btnChef.onClick.AddListener(() => DataSetUp(CharacterType.chef));

        popupController.OnEnabledpartnerSelectView += OnOffViewState;

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
        jsonAllpartner = JObject.Parse(character);
    }

    private void DataSetUp(CharacterType type)
    {
        GameManager.Inst.partnerCount = 1;

        customized.GetComponent<CharacterData>().characterClass = type;
        partnerboard.onPartnerSelectBoardOpen?.Invoke();
        isStart = true;

        switch (type)
        {
            case CharacterType.warrior:
                jTokenPartner = jsonAllpartner["warrior"];

                //파트너의 파츠별 이미지 
                SetPartnerParts();
                RefreshPartnerExp(type);
                break;
            case CharacterType.mage:
                jTokenPartner = jsonAllpartner["mage"];

                SetPartnerParts();
                RefreshPartnerExp(type);

                break;
            case CharacterType.cleric:
                jTokenPartner = jsonAllpartner["cleric"];

                SetPartnerParts();
                RefreshPartnerExp(type);
                break;
            case CharacterType.thief:
                jTokenPartner = jsonAllpartner["thief"];
                SetPartnerParts();
                RefreshPartnerExp(type);

                break;
            case CharacterType.popstar:
                jTokenPartner = jsonAllpartner["popstar"];
                SetPartnerParts();
                RefreshPartnerExp(type);
                break;
            case CharacterType.chef:
                jTokenPartner = jsonAllpartner["chef"];
                SetPartnerParts();
                RefreshPartnerExp(type);
                break;
            default:
                Debug.Log("파트너 직업 선택 오류");
                break;
        }

        string partner = JsonConvert.SerializeObject(jTokenPartner, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + $"/Partner_{GameManager.Inst.partnerCount}.json", partner);
    }


    //동료의 파츠 이미지와 이름 초기화
    private void SetPartnerParts()
    {
        //테스트용
        //GameManager.Inst.partnerCount = 1;

        for (int i = 0; i < 6; i++)
        {
            customized.RandomParts(i);
        }

        characterData._name = jTokenPartner["_name"].Value<string>();

        characterData.skillname = jTokenPartner["skill"][0].Value<string>();
        characterData.skilldesc = jTokenPartner["skill"][1].Value<string>();

    }

    void RefreshPartnerExp(CharacterType type)
    {
         string name = $"Skill_{(int)type}";

        if (GameManager.Inst.partnerCount == 0 || GameManager.Inst.partnerCount == 1)
        {
            onPartnerSelectBoard?.Invoke();
            partnerName1.text = characterData._name;
            skillName1.text = characterData.skillname;
            PartnerExplanation1.text = characterData.skilldesc;
            skillImage1.sprite = Resources.Load<Sprite>($"Character/Skill/{name.Replace(".png", "")}");
        }
        else if (GameManager.Inst.partnerCount == 2)
        {
            RefreshPartnerExp();
            onPartnerSelectBoard?.Invoke();
            partnerName2.text = characterData._name;
            skillName2.text = characterData.skillname;
            PartnerExplanation2.text = characterData.skilldesc;
            skillImage2.sprite = Resources.Load<Sprite>($"Character/Skill/{name.Replace(".png", "")}");
        }
        else if (GameManager.Inst.partnerCount == 3)
        {
            RefreshPartnerExp();
            onPartnerSelectBoard?.Invoke();
            partnerName3.text = characterData._name;
            skillName3.text = characterData.skillname;
            PartnerExplanation3.text = characterData.skilldesc;
            skillImage3.sprite = Resources.Load<Sprite>($"Character/Skill/{name.Replace(".png", "")}");
        }
        else
        {
            Debug.Log("동료선택모음창 오류");
        }

    }

    void RefreshPartnerExp()
    {
        DataManager.Instance.LoadPartnerData();

        if(GameManager.Inst.partnerCount == 1)
        {
            if (isStart)
            {
                partnerName1.text = DataManager.Instance.jPartner1["_name"].Value<string>();
                skillName1.text = DataManager.Instance.jPartner1["skill"][0].Value<string>();
                PartnerExplanation1.text = DataManager.Instance.jPartner1["skill"][1].Value<string>();
            }

        }
        else if (GameManager.Inst.partnerCount == 2)
        {
            partnerName1.text = DataManager.Instance.jPartner1["_name"].Value<string>();
            skillName1.text = DataManager.Instance.jPartner1["skill"][0].Value<string>();
            PartnerExplanation1.text = DataManager.Instance.jPartner1["skill"][1].Value<string>();

        }else if (GameManager.Inst.partnerCount == 3)
        {
            partnerName1.text = DataManager.Instance.jPartner1["_name"].Value<string>();
            skillName1.text = DataManager.Instance.jPartner1["skill"][0].Value<string>();
            PartnerExplanation1.text = DataManager.Instance.jPartner1["skill"][1].Value<string>();

            partnerName2.text = DataManager.Instance.jPartner2["_name"].Value<string>();
            skillName2.text = DataManager.Instance.jPartner2["skill"][0].Value<string>();
            PartnerExplanation2.text = DataManager.Instance.jPartner2["skill"][1].Value<string>();

        }

    }
}
