using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Test_Skill : MonoBehaviour
{
    public GameObject player;


    public GameObject partner1;
    public GameObject partner2;
    public GameObject partner3;

    public GameObject goblin;
    public GameObject fiend;
    public GameObject golem;
    public GameObject darkLord;

    Button movebtn;
    Button attackbtn;
    Button skillbtn;


    Transform movePoint;    //플레이어 이동지점
    Transform movePoint2;    //몬스터 이동지점
    public Animator anim;
    public float moveSpeed = 10;

    public CharacterData characters;


    public GameObject testplayer;
    public Animator testAnim;

    public ParticleSystem fireparticle;
    public GameObject cureparticle;
    //public GameObject shadow;

    public bool isPlay = false;


    private void Awake()
    {
        movebtn = GameObject.Find("Move").GetComponent<Button>();
        attackbtn = GameObject.Find("Attack").GetComponent<Button>();
        skillbtn = GameObject.Find("Skill").GetComponent<Button>();
        movePoint = GameObject.Find("MovePoint").gameObject.transform;
        movePoint2 = GameObject.Find("MovePoint_2").gameObject.transform;

        characters = FindObjectOfType<CharacterData>();

        fireparticle = GameObject.Find("Mage_Particle_fire").GetComponent<ParticleSystem>();

    }


    void Start()
    {
        player = Instantiate(player, new Vector3(-6.0f, 3f, 0), Quaternion.identity);
        partner1 = Instantiate(partner1, new Vector3(-6.0f, 1f, 0), Quaternion.identity);
        partner2 = Instantiate(partner2, new Vector3(-6.0f, -1f, 0), Quaternion.identity);
        partner3 = Instantiate(partner3, new Vector3(-6.0f, -3f, 0), Quaternion.identity);
   

        goblin = Instantiate(goblin, new Vector3(6.0f, 3.0f,0), Quaternion.identity);
        fiend = Instantiate(fiend, new Vector3(6.0f, 1, 0), Quaternion.identity);
        golem = Instantiate(golem, new Vector3(6.0f, -1.0f, 0), Quaternion.identity);
        darkLord = Instantiate(darkLord, new Vector3(6.0f, -3.0f, 0), Quaternion.identity);

        anim = player.GetComponent<Animator>();
        movebtn.onClick.AddListener(onMove);
        attackbtn.onClick.AddListener(onAttack);
        skillbtn.onClick.AddListener(onSkill);

        testAnim = testplayer.GetComponent<Animator>();
        cureparticle.SetActive(false);
        //shadow.SetActive(false);

    }

    private void Update()
    {
        if (Keyboard.current.digit1Key.isPressed)
        {
            isPlay = true;
            if(isPlay)
            {
                SkillWarrior();

            }

        }else if (Keyboard.current.digit2Key.isPressed)
        {
            isPlay = true;
            if (isPlay)
            {
                SkillMage();

            }

        }else if (Keyboard.current.digit3Key.isPressed)
        {
            isPlay = true;
            if (isPlay)
            {
                SkillCleric();

            }
        }else if(Keyboard.current.digit4Key.isPressed)
        {
            isPlay = true;
            if (isPlay)
            {
                SkillThief();

            }

        }else if (Keyboard.current.digit5Key.isPressed)
        {
            isPlay = true;
            if (isPlay)
            {
                SkillPopstar();

            }
        }else if (Keyboard.current.digit6Key.isPressed)
        {
            isPlay = true;
            if (isPlay)
            {
                SkillChef();

            }
        }

    }


    //일반공격, 지속시간 미정
    private void onAttack()
    {
        testAnim.Play("2_Attack_Normal");
    }

    //스킬공격
    private void onSkill()
    {
        //일반 공격
        testAnim.Play("2_Attack_Magic");

        //워리어 스킬 : 점프베기, 마나 2 감소, 높이 뛰어올라 검을 내려쳐 적 하나를 벱니다.
        //마법사 스킬 : 파이어, 2, 적 하나에게 마법의 불꽃을 날려 피해를 줍니다.
        //승려 : 큐어, 2, 기도의 힘으로 HP를 회복한다. 
        //도적 : 함정, 2, 자신 앞에 함정을 설치해 공격해 오는 적에게 피해를 줍니다.
        //아이돌 : 적을 위한 노래 (Earworm), 2, "한 턴 동안 적의 주의를 산만하게 해서 노래에 맞춰 춤을 추게 한다."
        //쉐프 : 회복요리,  3, 요리를 만들어 동료 한 명의 체력을 회복시킵니다.


    }

    void SkillWarrior()     //점프베기, trail_blue
    {
        float skillTime = 1f;

        while(skillTime >0)
        {
            testAnim.Play("5_Skill_Magic");
            skillTime -= Time.deltaTime;
        }
        //anim.Stop("5_Skill_Magic");

        //임시 :  마나2 감소 
        characters.mp -= 2;

    }

    void SkillMage()        //파이어, 파티클 + 리지드바디2D
    {
        testAnim.Play("5_Skill_Bow");

        characters.mp -= 2;

        //파이어
        //임시로 고블린을 타겟으로 
        Vector3 dir = darkLord.transform.position - fireparticle.transform.position;
        
        fireparticle.transform.Rotate(0, 0, 60f);

        
        if (dir.sqrMagnitude > 0.01f)
        {
            fireparticle.GetComponent<Rigidbody2D>().velocity = dir.normalized * moveSpeed; 
        }
    }

    void SkillCleric()       //큐어, 2, 기도의 힘으로 HP를 회복
    {
        testAnim.Play("2_Attack_Bow");
        characters.mp -= 2;

        cureparticle.SetActive(true);

    }

    void SkillThief()       //함정, 2, 적 앞에 함정 만들기
    {
        testAnim.Play("5_Skill_Normal");
        characters.mp -= 2;
        //랜덤한 위치에 함정 표시

       // shadow.SetActive(true);
        //shadow.GetComponent<SpriteRenderer>().color = MathF.Ler




    }

    void SkillPopstar()     //적을 위한 노래 (Earworm), 적이 춤을 추고 공격을 안 함
    {
        testAnim.Play("2_Attack_Bow");
        characters.mp -= 2;
    }

    void SkillChef()        //회복요리,  3, 요리를 만들어 자신과 동료 한 명의 체력을 회복
    {
        testAnim.Play("2_Attack_Magic");
        characters.mp -= 2;
    }


    private void onMove()
    {
        while ((movePoint.position - player.transform.position).sqrMagnitude > 0.1f)
        {
            Move();
        }
    }

    private void Move()
    {
        //애니메이션 제외   
        player.transform.position = Vector3.MoveTowards(player.transform.position, movePoint.position, Time.deltaTime * moveSpeed);
        goblin.transform.position = Vector3.MoveTowards(goblin.transform.position, movePoint2.position, Time.deltaTime * moveSpeed);

    }



}
