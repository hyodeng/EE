using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    //Json Ŭ������ �����ϱ� ���� ���� ���� 
    SavePlayerData playerData = new SavePlayerData();

    //ĳ���� ���� ����
    CharacterStat stats;

    //ĳ���� Ŀ���͸�����(����) ����
    Customized customized;

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

        stats = FindObjectOfType<CharacterStat>();
    }

    void Initailize()
    {

    }

    //�÷��̾� ������ Json ����
    public void SavePlayerData()
    {
        if (stats != null)
        {
            playerData._name = stats.jsonname;
            playerData.hp = stats.jsonhp;
            playerData.mp = stats.jsonmp;
            playerData.attack = stats.jsonattack;
            playerData.magic = stats.jsonmagic;
            playerData.defence = stats.jsondefence;
            playerData.speed = stats.jsonspeed;
        }


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
        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", json);

        PrintData();

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

    //����׿� ���
    void PrintData()
    {
        Debug.Log(Application.persistentDataPath);  //json ������ ���� ��� 
     
        //Debug.Log(playerData._name);
        //Debug.Log(playerData.hp);
        //Debug.Log(playerData.mp);
        //Debug.Log(playerData.attack);
        //Debug.Log(playerData.defence);
        //Debug.Log(playerData.speed);

        //for (int i = 0; i < playerData.parts.Length; i++)
        //{
        //    Debug.Log(playerData.parts[i]);
        //}
    }



}
