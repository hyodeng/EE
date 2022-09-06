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

    public string[] weapons = new string[3];
    public string[] sheilds = new string[3];


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

        customized[0] = character0.GetComponent<Customized>();


        characterStat = FindObjectOfType<CharacterStat>();

        SAVE_DATA_DIRECTORY = Application.dataPath + "/Resources/Json/";
        Initailize();

    }

    void Initailize()
    {

    }
    public Sprite[] sprites = new Sprite[10];

    JArray array = new JArray();

    //플레이어 파츠를 PlayerParts.json 에 저장
    public void SavePlayerParts()
    {

        characterStat = FindObjectOfType<CharacterStat>();

        //파츠 이미지
        for (int i = 0; i < customized[0].parts.Length; i++)
        {
           parts.Add($"{i}", GameManager.Inst.partsName[i]);
        }

        //색상
        for (int i = 0; i < 3; i++)
        {
            parts.Add($"color{i}", GameManager.Inst.partsColor[i, 0].ToString());
            Debug.Log($"{i}, {GameManager.Inst.partsColor[i, 0]}");
        }

        weapons[0] = characterStat.weapon;
        array.Add(weapons[0]);

        parts.Add(array);

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

        for (int i = 0; i < customized[1].parts.Length; i++)
        {
            //이미지
            partnerParts.Add($"{i}", GameManager.Inst.partsName[i]);
        }

        //색상
        for (int i = 0; i < 3; i++)
        {
            partnerParts.Add($"color{i}", GameManager.Inst.partsColor[i, 0].ToString());
            Debug.Log($"{i}, {GameManager.Inst.partsColor[i, 0]}");
        }

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

    //5:오른쪽 무기, 6 : 왼손 무기
    void ChangeWeapon()
    {

    }

    void ChangeShield()
    {


    }

}
