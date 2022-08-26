using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CharacterData : MonoBehaviour
{
    public string weaponType = "Normal";
    public Vector3 oriPos;
    public float moveSpeed = 5f;
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
                    anim.Play("0_Idle");
                    Idle();
                    break;
                case 1:
                    StartCoroutine(TargettingAttack());
                    break;
                case 2:
                    if (isTargetingSkill)
                    {
                        StartCoroutine(TargettingAttack());
                    }
                    else
                    {
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
        while((transform.position - Vector3.zero).sqrMagnitude > 0.01f)
        {
            transform.position  = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * moveSpeed);
            yield return null;
        }
        transform.position = Vector3.zero;
    }
    public void StateChange(int i)
    {
        state = i;
    }
    public void Idle()
    {
        battle.Operator.gameObject.SetActive(false);
    }
    IEnumerator TargettingAttack()
    {
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
    IEnumerator AreaAttack()
    {
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
    public IEnumerator Skill()
    {
        anim.Play("2_Attack_Normal");
        yield return new WaitForSeconds(1f);
        while (!BehaviorEnd)
        {
            yield return null;
        }
        /*
        foreach (var target in enemy)
        {
            TakeDamage(target);
        }
        */
        BehaviorEnd = false;
    }
    public void OnMouseEnter()
    {
       battle.Targetting(System.Convert.ToInt32(gameObject.name[^1]));
    }
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            battle.OperateCharacter(1);
            battle.target = this;
            battle.TargetPoint.SetActive(false);
        }
    }
    public void TakeDamage()
    {
        battle.target.hp -= attack - battle.target.defence;
    }
}