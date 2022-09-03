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


    public System.Action SavePlayerToJson;
    public System.Action SavePartnerToJson;


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
        // 10 == customized.parts.Length; �ε� �ȵ�.
        playerData.parts = new string[10];

        customized = FindObjectOfType<Customized>();

        //Ŀ���͸����� �̹��� ����
        //customized.parts.Length
        for (int i = 0; i < customized.parts.Length; i++)
        {
            playerData.parts[i] = customized.parts[i].name;
        }


        //json���� ������ ����
        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", json);

    }

    //�÷��̾� ������ �ε�
    public void LoadPalyerData()
    {
        string data = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
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



}
