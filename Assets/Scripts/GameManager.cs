using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //AudioSource eatSound;

    int hp=10;
    int mp=10;
    int atk=10;
    int magic=10;
    int def=10;
    int dex=10;

    int maxhp;
    int maxmp;
    int maxatk;
    int maxmagic;
    int maxdef;
    float maxspeed;

    public GameObject p_BaconEgg;
    public GameObject p_ChickenLeg;
    public GameObject p_BeerChicken;
    public GameObject p_Winebread;
    public GameObject p_CakeCookie;

    public GameObject hp_up;
    public GameObject mp_up;
    public GameObject atk_up;
    public GameObject magic_up;
    public GameObject def_up;
    public GameObject dex_up;

    public GameObject yesBtn; 
    public GameObject yesBtn1; 
    public GameObject yesBtn2; 
    public GameObject yesBtn3; 
    public GameObject yesBtn4; 
    public GameObject noBtn;
    public Text text;

    public Text hp_upText;
    public Text mp_upText;
    public Text atk_upText;
    public Text magic_upText;
    public Text def_upText;
    public Text dex_upText;

    public Text hp_now;
    public Text mp_now;
    public Text atk_now;
    public Text magic_now;
    public Text def_now;
    public Text dex_now;

    //private void Awake()
    //{
    //    eatSound = GetComponent<AudioSource>();
    //}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            //if (hit.collider != null)
            //{
            //    Debug.Log(hit.collider.gameObject.name);
            //}

            if (hit.collider.gameObject.tag == "BE")
            {
                p_ChickenLeg.gameObject.SetActive(false);
                p_BeerChicken.gameObject.SetActive(false);
                p_Winebread.gameObject.SetActive(false);
                p_CakeCookie.gameObject.SetActive(false);
                p_BaconEgg.gameObject.SetActive(true);

                mp_up.gameObject.SetActive(false);
                dex_up.gameObject.SetActive(false);
                def_up.gameObject.SetActive(false);
                magic_up.gameObject.SetActive(false);
                hp_up.gameObject.SetActive(true);
                atk_up.gameObject.SetActive(true);

                yesBtn1.gameObject.SetActive(false);
                yesBtn2.gameObject.SetActive(false);
                yesBtn3.gameObject.SetActive(false);
                yesBtn4.gameObject.SetActive(false);
                yesBtn.gameObject.SetActive(true);
                noBtn.gameObject.SetActive(true);

                text.text = "베이컨과 계란후라이를 먹을까?";
            }

            if (hit.collider.gameObject.tag == "CL")
            {
                p_BaconEgg.gameObject.SetActive(false);
                p_BeerChicken.gameObject.SetActive(false);
                p_Winebread.gameObject.SetActive(false);
                p_CakeCookie.gameObject.SetActive(false);

                hp_up.gameObject.SetActive(false);
                dex_up.gameObject.SetActive(false);
                def_up.gameObject.SetActive(false);
                magic_up.gameObject.SetActive(false);

                mp_up.gameObject.SetActive(true);
                atk_up.gameObject.SetActive(true);

                p_ChickenLeg.gameObject.SetActive(true);

                yesBtn.gameObject.SetActive(false);
                yesBtn2.gameObject.SetActive(false);
                yesBtn3.gameObject.SetActive(false);
                yesBtn4.gameObject.SetActive(false);
                yesBtn1.gameObject.SetActive(true);
                noBtn.gameObject.SetActive(true);

                text.text = "닭다리를 먹을까?";
            }

            if (hit.collider.gameObject.tag == "BC")
            {
                p_BaconEgg.gameObject.SetActive(false);
                p_ChickenLeg.gameObject.SetActive(false);
                p_Winebread.gameObject.SetActive(false);
                p_CakeCookie.gameObject.SetActive(false);

                hp_up.gameObject.SetActive(false);
                def_up.gameObject.SetActive(false);
                mp_up.gameObject.SetActive(false);
                atk_up.gameObject.SetActive(false);

                magic_up.gameObject.SetActive(true);
                dex_up.gameObject.SetActive(true);

                p_BeerChicken.gameObject.SetActive(true);

                yesBtn.gameObject.SetActive(false);
                yesBtn1.gameObject.SetActive(false);
                yesBtn3.gameObject.SetActive(false);
                yesBtn4.gameObject.SetActive(false);
                yesBtn2.gameObject.SetActive(true);
                noBtn.gameObject.SetActive(true);

                text.text = "치킨과 맥주를 먹을까?";
            }

            if (hit.collider.gameObject.tag == "WB")
            {
                p_BaconEgg.gameObject.SetActive(false);
                p_ChickenLeg.gameObject.SetActive(false);
                p_BeerChicken.gameObject.SetActive(false);
                p_CakeCookie.gameObject.SetActive(false);

                atk_up.gameObject.SetActive(false);
                magic_up.gameObject.SetActive(false);
                def_up.gameObject.SetActive(false);

                hp_up.gameObject.SetActive(true);
                mp_up.gameObject.SetActive(true);
                dex_up.gameObject.SetActive(true);


                p_Winebread.gameObject.SetActive(true);

                yesBtn.gameObject.SetActive(false);
                yesBtn1.gameObject.SetActive(false);
                yesBtn2.gameObject.SetActive(false);
                yesBtn4.gameObject.SetActive(false);
                yesBtn3.gameObject.SetActive(true);
                noBtn.gameObject.SetActive(true);

                text.text = "와인과 빵을 먹을까?";
            }
                
            if (hit.collider.gameObject.tag == "CC")
            {
                p_BaconEgg.gameObject.SetActive(false);
                p_ChickenLeg.gameObject.SetActive(false);
                p_BeerChicken.gameObject.SetActive(false);
                p_Winebread.gameObject.SetActive(false);

                hp_up.gameObject.SetActive(false);
                atk_up.gameObject.SetActive(false);
                dex_up.gameObject.SetActive(false);
                mp_up.gameObject.SetActive(false);

                def_up.gameObject.SetActive(true);
                magic_up.gameObject.SetActive(true);

                p_CakeCookie.gameObject.SetActive(true);

                yesBtn.gameObject.SetActive(false);
                yesBtn1.gameObject.SetActive(false);
                yesBtn1.gameObject.SetActive(false);
                yesBtn3.gameObject.SetActive(false);
                yesBtn4.gameObject.SetActive(true);
                noBtn.gameObject.SetActive(true);

                text.text = "케이크와 쿠키를 먹을까?";
            }

            //else if (hit.collider.gameObject.tag == "BG")
            //{
            //    EraseFood();
            //    EraseBtnArTxt();
            //}
            //되긴 하는ㄷㅔ 버튼은 하나도 안 눌림...........
        }
    }

    public void EraseBtnArTxt()
    {
        yesBtn.gameObject.SetActive(false);
        yesBtn1.gameObject.SetActive(false);
        yesBtn2.gameObject.SetActive(false);
        yesBtn3.gameObject.SetActive(false);
        yesBtn4.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);

        hp_up.gameObject.SetActive(false);
        atk_up.gameObject.SetActive(false);
        dex_up.gameObject.SetActive(false);
        mp_up.gameObject.SetActive(false);
        def_up.gameObject.SetActive(false);
        magic_up.gameObject.SetActive(false);

        text.text = "";
    }

    public void BE()
    {
        int hpup = Random.Range(5, 11);
        int atkup = Random.Range(5, 11);

        hp += hpup;
        hp_upText.text = $"+{hpup}";

        atk += atkup;
        atk_upText.text = $"+{atkup}";

        hp_now.text = $"{hp}";
        atk_now.text = $"{atk}";
        dex_now.text = $"{dex}";
        mp_now.text = $"{mp}";
        def_now.text = $"{def}";
        magic_now.text = $"{magic}";

        //eatSound.Play();
    }

    public void CL()
    {
        int mpup = Random.Range(5, 11);
        int atkup = Random.Range(5, 11);

        mp += mpup;
        mp_upText.text = $"+{mpup}";

        atk += atkup;
        atk_upText.text = $"+{atkup}";

        hp_now.text = $"{hp}";
        atk_now.text = $"{atk}";
        dex_now.text = $"{dex}";
        mp_now.text = $"{mp}";
        def_now.text = $"{def}";
        magic_now.text = $"{magic}";

        //eatSound.Play();
    }

    public void EraseFood()
    {
        p_BaconEgg.gameObject.SetActive(false);
        p_ChickenLeg.gameObject.SetActive(false);
        p_BeerChicken.gameObject.SetActive(false);
        p_Winebread.gameObject.SetActive(false);
        p_CakeCookie.gameObject.SetActive(false);
    }

    //스탯 최댓값 정하기
    //화면 이미지에 콜라이더넣고(클릭 되도록) 콜라이더 제트값을 음식 제트값 보다 크게한다음에.. 클릭하면 취소되게?

}