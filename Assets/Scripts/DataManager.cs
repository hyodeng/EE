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


    //�÷��̾� ���� �̸��� Json���� ����
    public void SavePlayerParts()
    {
        customized = FindObjectOfType<Customized>();
        
        //jobject
        for (int i = 0; i < customized.parts.Length; i++)
        {
           parts.Add($"{i}", GameManager.Inst.partsName[i]);
            Debug.Log($"{i}, {GameManager.Inst.partsName[i]}");
        }

        jsonPlayer.Add("parts", parts);

        //��� ����
        string playerparts = JsonConvert.SerializeObject(jsonPlayer, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + "/PlayerParts.json", playerparts);
        Debug.Log("�÷��̾� ���� ����");
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
            parts.Add($"{i}", GameManager.Inst.partsName[i]);
        }

        jsonPlayer.Add("parts", parts);

        string playerparts = JsonConvert.SerializeObject(jsonPlayer, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + $"/PartnerParts_{GameManager.Inst.partnerCount}.json", playerparts);
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
}
