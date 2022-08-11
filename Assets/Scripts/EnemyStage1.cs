using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStage1 : MonoBehaviour, IHealth, IBattle
{
    //stage 창
    public GameObject NowStage;
    public GameObject selectStage;
    public GameObject Repair1;
    public GameObject STAGE2;


    Animator anim;
    OpenGrass_Scene stage1To2;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

   
    //ihealth

    public float hp = 100.0f;
    public float maxHp = 100.0f;

    public float HP
    {
        get => hp;
        set
        {
            hp = value;
           // onHealthChange.Invoke();
        }
    }

    public float MaxHP
    {
        get => maxHp;
    }


    public System.Action onHealthChange { get; set; }

    //ibattle
    float attackCoolTime = 1.0f;
    float attackSpeed = 1.0f;
    float criticalRate = 0.1f;

    float attackPower = 10.0f;
    float deffencePower = 10.0f;

    public float AttackPower
    {
        get => attackPower;
    }

    public float DefencePower
    {
        get => deffencePower;
    }

    public void Attack(IBattle target)
    {
        if(target != null) 
        { 
            float damage = AttackPower;
            
            if (Random.Range(0.0f,1.0f)< criticalRate)
            {
                damage *= 2.0f;
            }
            target.TakeDamage(damage);

        }

    }

    public void TakeDamage(float damage)
    {
        float realDamage = damage - deffencePower;
        if(realDamage < 1.0f)
        {
            realDamage = 1.0f;
        }
        HP -= realDamage;
        if(HP > 0.0f)
        {
            
            anim.SetTrigger("Hit");
            attackCoolTime = attackSpeed;
        }
        else
        {
            Die();


            NowStage.SetActive(false); // 현재 닫히고
            selectStage.SetActive(true); // 선택 창 열리고
            GameObject.Find("Repair1").GetComponent<Button>().interactable = true; // 여관1 버튼 활성화
            GameObject.Find("STAGE_2").GetComponent<Button>().interactable = true; // 스테이지 2 버튼 활성화
        }
        void Die()
        {
            transform.position = Vector3.zero;
            anim.SetTrigger("Die");


        }
    }
    
       
          
       
  
}
