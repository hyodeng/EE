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
    public int hp, mp, attack, magic, defence, speed;

    public bool BehaviorEnd;
    public bool isTargetingSkill;

    public bool isPlayer;

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
                    if (isTargetingSkill)
                    {
                        battle.Focusing = true;
                        StartCoroutine(TargettingAttack());
                    }
                    else
                    {
                        battle.Focusing = true;
                        StartCoroutine(AreaAttack());
                    }
                    break;
            }
        }
    }
    private void Awake()
    {
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
    public void StateChange(int i)
    {
        state = i;
    }
    public void Idle()
    {
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
        yield return new WaitForSeconds(1f);
        while (!BehaviorEnd)
        {
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
    public IEnumerator AreaAttack()
    {
        battle.targetCam = false;
        battle.Focusing = false;
        anim.Play($"5_Skill_{weaponType}");
        yield return new WaitForSeconds(1f);
        while (!BehaviorEnd)
        {
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
            battle.target = this;
            battle.OperateCharacter(1);
            battle.TargetPoint.SetActive(false);
        }
    }
    public void TakeDamage()
    {
        battle.targetCam = true;
        battle.target.hp -= attack - battle.target.defence;
        GameObject Damage = Instantiate(DamagePrefab);
        Damage.transform.position = battle.target.transform.position + new Vector3(0, 0.5f, -50);
        Damage.GetComponent<TextMeshPro>().text = (attack - battle.target.defence).ToString();
    }
}