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

    public PartnerSelect spawner;

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
        customized = GameObject.Find("Character1").GetComponent<Customized>();

        string data = File.ReadAllText(Application.dataPath + "/Resources/Json/" + "/Character.json");
        partnerData = JsonUtility.FromJson<Character>(data);

        btnWarrior.onClick.AddListener( () => DataSetUp(CharacterType.warrior));
        btnMage.onClick.AddListener(() => DataSetUp(CharacterType.mage));
        btnCleric.onClick.AddListener(() => DataSetUp(CharacterType.cleric));
        btnThief.onClick.AddListener(() => DataSetUp(CharacterType.thief));
        btnPopstar.onClick.AddListener(() => DataSetUp(CharacterType.popstar));
        btnChef.onClick.AddListener(() => DataSetUp(CharacterType.chef));

    }
    private void DataSetUp(CharacterType type)
    {
        customized.GetComponent<CharacterData>().characterClass = type;
        SetPartnerParts(type);
    }

    private void SetPartnerParts(CharacterType type)
    {
        //for (int i = 0; i < customized.parts.Length; i++)
        //{
        //    if (partnerData.character[(int)type].parts[i] != "")
        //    {
        //        customized.SetParts(i, partnerData.character[(int)type].parts[i].ToString());
        //    }
        //}
    }
    void LoadJsonPartnerData()
    {

    }
    void NextScene()
    {
        SceneManager.LoadScene("Stage_Scene");
    }
}
