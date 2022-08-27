using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class PartnerSelectButton : MonoBehaviour
{
    Button btnWarrior;
    Button btnMage;
    Button btnCleric;
    Button btnThief;
    Button btnPopstar;
    Button btnChef;
    public Button nextButton;

    Customized customized;
    
    Character partnerData;

    public PartnerSpawner spawner;

    private void Awake()
    {
        btnWarrior = GameObject.Find("Btn_Warrior").GetComponent<Button>();
        btnMage = GameObject.Find("Btn_Mage").GetComponent<Button>();
        btnCleric = GameObject.Find("Btn_Cleric").GetComponent<Button>();
        btnThief = GameObject.Find("Btn_Thief").GetComponent<Button>();
        btnPopstar = GameObject.Find("Btn_Popstar").GetComponent<Button>();
        btnChef = GameObject.Find("Btn_Chef").GetComponent<Button>();
        nextButton.onClick.AddListener(NextScene);
    }
    private void Start()
    {
        customized = FindObjectOfType<Customized>();

        string data = File.ReadAllText(Application.dataPath + "/Resources/Json/" + "/Character.json");
        partnerData = JsonUtility.FromJson<Character>(data);

        btnWarrior.onClick.AddListener( () => DataSetUp(CharacterType.Warrior));
        btnMage.onClick.AddListener(() => DataSetUp(CharacterType.Mage));
        btnCleric.onClick.AddListener(() => DataSetUp(CharacterType.Cleric));
        btnThief.onClick.AddListener(() => DataSetUp(CharacterType.Thief));
        btnPopstar.onClick.AddListener(() => DataSetUp(CharacterType.Popstar));
        btnChef.onClick.AddListener(() => DataSetUp(CharacterType.Chef));

    }
    private void DataSetUp(CharacterType type)
    {

        switch (type)
        {
            case CharacterType.Warrior:
                //��Ʈ���� �̹���
                SetPartnerParts(type);

                break;
            case CharacterType.Mage:
                break;
            case CharacterType.Cleric:
                break;
            case CharacterType.Thief:
                break;
            case CharacterType.Popstar:
                break;
            case CharacterType.Chef:
                break;
            default:
                Debug.Log("��Ʈ�� ���� ���� ����");
                break;
        }
    }

    private void SetPartnerParts(CharacterType type)
    {
        for (int i = 0; i < customized.parts.Length; i++)
        {
            if (partnerData.character[(int)type].parts[i] != "")
            {
                customized.SetParts(i, partnerData.character[(int)type].parts[i].ToString());
            }
            else
            {
                Debug.Log($"{type}�� �� ���� ��ȣ: {i}");
            }
        }
    }
    void LoadJsonPartnerData()
    {

    }
    void NextScene()
    {
        if(spawner.OnSpawn)
        {
            SceneManager.LoadScene("Stage_Scene");
        }
    }
}
