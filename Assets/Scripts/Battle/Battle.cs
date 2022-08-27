using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    public Player[] player;
    public Monster[] monster;
    public CharacterData[] characters;

    public List<CharacterData> charactersList;
    public List<SpriteRenderer> parts;

    public Camera cam;

    public CharacterData target;

    public GameObject UserParty, Monsters;
    public GameObject[] Avatars;
    public Transform Operator;
    public bool Focusing;
    public bool targetCam;

    public SkillDataManager skillData;
    public SkillDataManager SkillData => skillData;

    public int index = 0;
    public int Index
    {
        get => index;
        set
        {
            index = value;
            if (value == characters.Length)
            {
                targetCam = false;
                Focusing = false;
                index = 0;
            }
            else
            {
                OperateCharacter(Random.Range(1, 3));
            }
        }
    }

    public GameObject TargetPoint;

    private void Awake()
    {
        for(int i = 0; i<4; i++)
        {
            if (GameManager.Inst.userPartyCheck[i])
            {
                GameObject.Find("UserParty").transform.Find($"Character{i}").gameObject.SetActive(true);
            }
        }
    }
    private void Start()
    {
        Initialize();
    }
    private void Update()
    {
        if (Focusing && !targetCam)
        {
            cam.orthographicSize = 3.5f;
            cam.transform.position = Avatars[index].transform.position - new Vector3(0, 0, 10);
        }
        else if(!Focusing && !targetCam)
        {
            cam.orthographicSize = 5f;
            cam.transform.position = Vector3.zero - new Vector3(0, 0, 10);
        }
        else if(targetCam)
        {
            cam.orthographicSize = 3.5f;
            cam.transform.position = target.transform.position - new Vector3(0, 0, 10);
        }
    }
    private void Initialize()
    {
        skillData = GetComponent<SkillDataManager>();

        TargetPoint.SetActive(false);
        player = Transform.FindObjectsOfType<Player>();
        System.Array.Sort<Player>(player, (x, y) => x.transform.GetSiblingIndex().CompareTo(y.transform.GetSiblingIndex()));
        monster = Transform.FindObjectsOfType<Monster>();
        System.Array.Sort<Monster>(monster, (x, y) => x.transform.GetSiblingIndex().CompareTo(y.transform.GetSiblingIndex()));

        Operator.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnTarget());
        Operator.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OperateCharacter(2));
        Operator.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OperateCharacter(3));
        
        SetTurn();
    }
    public void SetTurn()
    {
        characters = FindObjectsOfType<CharacterData>();
        charactersList = new List<CharacterData>(characters);


        for (int i = 0; i < characters.Length; i++)
        {

            for (int j = i + 1; j < characters.Length; j++)
            {
                if (characters[i].speed < characters[j].speed)
                {
                    (characters[i], characters[j]) = (characters[j], characters[i]);
                }
            }
        }
        for (int i = 0; i < charactersList.Count; i++)
        {
            charactersList.Sort((x, y) => x.speed.CompareTo(y.speed) * (-1));
        }
        System.Array.Resize(ref Avatars, characters.Length);
        for (int i = 0; i < characters.Length; i++)
        {
            Avatars[i] = characters[i].gameObject;
        }
    }
    public void OperateCharacter(int State)
    {
        characters[index].State = State;
        Operator.gameObject.SetActive(false);
    }
    public void OnTarget()
    {
        Operator.gameObject.SetActive(false);
        TargetPoint.SetActive(true);
        Targetting(new(0, 999));
    }
    public void Targetting(Vector2 pos)
    {
        TargetPoint.transform.position = new Vector3(6, pos.y , 0);
    }
}
