using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class DataManager : MonoBehaviour
{
    //Json �����ϱ� ���� ����
    public JObject jsonPlayer = new JObject();
    public JObject parts = new JObject();
    public JObject partnerParts = new JObject();
    public JToken jtoken;

    public JObject jPartner1 = new JObject();
    public JObject jPartner2 = new JObject();

    string SAVE_DATA_DIRECTORY;

    public string[] weapons = new string[3];
    public string[] sheilds = new string[3];


    //ĳ���� Ŀ���͸�����(����) ����
    GameObject character0;
    GameObject character1;
    Customized[] customized = new Customized[2];

    //�÷��̾� ����
    CharacterStat characterStat;
    public CharacterStat CharacterStat => characterStat;

    //���� ����
    PartnerSelectView partnerSelectView;
    public PartnerSelectView PartnerSelectView => partnerSelectView;

    PartnerSelectBoard partnerSelectBoard;
    public PartnerSelectBoard PartnerSelectBoard => partnerSelectBoard;

    //�̱��� ---------------------------------------
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

    //�÷��̾� ������ PlayerParts.json �� ����
    public void SavePlayerParts()
    {

        characterStat = FindObjectOfType<CharacterStat>();

        //���� �̹���
        for (int i = 0; i < customized[0].parts.Length; i++)
        {
           parts.Add($"{i}", GameManager.Inst.partsName[i]);
        }

        //����
        for (int i = 0; i < 3; i++)
        {
            parts.Add($"color{i}", GameManager.Inst.partsColor[i, 0].ToString());
            Debug.Log($"{i}, {GameManager.Inst.partsColor[i, 0]}");
        }

        weapons[0] = characterStat.weapon;
        array.Add(weapons[0]);

        parts.Add(array);

        //����
        string jsonPlayerParts = JsonConvert.SerializeObject(parts, Formatting.Indented);
        File.WriteAllText(SAVE_DATA_DIRECTORY + "/PlayerParts.json", jsonPlayerParts);
        Debug.Log("�÷��̾� ���� ����");        
    }


    //������ ������ ���� �÷��̾� �����͸� Player.json �� �ʱ�ȭ
    public void SetPlayerToJson()
    {
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);

        jtoken = characterStat.jTokenplayer;
        
        //DataManager�� �÷��̾� ���� ������ ���ļ� �����Ϸ��� �ߴµ� ����... ���߿� ����
        string player = JsonConvert.SerializeObject(jtoken, Formatting.Indented);
        File.WriteAllText(SAVE_DATA_DIRECTORY + "/Player.json", player);

        Debug.Log("�÷��̾� ������ Json �ʱ�ȭ");
    }

    //�̹��� ������� LoadParts.cs�� �� ����
    public void LoadPlayerparts()
    {


    }

    //Partner_1 ~3�� �����̹����� ����
    public void SavePartnerParts()
    {
        character1 = GameObject.Find("Character1");
        customized[1] = character1.GetComponent<Customized>();
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);

        for (int i = 0; i < customized[1].parts.Length; i++)
        {
            //�̹���
            partnerParts.Add($"{i}", GameManager.Inst.partsName[i]);
        }

        //����
        for (int i = 0; i < 3; i++)
        {
            partnerParts.Add($"color{i}", GameManager.Inst.partsColor[i, 0].ToString());
            Debug.Log($"{i}, {GameManager.Inst.partsColor[i, 0]}");
        }

        string partnersParts = JsonConvert.SerializeObject(partnerParts, Formatting.Indented);
        File.WriteAllText(SAVE_DATA_DIRECTORY + $"/PartnerParts_{GameManager.Inst.partnerCount}.json", partnersParts);
        Debug.Log($"PartnerParts_{GameManager.Inst.partnerCount} ����");
    }

    public void SetPartnerToJson()
    {
        partnerSelectView = FindObjectOfType<PartnerSelectView>();

        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);

        jtoken = PartnerSelectView.jTokenPartner;

        string partner = JsonConvert.SerializeObject(jtoken, Formatting.Indented);
        File.WriteAllText(SAVE_DATA_DIRECTORY + $"/Partner_{GameManager.Inst.partnerCount}.json", partner);

        Debug.Log($"��Ʈ�� ������_{GameManager.Inst.partnerCount}�� Json �ʱ�ȭ");
    }


    //SelectPartner������ ���õ� ������ ������ �������� ���� �Լ�
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

    //5:������ ����, 6 : �޼� ����
    void ChangeWeapon()
    {

    }

    void ChangeShield()
    {


    }

}
