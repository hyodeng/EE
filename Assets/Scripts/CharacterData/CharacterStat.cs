using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class character
{
    public string _name, desc, skillname, skilldesc;
    public int maxhp, hp, maxmp, mp, attack, attackup, magic, magicup, defence, defenceup, speed, speedup;
    public string[] parts, equipment;
}
[Serializable]
public class Character
{
    public character[] character;
}


public class CharacterStat : MonoBehaviour
{
    //Json �о���� ���� ����
    public TextAsset textAsset;
    public Character character;

    //Json ����� ����
    public string jsonname;
    public int jsonhp;
    public int jsonmp;
    public int jsonattack;
    public int jsonmagic;
    public int jsondefence;
    public int jsonspeed;


    //Max ���� -> Ȯ���Ǹ� private����
    public int statHp = 15;
    public int statMp = 8;
    public int statAttack = 12;
    public int statMagic = 7;
    public int statDefence = 6;
    public int statSpeed = 12;

    public Button button_warrior;
    public Button button_mage;
    public Button button_clreic;
    public Button button_thief;
    public Button button_popstar;
    public Button button_chef;
    public Button nextButton;

    public Slider hpSlider;
    public Slider mpSlider;
    public Slider attackSlider;
    public Slider magicSlider;
    public Slider defenceSlider;
    public Slider speedSlider;

    GameObject characterBoard;
    GameObject statBarText;

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
        character = JsonUtility.FromJson<Character>(textAsset.text);

        characterBoard = GameObject.Find("Canvas").transform.Find("CharacterBoard").gameObject;
        statBarText = GameObject.Find("Canvas").transform.Find("StatBarText").gameObject;

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
        button_warrior.onClick.AddListener(() => DataSetup(CharacterType.Warrior));
        button_mage.onClick.AddListener(() => DataSetup(CharacterType.Mage));
        button_clreic.onClick.AddListener(() => DataSetup(CharacterType.Cleric));
        button_thief.onClick.AddListener(() => DataSetup(CharacterType.Thief));
        button_popstar.onClick.AddListener(() => DataSetup(CharacterType.Popstar));
        button_chef.onClick.AddListener(() => DataSetup(CharacterType.Chef));
        nextButton.onClick.AddListener(NextScene);

        backAura = GameObject.Find("BackAura").GetComponent<ParticleSystem>();
    }

    public void DataSetup(CharacterType type)
    {
        character data;

        statBarText.SetActive(true);
        FindObjectOfType<CharacterData>().characterClass = type;
        switch (type)
        {
            case CharacterType.Warrior:
                data = character.character[(int)CharacterType.Warrior];

                SetCharacterStat(data);
                SetCharacterExplanation(data);
                SetCharacterToJson(data);

                //������ �����ʼ� ���� ����
                customized.SetParts(5, "Sword_5.png");
                customized.SetParts(6, "");

                //ĳ���� ��� ��ƼŬ
                if (!backAura.isPlaying) { backAura.Play(); }
                break;

            case CharacterType.Mage:
                data = character.character[(int)CharacterType.Mage];

                SetCharacterStat(data);
                SetCharacterExplanation(data);
                SetCharacterToJson(data);

                //������ �����ʼ� ���� ����
                customized.SetParts(5, "Ward_1.png");
                customized.SetParts(6, "");

                if (!backAura.isPlaying) { backAura.Play(); }
                break;

            case CharacterType.Cleric:
                data = character.character[(int)CharacterType.Cleric];

                SetCharacterStat(data);
                SetCharacterExplanation(data);
                SetCharacterToJson(data);

                //������ �����ʼ� ���� ����
                customized.SetParts(5, "Cleric_1.png");
                customized.SetParts(6, "");

                if (!backAura.isPlaying) { backAura.Play(); }
                break;

            case CharacterType.Thief:
                data = character.character[(int)CharacterType.Thief];

                SetCharacterStat(data);
                SetCharacterExplanation(data);
                SetCharacterToJson(data);

                //������ �����ʼ�, ���� ���� ����
                customized.SetParts(5, "Sword_1.png");
                customized.SetParts(6, "Shield_1.png");

                if (!backAura.isPlaying) { backAura.Play(); }
                break;

            case CharacterType.Popstar:
                data = character.character[(int)CharacterType.Popstar];

                SetCharacterStat(data);
                SetCharacterExplanation(data);
                SetCharacterToJson(data);

                //������ �����ʼ� ���� ����
                customized.SetParts(5, "Pop_Star_Item.png");
                customized.SetParts(6, "");

                if (!backAura.isPlaying) { backAura.Play(); }
                break;

            case CharacterType.Chef:
                data = character.character[(int)CharacterType.Chef];

                SetCharacterStat(data);
                SetCharacterExplanation(data);
                SetCharacterToJson(data);

                //������ �����ʼ� ���� ����
                customized.SetParts(5, "Chef_Item.png");
                customized.SetParts(6, "");

                if (!backAura.isPlaying) { backAura.Play(); }
                break;
        }
    }

    private void SetCharacterToJson(character data)
    {
        //���õ� ĳ���� ������ Json �����ϱ� ���� �߰� ����
        jsonname = data._name;
        jsonhp = data.hp;
        jsonmp = data.mp;
        jsonattack = data.attack;
        jsonmagic = data.magic;
        jsondefence = data.defence;
        jsonspeed = data.speed;
    }

    private void SetCharacterStat(character data)
    {
        //���ȹ�_6���� ����
        hpSlider.value = (float)data.hp / statHp;
        mpSlider.value = (float)data.mp / statMp;
        attackSlider.value = (float)data.attack / statAttack;
        magicSlider.value = (float)data.magic / statMagic;
        defenceSlider.value = (float)data.defence / statDefence;
        speedSlider.value = (float)data.speed / statSpeed;

        //���ȹ� ����
        hptext.text = data.hp.ToString();
        mptext.text = data.mp.ToString();
        attackText.text = data.attack.ToString();
        magicText.text = data.magic.ToString();
        defText.text = data.defence.ToString();
        speedText.text = data.speed.ToString();

    }

    private void SetCharacterExplanation(character data)
    {
        //ĳ���� ����
        playerName.text = data._name;
        playerExplanation.text = data.desc;
        playerExplanation.transform.parent.gameObject.SetActive(true);

    }
    //5 : �����ʹ���
    void SetWeapon()
    {
        //DataManager.Instance.Customized.SetParts(5, "Sword_5.png");
        //parts[8].sprite = Resources.Load<Sprite>($"Character/Weapons/Sword_5");

    }
    void NextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelectPartner");
    }
}
