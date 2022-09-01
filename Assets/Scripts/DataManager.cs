using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    //Json Ŭ������ �����ϱ� ���� ���� ���� 
    SavePlayerData playerData = new SavePlayerData();
    //public SavePlayerData PlayerData => playerData;


    //ĳ���� Ŀ���͸�����(����) ����
    Customized customized;
    public Customized Customized => customized;

    //���� �ο����� �������� ���ؼ�
    PopupController popupController;
    public PopupController PopupController => popupController;


    //���� ���� �� �ε� ���� ��������Ʈ 
    public System.Action SavePlayerToJson;  //�÷��̾� ������ json���� ����(�ʱ�ȭ) _CharacterStat.cs���� ����
    public System.Action SavePartnerToJson;  //���� ������ json���� ����(�ʱ�ȭ) _PartnerSelectView.cs���� ����
    public System.Action RefreshPartnerCount;   //���� �ο��� ���� ���� _?? �߰� ����

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
        Initailize();
    }

    void Initailize()
    {
        customized = FindObjectOfType<Customized>();
        popupController = FindObjectOfType<PopupController>();
    }

    //�÷��̾� �̹��� ������ Json���� ����
    public void SavePlayerParts()
    {
        // 10 == customized.parts.Length; �ε� �ȵ�.
        playerData.parts = new string[10];


        for (int i = 0; i < customized.parts.Length; i++)
        {
            playerData.parts[i] = customized.tempImageName[i];
        }
        
        //json���� ���� �̸� ����
        string player = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + "/Player.json", player);
        Debug.Log("�÷��̾� ���� ����");

    }


    //�÷��̾� ������ �ε�
    public void LoadPalyerParts()
    {
        string data = File.ReadAllText(Application.dataPath + "/Resources/Json/" + "/Player.json");
        playerData = JsonUtility.FromJson<SavePlayerData>(data);

        //�׽�Ʈ�� : �÷��̾��� �κк� �̹��� ��������
        for (int i = 0; i < 10; i++)
        {
            if (playerData.parts[i] != "")
            {
                customized.SetParts(i, playerData.parts[i]);
            }
        }
    }

    //��Ʈ�� ������ �ε�

}
