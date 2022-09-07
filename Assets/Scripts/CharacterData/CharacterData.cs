using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

[Serializable]
public class Character
{
    public JObject jobject;
    public JObject classData;

    public Character(string text, string cl)
    {
        jobject = JObject.Parse(text);
        classData = (JObject)jobject[cl];
    }
}
public class CharacterData : MonoBehaviour
{
    public GameObject DamagePrefab;

    public string weaponType = "Normal";
    public Vector3 oriPos;
    public float moveSpeed = 10f;
    public Animator anim;
    public Battle battle;
    public int state = 0;
    public string skillName;
    public string skillDesc;
    public bool? skillTarget;
    public bool AOE;
    public float skillDmg;
    public int mhp, hp, mmp, mp, attack, magic, defence, speed;
    public int HP
    {
        get => hp;
        set
        {
            hp = value;
            if(hp > mhp)
            {
                hp = mhp;
            }
            if(hp < 0)
            {
                hp = 0;
            }
            if (value <= 0)
            {
                value = 0;
                battle.charactersList.Remove(this);
                gameObject.SetActive(false);
            }
            if (transform.parent != null)
            {
                Relocation();
            }
        }
    }
    public int MP
    {
        get => mp;
        set
        {
            mp = value;
            if (mp > mmp)
            {
                mp = mmp;
            }
            if (mp < 0)
            {
                mp = 0;
            }
            if (transform.parent != null)
            {
                Relocation();
            }
        }
    }

    public bool BehaviorEnd;
    public bool Trigger;

    public bool isPlayer;
    public CharacterType characterclass;
    public CharacterType characterClass
    {
        get => characterclass;
        set
        {
            characterclass = value;
            switch (value)
            {
                case CharacterType.mage:
                    weaponType = "Magic";
                    break;
                case CharacterType.popstar:
                    weaponType = "Magic";
                    break;
                default:
                    weaponType = "Normal";
                    break;
            }
            if(value!=CharacterType.monster)
            {
                GameManager.Inst.SetInitStat(new Character(GameManager.Inst.CharacterJson.text, Enum.GetName(typeof(CharacterType), value)), this);
            }
        }
    }

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
                    battle.normalAttack = true;
                    StartCoroutine(TargettingAttack());
                    break;
                case 2:
                    if (MP < 1)
                    {
                        State = 1;
                        break;
                    }
                    battle.Focusing = true;
                    MP--;
                    Skill();
                    break;
            }
        }
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        if(gameObject.name.IndexOf("Character")>-1)
        {
            AsyncData(System.Convert.ToInt32(gameObject.name.Replace("Character", "")));
        }
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
        while ((transform.position - targetPos).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
            yield return null;
        }
        transform.position = targetPos;
    }
    public void Idle()
    {
        Relocation();
        battle.Operator.gameObject.SetActive(false);
    }
    public IEnumerator TargettingAttack()
    {
        battle.targetCam = false;
        if (!isPlayer)
        {
            List<CharacterData> list = new();
            for (int i = 0; i < battle.charactersList.Count; i++)
            {
                if (transform.parent.name.IndexOf("User") > -1 && battle.charactersList[i].name.IndexOf("Monster") > -1)
                {
                    list.Add(battle.charactersList[i]);
                }
                else if (transform.parent.name.IndexOf("Monsters") > -1 && battle.charactersList[i].name.IndexOf("Character") > -1)
                {
                    list.Add(battle.charactersList[i]);
                }
            }
            battle.target = list[UnityEngine.Random.Range(0, list.Count)];
        }
        yield return StartCoroutine(Move());
        anim.Play($"2_Attack_{weaponType}");
        while (!BehaviorEnd)
        {
            if (Trigger)
            {
                TakeDamage(CalcDmg(1, false));
                Trigger = false;
            }
            yield return null;
        }
        BehaviorEnd = false;
        transform.localScale = new Vector3(-2, 2, 2);
        Relocation();
        anim.Play("1_Run");
        battle.targetCam = false;
        while (transform.position != oriPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, oriPos, Time.deltaTime * moveSpeed);
            yield return null;
        }
        transform.localScale = new Vector3(2, 2, 2);
        Relocation();
        State = 0;
        battle.Operator.gameObject.SetActive(true);
        battle.Index++;
    }
    public void Relocation()
    {
        transform.Find("hpbar").localPosition = new Vector3(-0.3f * Mathf.Sign(transform.lossyScale.x), -0.5f, 0);
        transform.Find("mpbar").localPosition = new Vector3(-0.3f * Mathf.Sign(transform.lossyScale.x), -0.6f, 0);
        transform.Find("hpbar").localScale = new Vector3((float)hp / (float)mhp * Mathf.Sign(transform.lossyScale.x), 1, 1);
        transform.Find("mpbar").localScale = new Vector3((float)mp / (float)mmp * Mathf.Sign(transform.lossyScale.x), 1, 1);
    }
    private int CalcDmg(float val, bool heal)
    {
        if (!heal)
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
        if (characterClass == CharacterType.warrior)
        {
            StartCoroutine(WarriorSkill());
        }
        else if (characterClass == CharacterType.mage)
        {
            StartCoroutine(MageSkill());
        }
        else if (characterClass == CharacterType.cleric)
        {
            StartCoroutine(ClericSkill());
        }
        else if (characterClass == CharacterType.thief)
        {
            StartCoroutine(ThiefSkill());
        }
        else if (characterClass == CharacterType.popstar)
        {
            StartCoroutine(PopstarSkill());
        }
        else if (characterClass == CharacterType.chef)
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
                for (int i = battle.charactersList.Count - 1; i >= 0; i--)
                {
                    if (battle.charactersList[i].transform.parent.name.IndexOf("Monster") > -1)
                    {
                        Transform obj = Instantiate(Resources.Load<Transform>("Prefabs/Particle/WarriorSkill"), battle.charactersList[i].transform.position, Quaternion.identity);
                        Destroy(obj.gameObject, 4f);
                        battle.target = battle.charactersList[i];
                        TakeDamage(CalcDmg(skillDmg, skillDmg < 0));
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
        Transform obj = Instantiate(Resources.Load<Transform>("Prefabs/Particle/MageSkill"), Vector3.zero, Quaternion.Euler(-40f, 0, 0));
        Destroy(obj.gameObject, 5f);    
        battle.targetCam = false;
        battle.Focusing = false;
        anim.Play($"5_Skill_Magic");
        while (!BehaviorEnd)
        {
            if (Trigger)
            {
                for (int i = battle.charactersList.Count - 1; i >= 0; i--)
                {
                    if (battle.charactersList[i].transform.parent.name.IndexOf("Monster") > -1)
                    {
                        battle.target = battle.charactersList[i];
                        TakeDamage(CalcDmg(skillDmg, skillDmg < 0));
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
        while (!BehaviorEnd)
        {
            Transform obj = Instantiate(Resources.Load<Transform>("Prefabs/Particle/ClericSkill"), transform.position, Quaternion.Euler(-40f, 0, 0));
            Destroy(obj.gameObject, 5f);
            if (Trigger)
            {
                for (int i = battle.charactersList.Count - 1; i >= 0; i--)
                {
                    if (battle.charactersList[i].transform.parent.name.IndexOf("User") > -1)
                    {
                        battle.target = battle.charactersList[i];
                        TakeDamage(CalcDmg(skillDmg, skillDmg < 0));
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
    public IEnumerator ThiefSkill()
    {
        if (!isPlayer)
        {
            List<CharacterData> list = new();
            for (int j = 0; j < battle.charactersList.Count; j++)
            {
                if (transform.parent.name.IndexOf("User") > -1 && battle.charactersList[j].name.IndexOf("Monster") > -1)
                {
                    list.Add(battle.charactersList[j]);
                }
                else if (transform.parent.name.IndexOf("Monsters") > -1 && battle.charactersList[j].name.IndexOf("Character") > -1)
                {
                    list.Add(battle.charactersList[j]);
                }
            }
            battle.target = list[UnityEngine.Random.Range(0, list.Count)];
        }
        battle.targetCam = false;
        battle.Focusing = true;
        yield return StartCoroutine(Move());
        anim.Play($"5_Skill_Normal");
        int i = 0;
        while (!BehaviorEnd)
        {
            if (Trigger)
            {
                if (i == 3)
                {
                    Trigger = false;
                    break;
                }
                Transform obj = Instantiate(Resources.Load<Transform>("Prefabs/Particle/WarriorSkill"), battle.target.transform.position, Quaternion.identity);
                Destroy(obj.gameObject, 5f);
                TakeDamage(CalcDmg(skillDmg, skillDmg < 0));
                i++;
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
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
        while (!BehaviorEnd)
        {
            if (Trigger)
            {
                Transform obj = Instantiate(Resources.Load<Transform>("Prefabs/Particle/MageSkill"), Vector3.zero, Quaternion.identity);
                Destroy(obj.gameObject, 5f);
                for (int i = battle.charactersList.Count - 1; i >= 0; i--)
                {
                    battle.target = battle.charactersList[i];
                    TakeDamage(CalcDmg(skillDmg, skillDmg < 0));
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
        if (!isPlayer)
        {
            List<CharacterData> list = new();
            for (int j = 0; j < battle.charactersList.Count; j++)
            {
                if (battle.charactersList[j].name.IndexOf("Monster") > -1)
                {
                    list.Add(battle.charactersList[j]);
                }
            }
            battle.target = list[UnityEngine.Random.Range(0, list.Count)];
        }
        battle.targetCam = false;
        battle.Focusing = true;
        anim.Play($"5_Skill_Normal");
        while (!BehaviorEnd)
        {
            if (Trigger)
            {
                Transform obj = Instantiate(Resources.Load<Transform>("Prefabs/Particle/ChefSkill"), battle.target.transform.position, Quaternion.identity);
                Destroy(obj.gameObject, 5f);
                TakeDamage(CalcDmg(skillDmg, skillDmg < 0));
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
    public void OnMouseOver()
    {
        battle.Targetting(this.transform.position);
        if (Input.GetMouseButtonDown(0) && battle.TargetPoint.activeSelf)
        {
            if (battle.normalAttack)
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
        if (battle.target.HP > battle.target.mhp)
        {
            battle.target.HP = battle.target.mhp;
        }
        GameObject Damage = Instantiate(DamagePrefab);
        Damage.transform.position = battle.target.transform.position + new Vector3(0, 1f, -50);
        if (dmg < 0)
        {
            Damage.GetComponent<TextMeshPro>().color = Color.green;
        }
        Damage.GetComponent<TextMeshPro>().text = Mathf.Abs(dmg).ToString();
    }
    public void AsyncData(int index)
    {
        characterClass = GameManager.Inst.dataClass[index];
        HP = GameManager.Inst.dataHP[index];
        mhp = HP;
        if (hp > 0 && transform.Find("hpbar") != null)
        {
            Relocation();
        }
        mp = GameManager.Inst.dataHP[index];
        mmp = MP;
        if (mp > 0 && transform.Find("mpbar") != null)
        {
            Relocation();
        }
        attack = GameManager.Inst.dataHP[index];
        magic = GameManager.Inst.dataHP[index];
        defence = GameManager.Inst.dataHP[index];
        speed = GameManager.Inst.dataHP[index];
        skillName = GameManager.Inst.dataSkillName[index];
        skillDesc = GameManager.Inst.dataSkillDesc[index];
        skillTarget = GameManager.Inst.dataSkillTarget[index];
        AOE = GameManager.Inst.dataAOE[index];
        skillDmg = GameManager.Inst.dataSkillDmg[index];
    }
}