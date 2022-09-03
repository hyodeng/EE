using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class CharacterStat : MonoBehaviour
{
    public CharacterData characterData;


    //Json ����� ����
    //public string jsonname;
    //public int jsonhp;
    //public int jsonmp;
    //public int jsonattack;
    //public int jsonmagic;
    //public int jsondefence;
    //public int jsonspeed;


    //Max ���� -> Ȯ���Ǹ� private����
    //public int statHp = 15;
    //public int statMp = 8;
    //public int statAttack = 12;
    //public int statMagic = 7;
    //public int statDefence = 6;
    //public int statSpeed = 12;

    public Button button_warrior;
    public Button button_mage;
    public Button button_cleric;
    public Button button_thief;
    public Button button_popstar;
    public Button button_chef;
   // public Button nextButton;

    public Slider hpSlider;
    public Slider mpSlider;
    public Slider attackSlider;
    public Slider magicSlider;
    public Slider defenceSlider;
    public Slider speedSlider;

    GameObject characterBoard;
    GameObject statBarText;
    GameObject skillBoard;

    TextMeshProUGUI playerName;
    TextMeshProUGUI playerExplanation;
    TextMeshProUGUI hptext;
    TextMeshProUGUI mptext;
    TextMeshProUGUI attackText;
    TextMeshProUGUI magicText;
    TextMeshProUGUI defText;
    TextMeshProUGUI speedText;

    ParticleSystem backAura;

    //���� ���� 
    Customized customized;

    private void Awake()
    {
        //Resource ����/Json ����/Character.json ���Ͽ��� �����ʱ� ������ ĳ���� ���� �о��
        characterBoard = GameObject.Find("Canvas").transform.Find("CharacterBoard").gameObject;
        statBarText = GameObject.Find("Canvas").transform.Find("StatBarText").gameObject;
        skillBoard = GameObject.Find("SkillBoard");

        playerName = characterBoard.transform.Find("PlayerName").GetComponent<TextMeshProUGUI>();
        playerExplanation = characterBoard.transform.Find("PlayerExplanation").GetComponent<TextMeshProUGUI>();
        hptext = statBarText.transform.Find("HPtext").GetComponent<TextMeshProUGUI>();
        mptext = statBarText.transform.Find("MPtext").GetComponent<TextMeshProUGUI>();
        attackText = statBarText.transform.Find("Attacktext").GetComponent<TextMeshProUGUI>();
        magicText = statBarText.transform.Find("Magictext").GetComponent<TextMeshProUGUI>();
        defText = statBarText.transform.Find("Deftext").GetComponent<TextMeshProUGUI>();
        speedText = statBarText.transform.Find("Speedtext").GetComponent<TextMeshProUGUI>();

        customized = FindObjectOfType<Customized>();

        characterBoard.SetActive(false);
        statBarText.SetActive(false);
    }

    private void Start()
    {
        //button_warrior.onClick.AddListener(() => { characterData.characterClass = CharacterType.warrior; });
        //button_mage.onClick.AddListener(() => { characterData.characterClass = CharacterType.mage; });
        //button_cleric.onClick.AddListener(() => { characterData.characterClass = CharacterType.cleric; });
        //button_thief.onClick.AddListener(() => { characterData.characterClass = CharacterType.thief; });
        //button_popstar.onClick.AddListener(() => { characterData.characterClass = CharacterType.popstar; });
        //button_chef.onClick.AddListener(() => { characterData.characterClass = CharacterType.chef; });

        //button_warrior.onClick.AddListener(() => DataSetup(CharacterType.warrior));
        //button_mage.onClick.AddListener(() => DataSetup(CharacterType.mage));
        //button_cleric.onClick.AddListener(() => DataSetup(CharacterType.cleric));
        //button_thief.onClick.AddListener(() => DataSetup(CharacterType.thief));
        //button_popstar.onClick.AddListener(() => DataSetup(CharacterType.popstar));
        //button_chef.onClick.AddListener(() => DataSetup(CharacterType.chef));
        
        skillBoard.SetActive(false);
        backAura = GameObject.Find("BackAura").GetComponent<ParticleSystem>();
    }


    //public void DataSetup(CharacterType type)
    //{

    //    statBarText.SetActive(true);
    //    FindObjectOfType<CharacterData>().characterClass = type;

    //    switch (type)
    //    {
    //        case CharacterType.warrior:

    //            SetCharacterStat(data);
    //            SetCharacterExplanation(data);
    //            SetCharacterToJson(data);
    //            //������ �����ʼ� ���� ����
    //            customized.SetParts(5, "Sword_5.png");
    //            customized.SetParts(6, "");
    //            //ĳ���� ��� ��ƼŬ
    //            if (!backAura.isPlaying) { backAura.Play(); }
    //            break;
    //        case CharacterType.mage:
    //            data = character.character[(int)CharacterType.Mage];
    //            SetCharacterStat(data);
    //            SetCharacterExplanation(data);
    //            SetCharacterToJson(data);
    //            //������ �����ʼ� ���� ����
    //            customized.SetParts(5, "Ward_1.png");
    //            customized.SetParts(6, "");
    //            if (!backAura.isPlaying) { backAura.Play(); }
    //            break;
    //        case CharacterType.cleric:
    //            data = character.character[(int)CharacterType.Cleric];
    //            SetCharacterStat(data);
    //            SetCharacterExplanation(data);
    //            SetCharacterToJson(data);
    //            //������ �����ʼ� ���� ����
    //            customized.SetParts(5, "Cleric_1.png");
    //            customized.SetParts(6, "");
    //            if (!backAura.isPlaying) { backAura.Play(); }
    //            break;
    //        case CharacterType.thief:
    //            data = character.character[(int)CharacterType.Thief];
    //            SetCharacterStat(data);
    //            SetCharacterExplanation(data);
    //            SetCharacterToJson(data);
    //            //������ �����ʼ�, ���� ���� ����
    //            customized.SetParts(5, "Sword_1.png");
    //            customized.SetParts(6, "Shield_1.png");
    //            if (!backAura.isPlaying) { backAura.Play(); }
    //            break;
    //        case CharacterType.popstar:
    //            data = character.character[(int)CharacterType.Popstar];
    //            SetCharacterStat(data);
    //            SetCharacterExplanation(data);
    //            SetCharacterToJson(data);
    //            //������ �����ʼ� ���� ����
    //            customized.SetParts(5, "Pop_Star_Item.png");
    //            customized.SetParts(6, "");
    //            if (!backAura.isPlaying) { backAura.Play(); }
    //            break;
    //        case CharacterType.chef:
    //            data = character.character[(int)CharacterType.Chef];
    //            SetCharacterStat(data);
    //            SetCharacterExplanation(data);
    //            SetCharacterToJson(data);
    //            //������ �����ʼ� ���� ����
    //            customized.SetParts(5, "Chef_Item.png");
    //            customized.SetParts(6, "");
    //            if (!backAura.isPlaying) { backAura.Play(); }
    //            break;
    //    }
    //}


    //private void SetCharacterStat(CharacterType type)
    //{
    //    Character character;
    //    //���ȹ�_6���� ����
    //    hpSlider.value = character.HP
            
            
    //        GameManager.Inst.dataClass[type];
    //        hpSlider.value = (float)data.hp / statHp;
    //    mpSlider.value = (float)data.mp / statMp;
    //    attackSlider.value = (float)data.attack / statAttack;
    //    magicSlider.value = (float)data.magic / statMagic;
    //    defenceSlider.value = (float)data.defence / statDefence;
    //    speedSlider.value = (float)data.speed / statSpeed;
    //    //���ȹ� ����
    //    hptext.text = data.hp.ToString();
    //    mptext.text = data.mp.ToString();
    //    attackText.text = data.attack.ToString();
    //    magicText.text = data.magic.ToString();
    //    defText.text = data.defence.ToString();
    //    speedText.text = data.speed.ToString();
    //}
    //private void SetCharacterExplanation()
    //{
    //    //ĳ���� ����
    //    playerName.text = data._name;
    //    playerExplanation.text = data.desc;
    //    playerExplanation.transform.parent.gameObject.SetActive(true);
    //}



}