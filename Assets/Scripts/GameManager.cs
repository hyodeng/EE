using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

public class GameManager : MonoBehaviour
{
    public TextAsset CharacterJson;

    string json = @"{
    'asd': [null, false, true]
    }";

    public string[] SetMonsters = new string[4];
    public bool[] SceneUnlocked = new bool[3];
    public int Stage = 1;
    public CharacterType[] dataClass;
    public int[] dataHP, dataMP, dataAttack, dataMagic, dataDefence, dataSpeed;
    public string[] dataSkillName, dataSkillDesc;
    public float[] dataSkillDmg;
    public bool?[] dataSkillTarget;
    public bool[] dataAOE;

    public int partnerCount = 1;

    static GameManager instance = null;
    public static GameManager Inst => instance;
    public CharacterData[] userPartyData = new CharacterData[4];
    public string[] partsName = new string[10];
    public Sprite[,] temp = new Sprite[10, 4];
    public Color[,] partsColor = new Color[3, 4];
    public bool[] userPartyCheck = new bool[4];
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
        dataClass = new CharacterType[4];

        dataHP = new int[4];
        dataMP = new int[4];
        dataAttack = new int[4];
        dataMagic = new int[4];
        dataDefence = new int[4];
        dataSpeed = new int[4];

        dataSkillName = new string[4];
        dataSkillDesc = new string[4];

        dataSkillDmg = new float[4];

        dataSkillTarget = new bool?[4];

        dataAOE = new bool[4];

        JObject jobject = JObject.Parse(json);
        JToken temp = jobject["asd"][0];
        //Debug.Log(jobject["asd"][0].Type == JTokenType.Null);
        //Debug.Log(temp.Type == JTokenType.Null);
        //Debug.Log(jobject["asd"][1].Value<bool>() == false);
        //Debug.Log(jobject["asd"][2]);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SceneUnlocked[0] = true;
        Initialize();
    }
    private void Initialize()
    {
    }
    public void SetInitStat(Character InitData, CharacterData targetData)
    {
        int index = System.Convert.ToInt32(targetData.gameObject.name.Replace("Character", ""));
        dataClass[index] = targetData.characterClass;
        targetData.mhp = InitData.classData["hp"][0].Value<int>(); ;
        targetData.HP = InitData.classData["hp"][0].Value<int>();
        dataHP[index] = targetData.HP;
        targetData.mmp = InitData.classData["mp"][0].Value<int>();
        targetData.MP = InitData.classData["mp"][0].Value<int>();
        dataMP[index] = targetData.mp;
        targetData.attack = InitData.classData["attack"][0].Value<int>();
        dataAttack[index] = targetData.attack;
        targetData.magic = InitData.classData["magic"][0].Value<int>();
        dataMagic[index] = targetData.magic;
        targetData.defence = InitData.classData["defence"][0].Value<int>();
        dataDefence[index] = targetData.defence;
        targetData.speed = InitData.classData["speed"][0].Value<int>();
        dataSpeed[index] = targetData.speed;
        targetData.skillName = InitData.classData["skill"][0].Value<string>();
        dataSkillName[index] = targetData.skillName;
        targetData.skillDesc = InitData.classData["skill"][1].Value<string>();
        dataSkillDesc[index] = targetData.skillDesc;
        targetData.skillTarget = InitData.classData["skill"][2].Value<bool?>();
        dataSkillTarget[index] = targetData.skillTarget;
        targetData.AOE = InitData.classData["skill"][3].Value<bool>();
        dataAOE[index] = targetData.AOE;
        targetData.skillDmg = InitData.classData["skill"][4].Value<float>();
        dataSkillDmg[index] = targetData.skillDmg;
    }
}