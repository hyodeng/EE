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

    private string SAVE_DATA_DIRECTORY; 


    //ĳ���� Ŀ���͸�����(����) ����
    Customized customized;
    public Customized Customized => customized;

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
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Resources/Json/";
        
        Initailize();

    }

    void Initailize()
    {

    }
    public Sprite[] sprites = new Sprite[10];


    //�÷��̾� ���� �̸��� Json���� ����
    public void SavePlayerParts()
    {
        customized = FindObjectOfType<Customized>();
        

        //���� �̹���
        for (int i = 0; i < customized.parts.Length; i++)
        {
           parts.Add($"{i}", GameManager.Inst.partsName[i]);
        }

        //����
        for (int i = 0; i < 3; i++)
        {
            parts.Add($"color{i}", GameManager.Inst.partsColor[i, 0].ToString());
            Debug.Log($"{i}, {GameManager.Inst.partsColor[i, 0]}");
        }

        //����
        string jsonPlayerParts = JsonConvert.SerializeObject(parts, Formatting.Indented);
        File.WriteAllText(SAVE_DATA_DIRECTORY + "/PlayerParts.json", jsonPlayerParts);
        Debug.Log("�÷��̾� ���� ����");
        
    }

    //�̹��� ������� LoadParts.cs�� �� ����
    public void LoadPlayerparts()
    {


    }


    public void SetPlayerToJson()
    {
        characterStat = FindObjectOfType<CharacterStat>();

        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);

        jtoken = characterStat.jTokenplayer;
        
        //DataManager�� �÷��̾� ���� ������ ���ļ� �����Ϸ��� �ߴµ� ����... ���߿� ����
        string player = JsonConvert.SerializeObject(jtoken, Formatting.Indented);
        File.WriteAllText(SAVE_DATA_DIRECTORY + "/Player.json", player);

        Debug.Log("�÷��̾� ������ Json �ʱ�ȭ");
    }

    //�̻����
    public void SavePartnerParts()
    {
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        //PartnerParts_0���� ������ ������ ���� �ǳ�

        for (int i = 0; i < customized.parts.Length; i++)
        {
            //�̹���
            partnerParts.Add($"{i}", GameManager.Inst.partsName[i]);
        }



        jsonPlayer.Add("parts", partnerParts);

        string playerparts = JsonConvert.SerializeObject(jsonPlayer, Formatting.Indented);
        File.WriteAllText(SAVE_DATA_DIRECTORY + $"/PartnerParts_{GameManager.Inst.partnerCount}.json", playerparts);
        Debug.Log("�÷��̾� ���� ����");
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
        if (GameManager.Inst.partnerCount == 2)
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

        Customized.SetParts(5,"");
    }

    void ChangeShield()
    {


    }

}
