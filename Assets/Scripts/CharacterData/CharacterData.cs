using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterData : MonoBehaviour
{
    public Vector3 oriPos;
    public float moveSpeed = 5f;
    public Animator anim;
    public Battle battle;
    public int state = 0; //0 : 대기 , 1 : 공격, 2 : 스킬, 3 : 아이템, 4 : 사망
    public int hp, mp, attack, magic, defence, speed;
    public bool BehaviorEnd;
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
                    break;
                case 1:
                    StartCoroutine(Move());
                    anim.Play("1_Run");
                    break;
            }
        }
    }
    IEnumerator Move()
    {
        while(transform.position != Vector3.zero)
        {
            transform.position  = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * moveSpeed);
            yield return null;
        }
        yield return StartCoroutine(Attack());
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        oriPos = transform.position;
    }
    public void StateChange(int i)
    {
        state = i;
    }
    public void Idle()
    {
        battle.Operator.gameObject.SetActive(false);
    }
    public IEnumerator Attack()
    {
        anim.Play("2_Attack_Normal");
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
        battle.index++;
    }
    public void AttackEnd()
    {
        BehaviorEnd = true;
    }
    public void OnMouseEnter()
    {
        battle.Targetting(Convert.ToInt32(gameObject.name[gameObject.name.Length - 1]));
    }
    public void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            battle.OperateCharacter(1);
            battle.TargetPoint.SetActive(false);
        }
    }
}