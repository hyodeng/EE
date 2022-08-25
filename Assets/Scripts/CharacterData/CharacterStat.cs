using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class character
{
    public string _name, desc;
    public int maxhp, hp, maxmp, mp, attack, attackup, magic, magicup, defence, defenceup, speed, speedup;
    public string[] parts;
    public string[] equipment;
}


[Serializable]
public class Character
{
    public character[] character;
}


public class CharacterStat : MonoBehaviour
{
    //Json 읽어오기 위한 변수
    public TextAsset textAsset;
    public Character character;

    //Json 저장용 변수
    public string jsonname;
    public int jsonhp;
    public int jsonmp;
    public int jsonattack;
    public int jsonmagic;
    public int jsondefence;
    public int jsonspeed;


    //Max 스탯 -> 확정되면 private으로
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

    public Slider hpSlider;
    public Slider mpSlider;
    public Slider attackSlider;
    public Slider magicSlider;
    public Slider defenceSlider;
    public Slider speedSlider;

    GameObject characterBoard;
    TextMeshProUGUI playerName;
    TextMeshProUGUI playerExplanation;
    GameObject statBarText;
    TextMeshProUGUI hptext;
    TextMeshProUGUI mptext;
    TextMeshProUGUI attackText;
    TextMeshProUGUI magicText;
    TextMeshProUGUI defText;
    TextMeshProUGUI speedText;

    ParticleSystem backAura;

    //무기 장착 
    Customized customized;


    private void Awake()
    {
        //Resource 폴더/Json 폴더/Character.json 파일에서 게임초기 선택할 캐릭터 정보 읽어옴
        character = JsonUtility.FromJson<Character>(textAsset.text);

        playerName = GameObject.Find("PlayerName").GetComponent<TextMeshProUGUI>();
        playerExplanation = GameObject.Find("PlayerExplanation").GetComponent<TextMeshProUGUI>();
        hptext = GameObject.Find("HPtext").GetComponent<TextMeshProUGUI>();
        mptext = GameObject.Find("MPtext").GetComponent<TextMeshProUGUI>();
        attackText = GameObject.Find("Attacktext").GetComponent<TextMeshProUGUI>();
        magicText = GameObject.Find("Magictext").GetComponent<TextMeshProUGUI>();
        defText = GameObject.Find("Deftext").GetComponent<TextMeshProUGUI>();
        speedText = GameObject.Find("Speedtext").GetComponent<TextMeshProUGUI>();

        customized = FindObjectOfType<Customized>();
    }

    private void Start()
    {

        characterBoard = GameObject.Find("CharacterBoard");
        characterBoard.SetActive(false);
        statBarText = GameObject.Find("StatBarText");
        statBarText.SetActive(false);

        button_warrior.onClick.AddListener(() => DataSetup(CharacterType.Warrior));
        button_mage.onClick.AddListener(() => DataSetup(CharacterType.Mage));
        button_clreic.onClick.AddListener(() => DataSetup(CharacterType.Cleric));
        button_thief.onClick.AddListener(() => DataSetup(CharacterType.Thief));
        button_popstar.onClick.AddListener(() => DataSetup(CharacterType.Popstar));
        button_chef.onClick.AddListener(() => DataSetup(CharacterType.Chef));

        backAura = GameObject.Find("BackAura").GetComponent<ParticleSystem>();

    }


    public void DataSetup(CharacterType type)
    {
        character data;

        statBarText.SetActive(true);
        switch (type)
        {
            case CharacterType.Warrior:
                data = character.character[(int)CharacterType.Warrior];

                SetCharacterStat(data);
                SetCharacterExplanation(data);
                SetCharacterToJson(data);

                //직업별 오른쪽손 무기 장착
                customized.SetParts(5, "Sword_5.png");
                customized.SetParts(6, "");

                //캐릭터 배경 파티클
                if (!backAura.isPlaying) { backAura.Play(); }
                break;

            case CharacterType.Mage:
                data = character.character[(int)CharacterType.Mage];

                SetCharacterStat(data);
                SetCharacterExplanation(data);
                SetCharacterToJson(data);

                //직업별 오른쪽손 무기 장착
                customized.SetParts(5, "Ward_1.png");
                customized.SetParts(6, "");

                if (!backAura.isPlaying) { backAura.Play(); }
                break;

            case CharacterType.Cleric:
                data = character.character[(int)CharacterType.Cleric];

                SetCharacterStat(data);
                SetCharacterExplanation(data);
                SetCharacterToJson(data);

                //직업별 오른쪽손 무기 장착
                customized.SetParts(5, "Cleric_1.png");
                customized.SetParts(6, "");

                if (!backAura.isPlaying) { backAura.Play(); }
                break;

            case CharacterType.Thief:
                data = character.character[(int)CharacterType.Thief];

                SetCharacterStat(data);
                SetCharacterExplanation(data);
                SetCharacterToJson(data);

                //직업별 오른쪽손, 왼쪽 무기 장착
                customized.SetParts(5, "Sword_1.png");
                customized.SetParts(6, "Shield_1.png");

                if (!backAura.isPlaying) { backAura.Play(); }
                break;

            case CharacterType.Popstar:
                data = character.character[(int)CharacterType.Popstar];

                SetCharacterStat(data);
                SetCharacterExplanation(data);
                SetCharacterToJson(data);

                //직업별 오른쪽손 무기 장착
                customized.SetParts(5, "Pop_Star_Item.png");
                customized.SetParts(6, "");

                if (!backAura.isPlaying) { backAura.Play(); }
                break;

            case CharacterType.Chef:
                data = character.character[(int)CharacterType.Chef];

                SetCharacterStat(data);
                SetCharacterExplanation(data);
                SetCharacterToJson(data);

                //직업별 오른쪽손 무기 장착
                customized.SetParts(5, "Chef_Item.png");
                customized.SetParts(6, "");

                if (!backAura.isPlaying) { backAura.Play(); }
                break;
        }
    }

    private void SetCharacterToJson(character data)
    {
        //선택된 캐릭터 정보를 Json 저장하기 위한 중간 과정
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
        //스탯바_6가지 셋팅
        hpSlider.value = (float)data.hp / statHp;
        mpSlider.value = (float)data.mp / statMp;
        attackSlider.value = (float)data.attack / statAttack;
        magicSlider.value = (float)data.magic / statMagic;
        defenceSlider.value = (float)data.defence / statDefence;
        speedSlider.value = (float)data.speed / statSpeed;

        //스탯바 숫자
        hptext.text = data.hp.ToString();
        mptext.text = data.mp.ToString();
        attackText.text = data.attack.ToString();
        magicText.text = data.magic.ToString();
        defText.text = data.defence.ToString();
        speedText.text = data.speed.ToString();

    }

    private void SetCharacterExplanation(character data)
    {
        //캐릭터 설명
        playerName.text = data._name;
        playerExplanation.text = data.desc;
        playerExplanation.transform.parent.gameObject.SetActive(true);

    }





    //5 : 오른쪽무기
    void SetWeapon()
    {
        //DataManager.Instance.Customized.SetParts(5, "Sword_5.png");
        //parts[8].sprite = Resources.Load<Sprite>($"Character/Weapons/Sword_5");

    }

}
