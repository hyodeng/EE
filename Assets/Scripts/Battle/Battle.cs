using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Battle : MonoBehaviour
{
    public GameObject Goblin, Piend, Golem, Darkload, Mosnters;
    public GameObject pepperBox;
    public TextMeshProUGUI hpp, mpp, resp;

    public int hpPepper = 10, mpPepper = 5, resurrectPepper = 1;
    public Player[] player;
    public Monster[] monster;

    public List<CharacterData> charactersList;
    public List<SpriteRenderer> parts;

    public Camera cam;

    public CharacterData target;

    public GameObject UserParty, Monsters;
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
            if (charactersList.Count > 1)
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
                if (!BothAlive)
                {
                    if (charactersList[0].transform.parent.name.IndexOf("User") > -1)
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
            else if (charactersList.Count == 1)
            {
                if (charactersList[0].transform.parent.name.IndexOf("User") > -1)
                {
                    BattleEnd(true);
                }
                else
                {
                    BattleEnd(false);
                }
            }
            else if (charactersList.Count == 0)
            {
                BattleEnd(false);
            }
        }
    }
    public GameObject TargetPoint;

    private void Awake()
    {
        pepperBox.SetActive(false);
        for(int i = 0; i<4; i++)
        {
            GameObject goblin = Instantiate(Goblin, Monsters.transform);
            goblin.name = $"Mosnter{i}";
            goblin.transform.localPosition = new Vector3(14.62f, i*2, 0);
            goblin.GetComponent<CharacterData>().battle = this;
        }

        for (int i = 0; i < 4; i++)
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
        hpp.text = hpPepper.ToString();
        mpp.text = mpPepper.ToString();
        resp.text = resurrectPepper.ToString();
        if (Focusing && !targetCam)
        {
            cam.orthographicSize = 3.5f;
            cam.transform.position = charactersList[index].transform.position - new Vector3(0, 0, 100);
        }
        else if (!Focusing && !targetCam)
        {
            cam.orthographicSize = 5f;
            cam.transform.position = Vector3.zero - new Vector3(0, 0, 100);
        }
        else if (targetCam)
        {
            cam.orthographicSize = 3.5f;
            cam.transform.position = target.transform.position - new Vector3(0, 0, 100);
        }
    }
    private void Initialize()
    {
        TargetPoint.SetActive(false);
        player = Transform.FindObjectsOfType<Player>();
        System.Array.Sort<Player>(player, (x, y) => x.transform.GetSiblingIndex().CompareTo(y.transform.GetSiblingIndex()));
        monster = Transform.FindObjectsOfType<Monster>();
        System.Array.Sort<Monster>(monster, (x, y) => x.transform.GetSiblingIndex().CompareTo(y.transform.GetSiblingIndex()));

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
        int k = 0;
        foreach (CharacterData character in charactersList)
        {
            character.Relocation();
            if (character.isPlayer)
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
        TargetPoint.transform.position = new Vector3(pos.x, pos.y, 0);
    }
    public void BattleEnd(bool vic)
    {
        if (vic)
        {
            Debug.Log("전투 승리");
        }
        else
        {
            Debug.Log("전투 패배");
        }
    }
    public void OnPepper()
    {
        pepperBox.SetActive(!pepperBox.activeSelf);
    }
    public void UsePepper(int i )
    {
        switch(i)
        {
            case 0:
                if(hpPepper>0)
                {
                    hpPepper--;
                    foreach(CharacterData data in charactersList)
                    {
                        if(data.gameObject.name.IndexOf("Character")>-1)
                        {
                            data.HP++;
                        }
                    }
                }
                break;
            case 1:
                if (mpPepper > 0)
                {
                    mpPepper--;
                    foreach (CharacterData data in charactersList)
                    {
                        if (data.gameObject.name.IndexOf("Character") > -1)
                        {
                            data.MP++;
                        }
                    }
                }
                break;
            case 2:
                if (resurrectPepper > 0)
                {
                    resurrectPepper--;
                    foreach (CharacterData data in charactersList)
                    {
                        if (data.gameObject.name.IndexOf("Character") > -1 && !data.gameObject.activeSelf)
                        {
                            data.gameObject.SetActive(true);
                            data.HP = 1;
                            charactersList.Add(data);
                        }
                    }
                    SetTurn();
                }
                break;
        }
    }
}