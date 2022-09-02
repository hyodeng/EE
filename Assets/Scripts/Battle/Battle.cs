using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    public TempPlayer[] player;
    public Monster[] monster;

    public List<CharacterData> charactersList;
    public List<SpriteRenderer> parts;

    public Camera cam;

    public CharacterData target;

    public GameObject UserParty, Monsters;
    public GameObject[] Avatars;
    public Transform Operator;
    public bool Focusing;
    public bool targetCam;
    public bool normalAttack;
    bool InitTemp = false;
    public int index = 0;
    public int Index
    {
        get => index;
        set
        {
            bool BothAlive = false;
            List<string> dataNames = new();
            index = value;
            if(charactersList.Count>1)
            {
                foreach (CharacterData data in charactersList)
                {
                    if (data)
                    {
                        dataNames.Add(data.name);
                    }
                }
                for (int i = 1; i < charactersList.Count; i++)
                {
                    if (dataNames[i - 1].Length != dataNames[i].Length)
                    {
                        BothAlive = true;
                        break;
                    }
                }
                if(!BothAlive)
                {
                    if (charactersList[0].transform.parent.name.IndexOf("User")>-1)
                    {
                        BattleEnd(true);
                    }
                    else
                    {
                        BattleEnd(false);
                    }
                }
                else
                {
                    if (value == charactersList.Count)
                    {
                        targetCam = false;
                        Focusing = false;
                        Index = 0;
                    }
                    else
                    {
                        if (InitTemp)
                        {
                            if (charactersList[value].transform.parent.name.IndexOf("User") > -1)
                            {
                                if (!charactersList[value].isPlayer)
                                { 
                                    OperateCharacter(Random.Range(1, 3));
                                }
                                else
                                {
                                    Operator.gameObject.SetActive(true);
                                    TargetPoint.SetActive(false);
                                }
                            }
                            else
                            {
                                OperateCharacter(1);
                            }
                        }
                    }
                }
            }
            else if(charactersList.Count == 1)
            {
                if (charactersList[0].transform.parent.name.IndexOf("User")>-1)
                {
                    BattleEnd(true);
                }
                else
                {
                    BattleEnd(false);
                }
            }
            else if(charactersList.Count == 0)
            {
                BattleEnd(false);
            }
        }
    }
    public GameObject TargetPoint;

    private void Awake()
    {
        for (int i = 0; i<4; i++)
        {
            GameObject.Find("UserParty").transform.Find($"Character{i}").gameObject.SetActive(GameManager.Inst.userPartyCheck[i]);
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
            cam.transform.position = Avatars[index].transform.position - new Vector3(0, 0, 100);
        }
        else if(!Focusing && !targetCam)
        {
            cam.orthographicSize = 5f;
            cam.transform.position = Vector3.zero - new Vector3(0, 0, 100);
        }
        else if(targetCam)
        {
            cam.orthographicSize = 3.5f;
            cam.transform.position = target.transform.position - new Vector3(0, 0, 100);
        }
    }
    private void Initialize()
    {
        TargetPoint.SetActive(false);
        player = Transform.FindObjectsOfType<TempPlayer>();
        System.Array.Sort<TempPlayer>(player, (x, y) => x.transform.GetSiblingIndex().CompareTo(y.transform.GetSiblingIndex()));
        monster = Transform.FindObjectsOfType<Monster>();
        System.Array.Sort<Monster>(monster, (x, y) => x.transform.GetSiblingIndex().CompareTo(y.transform.GetSiblingIndex()));

        //Operator.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnTarget());
        //Operator.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OperateCharacter(2));
        Operator.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OperateCharacter(3));
        
        SetTurn();
    }
    public void SetTurn()
    {
        CharacterData[] characters = FindObjectsOfType<CharacterData>();
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
        int k = 0;
        foreach (CharacterData character in charactersList)
        {
            if(character.isPlayer)
            { 
                Index = k;
                InitTemp = true;
            }
            k++;
        }
    }
    public void OperateCharacter(int State)
    {
        charactersList[Index].State = State;
        Operator.gameObject.SetActive(false);
    }
    public void NormalAttack(bool normal)
    {
        normalAttack = normal;
        if (charactersList[Index].isPlayer)
        {
            OnTarget();
        }
    }
    public void OnTarget()
    {
        Operator.gameObject.SetActive(false);
        TargetPoint.SetActive(true);
        Targetting(new(0, 999));
    }
    public void Targetting(Vector2 pos)
    {
        TargetPoint.transform.position = new Vector3(pos.x, pos.y , 0);
    }
    public void BattleEnd(bool vic)
    {
        if (vic)
        {
            Debug.Log("전투 승리!");
        }
        else
        {
            Debug.Log("전투 패배!");
        }
    }
}
