using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class DataManager : MonoBehaviour
{
    //Json 저장하기 위한 변수
    JObject jsonPlayer = new JObject();
    public JObject parts = new JObject();

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

    //플레이어 파츠 이름을 Json으로 저장
    public void SavePlayerParts()
    {
        customized = FindObjectOfType<Customized>();
        
        for (int i = 0; i < customized.parts.Length; i++)
        {
           parts.Add($"{i}", GameManager.Inst.partsName[i]);
            Debug.Log($"{i}, {GameManager.Inst.partsName[i]}");
        }


        //백업 저장
        string playerparts = JsonConvert.SerializeObject(jsonPlayer);
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + "/PlayerParts.json", playerparts);
        Debug.Log("플레이어 파츠 저장");

    }


}
