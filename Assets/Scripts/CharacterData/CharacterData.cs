using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterData : MonoBehaviour
{
    public GameObject DamagePrefab;

    public string weaponType = "Normal";
    public Vector3 oriPos;
    public float moveSpeed = 10f;
    public Animator anim;
    public Battle battle;
    public int state = 0;
    public int mhp, hp, mmp, mp, attack, magic, defence, speed;
    public int HP
    {
        get => hp;
        set
        {
            if(value < 0)
            {
                value = 0;
            }
            hp = value;
            if(transform.parent != null)
            {
                transform.Find("hpbar").localScale = new Vector3((float)hp / (float)mhp * Mathf.Sign(transform.parent.lossyScale.x), 1, 1);
            }
        }
    }

    public bool BehaviorEnd;
    public bool Trigger;

    public bool isPlayer;
    public CharacterType characterClass;

    public int State
    {
        get => state;
        set
        {
            state = value;
            switch (value)
            {
                case 0:
                    battle.Focusing = false;
                    anim.Play("0_Idle");
                    Idle();
                    break;
                case 1:
                    battle.Focusing = true;
                    StartCoroutine(TargettingAttack());
                    break;
                case 2:
                    battle.Focusing = true;
                    Skill();
                    break;
            }
        }
    }
    private void Awake()
    {
        hp = 10;
        mhp = HP;
        anim = GetComponent<Animator>();
    }
    IEnumerator Move()
    {
        anim.Play("1_Run");
        oriPos = transform.position;
        Vector3 targetPos = Vector3.zero;
        if (battle.target != null)
        {
            targetPos = battle.target.transform.position;
            if (battle.target.transform.parent.name.IndexOf("User") > -1)
            {
                targetPos += new Vector3(2, 0, 0);
            }
            else
            {
                targetPos += new Vector3(-2, 0, 0);
            }
        }
        while((transform.position - targetPos).sqrMagnitude > 0.01f)
        {
            transform.position  = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
            yield return null;
        }
        transform.position = targetPos;
    }
    public void Idle()
    {
        if (name.IndexOf("Monster") > -1)
        {
            transform.Find("hpbar").localScale = new Vector3((float)hp / (float)mhp * 1, 1, 1);
            transform.Find("hpbar").localPosition = new Vector3(-0.3f, -0.5f, 0);
        }
        else
        {
            transform.Find("hpbar").localScale = new Vector3((float)hp / (float)mhp * -1, 1, 1);
            transform.Find("hpbar").localPosition = new Vector3(0.3f, -0.5f, 0);
        }
        battle.Operator.gameObject.SetActive(false);
    }
    public IEnumerator TargettingAttack()
    {
        battle.targetCam = false;
        if (!isPlayer)
        {
            List<CharacterData> list = new();
            for (int i = 0; i < battle.characters.Length; i++)
            {
                if (transform.parent.name.IndexOf("User") > -1 && battle.characters[i].name.IndexOf("Monster") > -1)
                {
                    list.Add(battle.characters[i]);
                }
                else if (transform.parent.name.IndexOf("Monsters") > -1 && battle.characters[i].name.IndexOf("Character") > -1)
                {
                    list.Add(battle.characters[i]);
                }
            }
            battle.target = list[Random.Range(0, list.Count)];
        }
        yield return StartCoroutine(Move());
        anim.Play($"2_Attack_{weaponType}");
        while (!BehaviorEnd)
        {
            if(Trigger)
            {
                TakeDamage(CalcDmg(1, false));
                Trigger = false;
            }
            yield return null;
        }
        BehaviorEnd = false;
        transform.localScale = new Vector3(-2, 2, 2);
        if(name.IndexOf("Monster")>-1)
        {
            transform.Find("hpbar").localScale = new Vector3((float)hp / (float)mhp * -1, 1, 1);
            transform.Find("hpbar").localPosition = new Vector3(0.3f, -0.5f, 0);
        }
        else
        {
            transform.Find("hpbar").localScale = new Vector3((float)hp / (float)mhp * 1, 1, 1);
            transform.Find("hpbar").localPosition = new Vector3(-0.3f, -0.5f, 0);
        }
        anim.Play("1_Run");
        battle.targetCam = false;
        while (transform.position != oriPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, oriPos, Time.deltaTime * moveSpeed);
            yield return null;
        }
        transform.localScale = new Vector3(2, 2, 2);
        if (name.IndexOf("Monster") > -1)
        {
            transform.Find("hpbar").localScale = new Vector3((float)hp / (float)mhp * 1, 1, 1);
            transform.Find("hpbar").localPosition = new Vector3(-0.3f, -0.5f, 0);
        }
        else
        {
            transform.Find("hpbar").localScale = new Vector3((float)hp / (float)mhp * -1, 1, 1);
            transform.Find("hpbar").localPosition = new Vector3(0.3f, -0.5f, 0);
        }
        State = 0;
        battle.Operator.gameObject.SetActive(true);
        battle.Index++;
    }
    private int CalcDmg(float val, bool heal)
    {
        if(!heal)
        {
            if (weaponType == "Normal" || weaponType == "Bow")
            {
                return Mathf.Max((int)(val * attack) / Mathf.Max(1, battle.target.defence), 1);
            }
            else
            {
                return Mathf.Max((int)(val * magic) / Mathf.Max(1, battle.target.defence), 1);
            }
        }
        else
        {
            return (int)(val * magic);
        }
    }
    public void Skill()
    {
        if(characterClass == CharacterType.Warrior)
        {
            StartCoroutine(WarriorSkill());
        }
        else if (characterClass == CharacterType.Mage)
        {
            StartCoroutine(MageSkill());
        }
        else if (characterClass == CharacterType.Cleric)
        {
            StartCoroutine(MageSkill());
        }
        else if (characterClass == CharacterType.Thief)
        {
            StartCoroutine(ThiefSkill());
        }
        else if (characterClass == CharacterType.Popstar)
        {
            StartCoroutine(PopstarSkill());
        }
        else if (characterClass == CharacterType.Chef)
        {
            StartCoroutine(ChefSkill());
        }
    }
    public IEnumerator WarriorSkill()
    {
        battle.targetCam = false;
        battle.Focusing = false;
        anim.Play($"5_Skill_Normal");
        while (!BehaviorEnd)
        {
            if (Trigger)
            {
                foreach (CharacterData a in battle.charactersList)
                {
                    if(a.transform.parent.name.IndexOf("Monsters")>-1)
                    {
                        battle.target = a;
                        TakeDamage(CalcDmg(0.6f, false));
                    }
                }
                Trigger = false;
            }
            yield return null;
        }
        BehaviorEnd = false;
        State = 0;
        battle.Operator.gameObject.SetActive(true);
        battle.Index++;
    }
    public IEnumerator MageSkill()
    {
        battle.targetCam = false;
        battle.Focusing = false;
        anim.Play($"5_Skill_Magic");
        yield return new WaitForSeconds(1f);
        while (!BehaviorEnd)
        {
            if (Trigger)
            {
                foreach (CharacterData a in battle.charactersList)
                {
                    if (a.transform.parent.name.IndexOf("User") > -1)
                    {
                        battle.target = a;
                        TakeDamage(CalcDmg(-0.5f, true));
                    }
                }
                Trigger = false;
            }
            yield return null;
        }
        BehaviorEnd = false;
        State = 0;
        battle.Operator.gameObject.SetActive(true);
        battle.Index++;
    }
    public IEnumerator ClericSkill()
    {
        battle.targetCam = false;
        battle.Focusing = false;
        anim.Play($"5_Skill_Magic");
        yield return new WaitForSeconds(1f);
        while (!BehaviorEnd)
        {
            if (Trigger)
            {
                TakeDamage(CalcDmg(0.5f, false));
                Trigger = false;
            }
            yield return null;
        }
        BehaviorEnd = false;
        State = 0;
        battle.Operator.gameObject.SetActive(true);
        battle.Index++;
    }
    public IEnumerator ThiefSkill()
    {
        battle.targetCam = false;
        yield return StartCoroutine(Move());
        anim.Play($"2_Skill_Normal");
        yield return new WaitForSeconds(1f);
        int i = 0;
        while (!BehaviorEnd)
        {
            if (i == 3)
            {
                Trigger = false;
            }
            if (Trigger)
            {
                TakeDamage(CalcDmg(1, false));
            }
            yield return null;
            i++;
        }
        BehaviorEnd = false;
        transform.localScale = new Vector3(-2, 2, 2);
        anim.Play("1_Run");
        battle.targetCam = false;
        while (transform.position != oriPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, oriPos, Time.deltaTime * moveSpeed);
            yield return null;
        }
        transform.localScale = new Vector3(2, 2, 2);
        State = 0;
        battle.Operator.gameObject.SetActive(true);
        battle.Index++;
    }
    public IEnumerator PopstarSkill()
    {
        battle.targetCam = false;
        battle.Focusing = false;
        anim.Play($"5_Skill_Magic");
        yield return new WaitForSeconds(1f);
        while (!BehaviorEnd)
        {
            if (Trigger)
            {
                foreach (CharacterData a in battle.charactersList)
                {
                    battle.target = a;
                    TakeDamage(CalcDmg(0.4f, false));
                }
                Trigger = false;
            }
            yield return null;
        }
        BehaviorEnd = false;
        State = 0;
        battle.Operator.gameObject.SetActive(true);
        battle.Index++;
    }
    public IEnumerator ChefSkill()
    {
        battle.targetCam = false;
        if (!isPlayer)
        {
            List<CharacterData> list = new();
            for (int i = 0; i < battle.characters.Length; i++)
            {
                if (transform.parent.name.IndexOf("User") > -1)
                {
                    list.Add(battle.characters[i]);
                }
            }
            battle.target = list[Random.Range(0, list.Count)];
        }
        anim.Play($"2_Skill_Normal");
        while (!BehaviorEnd)
        {
            if (Trigger)
            {
                TakeDamage(CalcDmg(-1.5f, false));
                Trigger = false;
            }
            yield return null;
        }
        BehaviorEnd = false;
        State = 0;
        battle.Operator.gameObject.SetActive(true);
        battle.Index++;
    }
    public void AttackEnd()
    {
        BehaviorEnd = true;
    }
    public void OnMouseEnter()
    {
        battle.Targetting(this.transform.position);
    }
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && battle.TargetPoint.activeSelf)
        {
            if(battle.normalAttack)
            {
                battle.target = this;
                battle.OperateCharacter(1);
                battle.TargetPoint.SetActive(false);
            }
            else
            {
                battle.target = this;
                battle.OperateCharacter(2);
                battle.TargetPoint.SetActive(false);
            }
        }
    }
    public void TriggerOn()
    {
        Trigger = true;
    }
    public void TakeDamage(int dmg)
    {
        battle.target.HP -= dmg;
        GameObject Damage = Instantiate(DamagePrefab);
        Damage.transform.position = battle.target.transform.position + new Vector3(0, 1f, -50);
        Damage.GetComponent<TextMeshPro>().text = dmg.ToString();
    }
}