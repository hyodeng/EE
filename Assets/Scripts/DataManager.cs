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

        // for (int i = 0; i < playerData.parts.Length; i++)
        {
            playerData.parts = FindObjectOfType<Customized>().parts;
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

    }

    //����׿� ���
    void PrintData()
    {
        Debug.Log(Application.persistentDataPath);  //json ������ ���� ��� 
     
        Debug.Log(playerData._name);
        Debug.Log(playerData.hp);
        Debug.Log(playerData.mp);
        Debug.Log(playerData.attack);
        Debug.Log(playerData.defence);
        Debug.Log(playerData.speed);

        for (int i = 0; i < playerData.parts.Length; i++)
        {
            Debug.Log(playerData.parts[i]);
        }
    }



}
