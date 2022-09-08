using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class DataManager : MonoBehaviour
{
    //Json 저장하기 위한 변수
    public JObject jsonPlayer = new JObject();
    public JObject parts = new JObject();
    public JObject partnerParts = new JObject();
    public JToken jtoken;

    public JObject jPartner1 = new JObject();
    public JObject jPartner2 = new JObject();

    string SAVE_DATA_DIRECTORY;

    public Sprite[] weapons = new Sprite[3];
    public Sprite[] shields = new Sprite[3];
    public uint weaponCount = 1;
    public uint shieldCount = 0;
    public uint EquipCount; 


    //캐릭터 커스터마이즈(착장) 정보
    GameObject character0;
    GameObject character1;
    Customized[] customized = new Customized[2];

    //플레이어 스탯
    CharacterStat characterStat;
    public CharacterStat CharacterStat => characterStat;

    //동료 스탯
    PartnerSelectView partnerSelectView;
    public PartnerSelectView PartnerSelectView => partnerSelectView;

    PartnerSelectBoard partnerSelectBoard;
    public PartnerSelectBoard PartnerSelectBoard => partnerSelectBoard;

    //싱글톤 ---------------------------------------
    static DataManager instance = null;

    public static DataManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    //---------------------------------------------------

    private void Start()
    {
        character0 = GameObject.Find("Character0");

        if(character0 != null)
        customized[0] = character0.GetComponent<Customized>();


        SAVE_DATA_DIRECTORY = Application.dataPath + "/Resources/Json/";
        Test();

    }

    void Test()
    {
        //weapons[1] = Resources.Load<Sprite>($"Character/Weapons/Sword_2");
        //Debug.Log(weapons[1].ToString());
    }


    public Sprite[] sprites = new Sprite[10];


    //플레이어 파츠를 PlayerParts.json 에 저장
    public void SavePlayerParts()
    {

        // 0: 얼굴, 1: 헤어, 2: 수염, 3: 아머, 4: 바지, 5:오른쪽 무기, 6 : 왼손 무기
        characterStat = FindObjectOfType<CharacterStat>();

        //파츠 0~2번, 색상
        for (int i = 0; i < 3; i++)
        {
            parts.Add($"{i}", GameManager.Inst.partsName[i]);
            parts.Add($"color{i}", GameManager.Inst.partsColor[i, 0].ToString());
            //Debug.Log($"{i}, {GameManager.Inst.partsColor[i, 0]}");
        }

        parts.Add($"{3}", GameManager.Inst.partsName[3]);
        parts.Add($"{4}", GameManager.Inst.partsName[6]);
        parts.Add($"{5}", GameManager.Inst.partsName[8]);
        parts.Add($"{6}", GameManager.Inst.partsName[9]);

        //플레이어 무기 : 0번
        //weapons[0] = characterStat.weapon;
        //array.Add(weapons[0]);
        parts.Add("weapon0", characterStat.weapon);

        //저장
        string jsonPlayerParts = JsonConvert.SerializeObject(parts, Formatting.Indented);
        File.WriteAllText(SAVE_DATA_DIRECTORY + "/PlayerParts.json", jsonPlayerParts);
        Debug.Log("플레이어 파츠 저장");        
    }


    //선택한 직업에 따른 플레이어 데이터를 Player.json 에 초기화
    public void SetPlayerToJson()
    {
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);

        jtoken = characterStat.jTokenplayer;
        
        //DataManager의 플레이어 파츠 내용을 합쳐서 저장하려고 했는데 실패... 나중에 수정
        string player = JsonConvert.SerializeObject(jtoken, Formatting.Indented);
        File.WriteAllText(SAVE_DATA_DIRECTORY + "/Player.json", player);

        Debug.Log("플레이어 데이터 Json 초기화");
    }

    //이미지 입히기는 LoadParts.cs에 상세 구현
    public void LoadPlayerparts()
    {


    }

    //Partner_1 ~3의 파츠이미지를 저장
    public void SavePartnerParts()
    {
        character1 = GameObject.Find("Character1");
        customized[1] = character1.GetComponent<Customized>();
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);

        //파츠 0~2번, 색상
        for (int i = 0; i < 3; i++)
        {
            partnerParts.Add($"{i}", GameManager.Inst.partsName[i]);
            partnerParts.Add($"color{i}", GameManager.Inst.partsColor[i, 0].ToString());
            //Debug.Log($"{i}, {GameManager.Inst.partsColor[i, 0]}");
        }

        partnerParts.Add($"{3}", GameManager.Inst.partsName[3]);
        partnerParts.Add($"{4}", GameManager.Inst.partsName[6]);
        partnerParts.Add($"{5}", GameManager.Inst.partsName[8]);
        partnerParts.Add($"{6}", GameManager.Inst.partsName[9]);

        string partnersParts = JsonConvert.SerializeObject(partnerParts, Formatting.Indented);
        File.WriteAllText(SAVE_DATA_DIRECTORY + $"/PartnerParts_{GameManager.Inst.partnerCount}.json", partnersParts);
        Debug.Log($"PartnerParts_{GameManager.Inst.partnerCount} 저장");
    }

    public void SetPartnerToJson()
    {
        partnerSelectView = FindObjectOfType<PartnerSelectView>();

        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);

        jtoken = PartnerSelectView.jTokenPartner;

        string partner = JsonConvert.SerializeObject(jtoken, Formatting.Indented);
        File.WriteAllText(SAVE_DATA_DIRECTORY + $"/Partner_{GameManager.Inst.partnerCount}.json", partner);

        Debug.Log($"파트너 데이터_{GameManager.Inst.partnerCount}번 Json 초기화");
    }


    //SelectPartner씬에서 선택된 동료의 정보를 가져오기 위한 함수
    public void LoadPartnerData()
    {

        if (GameManager.Inst.partnerCount == 1 || GameManager.Inst.partnerCount == 2)
        {
            string partner1 = File.ReadAllText(SAVE_DATA_DIRECTORY + $"/Partner_{GameManager.Inst.partnerCount- 1}.json");
            jPartner1 = JObject.Parse(partner1);

        }else if(GameManager.Inst.partnerCount == 3)
        {
            string partner1 = File.ReadAllText(SAVE_DATA_DIRECTORY + $"/Partner_{GameManager.Inst.partnerCount - 2}.json");
            jPartner1 = JObject.Parse(partner1);

            string partner2 = File.ReadAllText(SAVE_DATA_DIRECTORY + $"/Partner_{GameManager.Inst.partnerCount - 1}.json");
            jPartner2 = JObject.Parse(partner2);

        }
    }


    //5:오른쪽 무기_웨폰, 6 : 왼손 무기_방어구
    string weaponName;
    string shieldName;

    //랜덤 장비이미지 초기에 나옴
    public void PopEquipmentImage()
    {

    }

    public void GainWeapon()
    {
        //weapon : 리소스/웨폰 폴더 sword 6개 이미지 중 
        int weaponIndex = Random.Range(0, 7);
        weaponName = $"Sword_{weaponIndex}";

        weapons[1]= Resources.Load<Sprite>($"Character/Weapons/Sword_2");

    }

    public void GainShield()
    {
        //shield : 리소스/실드 폴더 9개 이미지
        int shieldIndex = Random.Range(0, 10);
        shieldName = $"Shield_{shieldIndex}";

    }

}
