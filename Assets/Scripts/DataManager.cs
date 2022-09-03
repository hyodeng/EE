using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;


public class DataManager : MonoBehaviour
{
    //Json 클래스에 저장하기 위한 변수 생성 
    SavePlayerData playerData = new SavePlayerData();
    //public SavePlayerData PlayerData => playerData;


    //캐릭터 커스터마이즈(착장) 정보
    Customized customized;
    public Customized Customized => customized;


    //정보 저장 및 로드 관련 델리게이트 
    public System.Action SavePlayerToJson;  //플레이어 데이터 json으로 저장(초기화)
    public System.Action SavePartnerToJson;  //동료 데이터 json으로 저장(초기화)

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

    }

    void Initailize()
    {

    }

    //플레이어 이미지 파츠만 Json으로 저장
    public void SavePlayerParts()
    {
        // 10 == customized.parts.Length; 인데 안됨.
        playerData.parts = new string[10];

        customized = FindObjectOfType<Customized>();

        for (int i = 0; i < customized.parts.Length; i++)
        {
            playerData.parts[i] = customized.parts[i].name;
        }


        //json으로 파츠 이름 저장
        string player = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + "/Player.json", player);
        Debug.Log("플레이어 파츠 저장");

    }


    //플레이어 데이터 로드
    public void LoadPalyerParts()
    {
        string data = File.ReadAllText(Application.dataPath + "/Resources/Json/" + "/Player.json");
        playerData = JsonUtility.FromJson<SavePlayerData>(data);

        customized = FindObjectOfType<Customized>();

        //테스트용 : 플레이어의 부분별 이미지 가져오기
        for (int i = 0; i < 10; i++)
        {
            if (playerData.parts[i] != "")
            {
                customized.SetParts(i, playerData.parts[i]);
            }
        }
    }


}
