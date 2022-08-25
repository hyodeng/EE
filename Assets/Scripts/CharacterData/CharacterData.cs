using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CharacterData : MonoBehaviour
{
    public Vector3 oriPos;
    public float moveSpeed = 5f;
    public Animator anim;
    public Battle battle;
    public int state = 0; //0 : 대기 , 1 : 공격, 2 : 스킬, 3 : 아이템, 4 : 사망
    CharacterType type = CharacterType.Warrior;
    public GameObject attackTarget;
    public CharacterData target;
    public int hp, mp, attack, magic, defence, speed;
    List<CharacterData> enemy = new List<CharacterData>();
    List<CharacterData> playerParty = new List<CharacterData>();

    Collider collider;

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
                    StartCoroutine(Move());
                    anim.Play("1_Run");
                    break;
                case 2:
                    if (isTargetingSkill)
                    {
                        StartCoroutine(Move());
                        anim.Play("1_Run");
                    }
                    else
                    {
                        StartCoroutine(Skill());
                    }
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
        for (int i = 0; i < 4; i++)
        {
            playerParty.Add(battle.characters[i]);
        } 
        for (int i = 4; i < 8; i++)
        {
            enemy.Add(battle.characters[i]);
        }
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
        if (!isTargetingSkill)
        {
            yield return StartCoroutine(NormalAttack());
        }
        else if(isTargetingSkill && !isPlayer)
        {
                yield return null;
        }
        else if (isTargetingSkill && isPlayer)
        {
            while (true)
            {
                yield return null;
            }
        }
    }
    IEnumerator NormalAttack()
    {
        anim.Play("2_Attack_Normal");
        if (battle.Index == 0)
        {
            battle.Targetting(0, attackTarget); 
        }
        else
        {
            TakeDamage(battle.characters[Random.Range(4, 8)]);
        }
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
        foreach (var target in enemy)
        {
            TakeDamage(target);
        }
        BehaviorEnd = false;
    }
    public void OnMouseEnter()
    {
       battle.Targetting(System.Convert.ToInt32(gameObject.name[gameObject.name.Length - 1]));
    }
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            battle.OperateCharacter(1);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            collider.gameObject.GetComponent<CharacterData>();
            battle.TargetPoint.SetActive(false);
        }
    }
    void TakeDamage(CharacterData target)
    {
        if (target != null)
        {
            target.hp -= attack - target.defence;
        }
    }
}