using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    //Json 클래스에 저장하기 위한 변수 생성 
    SavePlayerData playerData = new SavePlayerData();

    //캐릭터 스탯 정보
    CharacterStat stats;


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
        Initailize();

        stats = FindObjectOfType<CharacterStat>();
    }

    void Initailize()
    {

    }

    //플레이어 데이터 Json 저장
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

        //json으로 데이터 저장
        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", json);

        PrintData();

    }

    //플레이어 데이터 로드
    public void LoadPalyerData()
    {
        string data = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
        playerData = JsonUtility.FromJson<SavePlayerData>(data);

    }

    //디버그용 출력
    void PrintData()
    {
        Debug.Log(Application.persistentDataPath);  //json 데이터 저장 경로 
     
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
