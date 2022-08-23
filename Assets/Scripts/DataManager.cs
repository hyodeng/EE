using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    //Json 클래스에 저장하기 위한 변수 생성 
    SavePlayerData playerData = new SavePlayerData();


    //플레이어 스탯 정보
    CharacterStat stats;

    //캐릭터 커스터마이즈(착장) 정보
    Customized customized;
    //character data;


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
        //스탯 정보 업데이트
        //playerData._name = 

        // 10 == customized.parts.Length; 인데 안됨.
        playerData.parts = new string[10];

        customized = FindObjectOfType<Customized>();

        //커스터마이즈 이미지 저장
        //customized.parts.Length
        for (int i = 0; i < customized.parts.Length; i++)
        {
            playerData.parts[i] = customized.tempImageName[i];
        }
        

        //json으로 데이터 저장
        string player = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.dataPath + "/Resources/Json/" + "/Player.json", player);
        Debug.Log("플레이어 파츠 저장, 플레이어 스탯 업데이트?");

    }


    //플레이어 데이터 로드
    public void LoadPalyerData()
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

    //테스트 파트너 데이터 저장
    public void SavePartnerData(CharacterType type)
    {

    }


}
