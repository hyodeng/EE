using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class DataManager : MonoBehaviour
{
    //Json �����ϱ� ���� ����
    JObject parts = new JObject();
    JObject jsonPlayer = new JObject();

    //ĳ���� Ŀ���͸�����(����) ����
    Customized customized;
    public Customized Customized => customized;


    //���� ���� �� �ε� ���� ��������Ʈ 
    public System.Action SavePlayerToJson;  //�÷��̾� ������ json���� ����(�ʱ�ȭ)
    public System.Action SavePartnerToJson;  //���� ������ json���� ����(�ʱ�ȭ)

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

    }

    //�÷��̾� ���� �̸��� Json���� ����
    public void SavePlayerParts()
    {

        //string jsonCharacter = File.ReadAllText(Application.dataPath + "/Resources/Json/" + "/Character.json");
        //jsonPlayer = JObject.Parse(jsonCharacter);

        customized = FindObjectOfType<Customized>();

        for (int i = 0; i < customized.parts.Length; i++)
        {
           parts.Add($"{i}", GameManager.Inst.partsName[i]);
        }

        //jsonPlayer.Add(parts);

        //������
        string playerparts = JsonConvert.SerializeObject(parts);
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + "/PlayerParts.json", playerparts);
        Debug.Log("�÷��̾� ���� ����");

    }


}
