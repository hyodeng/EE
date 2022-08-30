using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    //Json Ŭ������ �����ϱ� ���� ���� ���� 
    SavePlayerData playerData = new SavePlayerData();
   

    //ĳ���� Ŀ���͸�����(����) ����
    Customized customized;
    public Customized Customized => customized;

    //���� ���� ��Ʈ��
    PartnerBoard partnerboard;
    public PartnerBoard PartnerBoard => partnerboard;

    //�����ұ�?
    PopupController popupController;
    public PopupController PopupController => popupController;


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
   
    }



    //�÷��̾� ������ Json ����
    public void SavePlayerData()
    {
        //���� ���� ������Ʈ
        //playerData._name = 

        // 10 == customized.parts.Length; �ε� �ȵ�.
        playerData.parts = new string[10];

        customized = FindObjectOfType<Customized>();

        //Ŀ���͸����� �̹��� ����
        //customized.parts.Length
        for (int i = 0; i < customized.parts.Length; i++)
        {
            playerData.parts[i] = customized.tempImageName[i];
        }
        

        //json���� ������ ����
        string player = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.dataPath + "/Resources/Json/" + "/Player.json", player);
        Debug.Log("�÷��̾� ���� ����, �÷��̾� ���� ������Ʈ?");

    }


    //�÷��̾� ������ �ε�
    public void LoadPalyerData()
    {
        string data = File.ReadAllText(Application.dataPath + "/Resources/Json/" + "/Player.json");
        playerData = JsonUtility.FromJson<SavePlayerData>(data);

        customized = FindObjectOfType<Customized>();

        //�׽�Ʈ�� : �÷��̾��� �κк� �̹��� ��������
        for (int i = 0; i < 10; i++)
        {
            if (playerData.parts[i] != "")
            {
                customized.SetParts(i, playerData.parts[i]);
            }

        }
    }

    //�׽�Ʈ ��Ʈ�� ������ ����
    public void SavePartnerData(CharacterType type)
    {

    }


}
