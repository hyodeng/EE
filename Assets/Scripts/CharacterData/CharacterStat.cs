using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using TMPro;


[Serializable]
public class character
{
    public string _name, desc;
    public int maxhp, hp, maxmp, mp, attack, attackup, magic, magicup, defence, defenceup, speed, speedup;
    public string[] parts;
    public string skillname;
    public string skilldesc;
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
    SavePlayerData playerData = new SavePlayerData();

    ////Json 저장용 변수
    //public string jsonname;
    //public int jsonhp;
    //public int jsonmp;
    //public int jsonattack;
    //public int jsonmagic;
    //public int jsondefence;
    //public int jsonspeed;


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
    GameObject skillBoard;

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

        characterBoard = GameObject.Find("CharacterBoard");
        skillBoard = GameObject.Find("SkillBoard");
        statBarText = GameObject.Find("StatBarText");
    }

    private void Start()
    {
        characterBoard.SetActive(false);
        statBarText.SetActive(false);
        skillBoard.SetActive(false);

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

        characterBoard.SetActive(true);
        statBarText.SetActive(true);
        skillBoard.SetActive(true);

        switch (type)
        {
            case CharacterType.Warrior:
                data = character.character[(int)CharacterType.Warrior];

                SetCharacterStat(data);
                SetCharacterExplanation(data);
                SetCharacterToJson(data);

                //직업별 오른쪽손 무기 장착 , 수정 필요, 아래쪽에 SetWeapon 메소드로
                customized.SetParts(5, "Sword_5.png");
                customized.SetParts(6, "");

                SetSkillBoard(data);


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

                SetSkillBoard(data);

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

                SetSkillBoard(data);

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

                SetSkillBoard(data);

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

                SetSkillBoard(data);

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

                SetSkillBoard(data);

                if (!backAura.isPlaying) { backAura.Play(); }
                break;
        }
    }

    private void SetSkillBoard(character data)
    {
        //직업별 스킬 보여줌  , 수정사항 : 무기별 이름, 이미지,  내용 바꾸어야 함.

        skillBoard.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Test";
        skillBoard.GetComponentsInChildren<TextMeshProUGUI>()[1].text = data.skillname;
        skillBoard.GetComponentsInChildren<TextMeshProUGUI>()[2].text = data.skilldesc;
        //weaponName = "Sword";
        //skillName += data.skillname;
        //skillExp = data.skilldesc;
    }


    public void SetCharacterToJson(character data)
    {
        playerData._name = data._name;
        playerData.desc = data.desc;
        playerData.maxhp = data.maxhp;
        playerData.hp = data.hp;
        playerData.maxmp = data.maxmp;
        playerData.mp = data.mp;
        playerData.attack = data.attack;
        playerData.attackup = data.attackup;
        playerData.magic = data.magic;
        playerData.magicup = data.magicup;
        playerData.defence = data.defence;
        playerData.defenceup = data.defenceup;
        playerData.speed = data.speed;
        playerData.speedup = data.speedup;
        playerData.skillname = data.skillname;
        playerData.skilldesc = data.skilldesc;

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + "/Player.json", json, Encoding.UTF8);
        Debug.Log("플레이어 스탯 초기화");
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
