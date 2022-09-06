using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class CharacterStat : MonoBehaviour
{
    //플레이어 Json 저장용 --------------------------------------
    SavePlayerData characterData = new SavePlayerData();
    JObject jobject;
    public JToken jTokenplayer;
    //---------------------------------------------------------
    
    Button button_warrior;
    Button button_mage;
    Button button_cleric;
    Button button_thief;
    Button button_popstar;
    Button button_chef;

    public Slider hpSlider;
    public Slider mpSlider;
    public Slider attackSlider;
    public Slider magicSlider;
    public Slider defenceSlider;
    public Slider speedSlider;

    GameObject characterBoard;
    GameObject statBarText;
    CanvasGroup skillBoard;

    TextMeshProUGUI playerName;
    TextMeshProUGUI playerExplanation;
    TextMeshProUGUI hptext;
    TextMeshProUGUI mptext;
    TextMeshProUGUI attackText;
    TextMeshProUGUI magicText;
    TextMeshProUGUI defText;
    TextMeshProUGUI speedText;

    TextMeshProUGUI skillName;
    TextMeshProUGUI skillExp;
    Image skillImage;

    ParticleSystem backAura;

    //무기 장착 
    Customized customized;
    public string weapon;


    private void Awake()
    {
        characterBoard = GameObject.Find("CharacterBoard");
        skillBoard = GameObject.Find("SkillBoard").GetComponent<CanvasGroup>();

        button_warrior = transform.GetChild(0).GetComponent<Button>();
        button_mage = transform.GetChild(1).GetComponent<Button>();
        button_cleric = transform.GetChild(2).GetComponent<Button>();
        button_thief = transform.GetChild(3).GetComponent<Button>();
        button_popstar = transform.GetChild(4).GetComponent<Button>();
        button_chef = transform.GetChild(5).GetComponent<Button>();

        playerName = characterBoard.transform.Find("PlayerName").GetComponent<TextMeshProUGUI>();
        playerExplanation = characterBoard.transform.Find("PlayerExplanation").GetComponent<TextMeshProUGUI>();

        statBarText = transform.Find("StatBarText").gameObject;
        hptext = statBarText.transform.Find("HPtext").GetComponent<TextMeshProUGUI>();
        mptext = statBarText.transform.Find("MPtext").GetComponent<TextMeshProUGUI>();
        attackText = statBarText.transform.Find("Attacktext").GetComponent<TextMeshProUGUI>();
        magicText = statBarText.transform.Find("Magictext").GetComponent<TextMeshProUGUI>();
        defText = statBarText.transform.Find("Deftext").GetComponent<TextMeshProUGUI>();
        speedText = statBarText.transform.Find("Speedtext").GetComponent<TextMeshProUGUI>();

        skillName = skillBoard.GetComponentsInChildren<TextMeshProUGUI>()[0];
        skillExp = skillBoard.GetComponentsInChildren<TextMeshProUGUI>()[1];
        skillImage = skillBoard.transform.Find("SkilImage").GetComponent<Image>();

        customized = FindObjectOfType<Customized>();
    }

    private void Start()
    {

        button_warrior.onClick.AddListener(() => DataSetup(CharacterType.warrior));
        button_mage.onClick.AddListener(() => DataSetup(CharacterType.mage));
        button_cleric.onClick.AddListener(() => DataSetup(CharacterType.cleric));
        button_thief.onClick.AddListener(() => DataSetup(CharacterType.thief));
        button_popstar.onClick.AddListener(() => DataSetup(CharacterType.popstar));
        button_chef.onClick.AddListener(() => DataSetup(CharacterType.chef));

        characterBoard.SetActive(false);
        statBarText.SetActive(false);
        skillBoard.alpha = 0;

        backAura = GameObject.Find("BackAura").GetComponent<ParticleSystem>();

    }


    public void DataSetup(CharacterType type)
    {
        customized.GetComponent<CharacterData>().characterClass = type;
        statBarText.SetActive(true);

        //Character.Json에서 데이터 꺼내옴
        string jsonCharacter = File.ReadAllText(Application.dataPath + "/Resources/Json/" + "/Character.json");
        jobject = JObject.Parse(jsonCharacter);

        switch (type)
        {
            case CharacterType.warrior:
                jTokenplayer = jobject["warrior"];

                SetPlayerData();
                SetSliderBar();
                SetCharacterExplanation();
                SetSkillBoard(type);
                //SetPlayerToJson();

                //직업별 오른쪽손 무기 장착
                weapon = "Sword_5.png";
                customized.SetParts(5, weapon);
                //캐릭터 배경 파티클
                if (!backAura.isPlaying) { backAura.Play(); }
                break;
            case CharacterType.mage:
                jTokenplayer = jobject["mage"];

                SetPlayerData();
                SetSliderBar();
                SetCharacterExplanation();
                SetSkillBoard(type);
                // SetPlayerToJson();

                //직업별 오른쪽손 무기 장착
                weapon = "Ward_1.png";
                customized.SetParts(5, weapon);

                if (!backAura.isPlaying) { backAura.Play(); }
                break;
            case CharacterType.cleric:
                jTokenplayer = jobject["cleric"];

                SetPlayerData();
                SetSliderBar();
                SetCharacterExplanation();
                SetSkillBoard(type);
                //SetPlayerToJson();

                //직업별 오른쪽손 무기 장착
                weapon = "Cleric_1.png";
                customized.SetParts(5, weapon);
                //customized.SetParts(6, "");
                if (!backAura.isPlaying) { backAura.Play(); }
                break;
            case CharacterType.thief:
                jTokenplayer = jobject["thief"];

                SetPlayerData();
                SetSliderBar();
                SetCharacterExplanation();
                SetSkillBoard(type);
                // SetPlayerToJson();

                //직업별 오른쪽손, 왼쪽 무기 장착
                weapon = "Sword_1.png";
                customized.SetParts(5, weapon);
                //customized.SetParts(6, "Shield_1.png");
                if (!backAura.isPlaying) { backAura.Play(); }
                break;
            case CharacterType.popstar:
                jTokenplayer = jobject["popstar"];

                SetPlayerData();
                SetSliderBar();
                SetCharacterExplanation();
                SetSkillBoard(type);
                //SetPlayerToJson();

                //직업별 오른쪽손 무기 장착
                weapon = "Pop_Star_Item.png";
                customized.SetParts(5, weapon);
                //customized.SetParts(6, "");
                if (!backAura.isPlaying) { backAura.Play(); }
                break;
            case CharacterType.chef:
                jTokenplayer = jobject["chef"];

                SetPlayerData();
                SetSliderBar();
                SetCharacterExplanation();
                SetSkillBoard(type);
                //SetPlayerToJson();

                //직업별 오른쪽손 무기 장착
                weapon = "Chef_Item.png";
                customized.SetParts(5, weapon);
                //customized.SetParts(6, "");
                if (!backAura.isPlaying) { backAura.Play(); }
                break;
        }
    }


    public void SetPlayerData()
    {
        characterData._name = jTokenplayer["_name"].Value<string>();
        characterData.hp = jTokenplayer["hp"][0].Value<int>(); //hp
        characterData.mp = jTokenplayer["mp"][0].Value<int>();
        characterData.attack = jTokenplayer["attack"][0].Value<int>();
        characterData.magic = jTokenplayer["magic"][0].Value<int>();
        characterData.defence = jTokenplayer["defence"][0].Value<int>();
        characterData.speed = jTokenplayer["speed"][0].Value<int>();
        characterData.skillname = jTokenplayer["skill"][0].Value<string>();
        characterData.skilldesc = jTokenplayer["skill"][1].Value<string>();

        //나중에 추가
        //characterData.armor = jTokenplayer["armor"].Value<string>();
        //characterData.weapon = jTokenplayer["weapon"].Value<string>();
        characterData.desc = jTokenplayer["desc"].Value<string>();

    }

    private void SetSliderBar()
    {
        //스탯바_6가지 셋팅
        hpSlider.value = (float)characterData.hp / 20;
        mpSlider.value = (float)characterData.mp / 20;
        attackSlider.value = (float)characterData.attack / 20;
        magicSlider.value = (float)characterData.magic / 20;
        defenceSlider.value = (float)characterData.defence / 20;
        speedSlider.value = (float)characterData.speed / 20;
        //스탯바 숫자
        hptext.text = characterData.hp.ToString();
        mptext.text = characterData.mp.ToString();
        attackText.text = characterData.attack.ToString();
        magicText.text = characterData.magic.ToString();
        defText.text = characterData.defence.ToString();
        speedText.text = characterData.speed.ToString();
    }

    private void SetCharacterExplanation()
    {
        //캐릭터 설명
        characterBoard.SetActive(true);
        playerName.text = characterData._name;
        playerExplanation.text = characterData.desc;
    }

    private void SetSkillBoard(CharacterType type)
    {
        skillName.text = characterData.skillname;
        skillExp.text = characterData.skilldesc;

        string name = $"Skill_{(int)type}";
        skillImage.sprite = Resources.Load<Sprite>($"Character/Skill/{name.Replace(".png", "")}");
        StartCoroutine(OnSkillDesc());
        StartCoroutine(DelaySkillDesc());

    }

    IEnumerator OnSkillDesc()
    {
        yield return new WaitForSeconds(0.3f);
        skillBoard.alpha = 1;
    }

    IEnumerator DelaySkillDesc()
    {
        yield return new WaitForSeconds(2.5f);
        skillBoard.alpha = 0;

    }


}
